using MarblesAndMonsters.Events;
using System;
using System.Collections;
using UnityEngine;

namespace MarblesAndMonsters.Characters
{
    //Controller for characters
    //  Executes actions based on the attached character sheet (Unity component)
    //  Core Loop:
    //      1) State Check (fire, poison, sleep, etc)
    //      2) Movement

    public abstract class CharacterControl: MonoBehaviour, IDamagable
    {
        #region Properties
        //particle effects
        public ParticleSystem hitEffect;    //plays when stuck/attacked/damaged
        public ParticleSystem healEffect;   //plays when healing (players use a potion, monster regenerates, etc.)
        public ParticleSystem invincibilityEffect;
        public ParticleSystem fireEffect;

        public EventHandler<DeathEventArgs> OnDying;
        public EventHandler<DamageEventArgs> OnDamage;

        //rigidbody 
        public Rigidbody2D MyRigidbody;

        protected CharacterSheet mySheet;
        protected SpriteRenderer mySpriteRenderer;

        //input storage
        protected float input_horizontal;
        protected float input_vertical;

        public float ForceMultiplier = 1.0f;

        [SerializeField]
        private float defaultInvincibilityTime = 1.0f;

        protected AnimatorController animatorController;
        

        //sound control
        protected AudioSource audioSource;

        public bool isDying = false;   //similar to invincibility flag for ensuring multiple death calls won't be evaluated

        protected SpawnPoint spawnPoint;

        protected bool Respawn = false;

        public CharacterSheet MySheet => mySheet; //read-only accessor for accessing stats directly (for hp, attack/def values, etc)

        protected GameManager _gameManager;
        protected CharacterManager _characterManager;
        #endregion

        #region Unity Scripts
        protected virtual void Awake()
        {
            //cache some components
            mySheet = GetComponent<CharacterSheet>();
            MyRigidbody = GetComponent<Rigidbody2D>();
            mySpriteRenderer = GetComponent<SpriteRenderer>();
            animatorController = GetComponent<AnimatorController>();
            audioSource = GetComponent<AudioSource>();

            _gameManager = FindObjectOfType<GameManager>();
            _characterManager = FindObjectOfType<CharacterManager>();


        }

        protected virtual void OnEnable()
        {
            ResetHealth();
            isDying = false;
            //comment: subscribe!
            MySheet.OnInvincible += InvincibileOnHandler;
            MySheet.OnInvincibleEnd += InvincibileOffHandler;
        }

        protected virtual void Start()
        {
            //_gameManager.StoreCharacter(this);
            if (_characterManager.StoreCharacter(this))
            {
                //Debug.Log(String.Format("{0} has been added to Characters on GameController", this.gameObject.name));
            }
        }

        protected virtual void Update()
        {
            //death check
            if (MySheet.CurrentHealth <= 0)
            {
                CharacterDeath(DeathType.Damage);
            }
            
            
        }

        protected virtual void OnDisable()
        {
            //MySheet.OnBurning -= FireOnHandler;
            //MySheet.OnBurningEnd -= FireOffHandler;
            MySheet.OnInvincible -= InvincibileOnHandler;
            MySheet.OnInvincibleEnd -= InvincibileOffHandler;
            //if this character is the respawning type, start the spawn coroutine
            if (mySheet.RespawnFlag)
            {
                if (spawnPoint != null)
                {
                    spawnPoint.RemoteTriggerSpawn(mySheet.RespawnPeriod);
                }
            }
        }
        #endregion

        #region Damage and Powers
        public virtual void TakeDamage(int damageAmount, DamageType damageType)
        {
            //check for invincibility
            if (mySheet.IsInvincible || mySheet.DamageImmunities.Contains(DamageType.All))
            {
                //Debug.Log(string.Format("{0} is invincible!", gameObject.name));
            }
            //check immunity to damage type
            else if (mySheet.DamageImmunities.Contains(damageType))
            {
                //Debug.Log(string.Format("{0} is immune to {1} and takes no damage!", gameObject.name, damageType));
            }
            //check damage > armor
            else if (damageAmount <= mySheet.Armor)
            {
                //Debug.Log(string.Format("{0}'s armor absorbs all damage!", gameObject.name));
            }
            else
            {
                //take that damage!
                //adjust current health
                mySheet.CurrentHealth -= (damageAmount - mySheet.Armor);
                //set state for unique damage types
                //if (damageType == DamageType.Fire) { ApplyFire(); }
                //if (damageType == DamageType.Poison) { ApplyPoison(); }
                //if (damageType == DamageType.Ice) { ApplyIce(); }

                //check for death, if still alive, play particle effect and hit animation
                if (mySheet.CurrentHealth <= 0)
                {
                    //Debug.Log("Death trigger from TakeDamage()");
                    CharacterDeath(DeathType.Damage);
                }
                else
                {
                    hitEffect.Play();   //particles
                    OnDamage?.Invoke(this, new DamageEventArgs(damageType));
                    //animator.SetTrigger(aTriggerDamageNormal);
                    ApplyInvincible(defaultInvincibilityTime);
                }
            }
        }

        public virtual void ApplyDamageEffect(Type damageType)
        {

        }

        public virtual void ApplyInvincible(float duration)
        {
            MySheet.IsInvincible = true;
            MySheet.InvincibleTimeCounter = duration;
        }

        void InvincibileOnHandler(object sender, EventArgs e)
        {
            if (invincibilityEffect)
            {
                invincibilityEffect.Play();
            }
        }

        void InvincibileOffHandler(object sender, EventArgs e)
        {
            if (invincibilityEffect)
            {
                invincibilityEffect.Stop();
            }
        }

        public void ApplyImpulse(Vector2 force)
        {
            MyRigidbody.AddForce(force, ForceMode2D.Impulse);
        }

        public void ApplyForce(Vector2 force)
        {
            MyRigidbody.AddForce(force, ForceMode2D.Force);
        }

        public void ApplyFalling(Vector3 position)
        {
            if (!isDying && !MySheet.IsLevitating)
            {

                MyRigidbody.isKinematic = false;
                CharacterDeath(DeathType.Falling);
            }
        }

        public virtual bool HealDamage(int healAmount)
        {
            return false;
        }

        //
        
        #endregion

        #region Spawning and Dying
        internal virtual void SetSpawnPoint(SpawnPoint _spawnPoint)
        {
            spawnPoint = _spawnPoint;
        }

        private void ResetHealth()
        {
            mySheet.CurrentHealth = mySheet.MaxHealth;
        }

        public virtual void CharacterDeath(DeathType deathType)
        {
            if (isDying) 
            {
                return; 
            }
            
            isDying = true;

            if (MyRigidbody.isKinematic)
            {
                MyRigidbody.velocity = Vector2.zero;
            }
            PreDeathAnimation();
            OnDying?.Invoke(this, new DeathEventArgs(deathType));
            _characterManager.Characters.Remove(this);
        }

        /// <summary>
        /// Hook for things you want to be done just before the death animation is played
        /// </summary>
        protected virtual void PreDeathAnimation()
        {

        }

        
        #endregion
    }
}