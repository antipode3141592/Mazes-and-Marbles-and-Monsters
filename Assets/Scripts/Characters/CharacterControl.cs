using MarblesAndMonsters.Events;
using MoreMountains.Feedbacks;
using System;
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
        // More Mountains Feel Feedback Systems
        [SerializeField] protected MMFeedbacks collisionEffects;
        [SerializeField] protected MMFeedbacks hitEffects;
        [SerializeField] protected MMFeedbacks invincibilityEffects;
        [SerializeField] protected MMFeedbacks healEffects;
        protected float collisionIntensity = 1f;

        public EventHandler<DeathEventArgs> OnDying;
        public EventHandler<DamageEventArgs> OnDamage;

        //rigidbody 
        public Rigidbody2D MyRigidbody;
        protected CharacterSheet mySheet;
        protected SpriteRenderer mySpriteRenderer;

        public float ForceMultiplier = 1.0f;

        [SerializeField] float defaultInvincibilityTime = 1.0f;
        protected bool isInvincible = false;
        protected float invincibleTimeCounter = 0f;

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

            _gameManager = FindObjectOfType<GameManager>();
            _characterManager = FindObjectOfType<CharacterManager>();
        }

        protected virtual void OnEnable()
        {
            ResetHealth();
            isDying = false;
            isInvincible = false;
            animatorController.OnDeathAnimationComplete += OnDeathAnimationCompleted;
        }

        protected virtual void Start()
        {
            _characterManager.StoreCharacter(this);
        }

        protected virtual void Update()
        {
            //death check
            if (MySheet.CurrentHealth <= 0)
            {
                CharacterDeath(DeathType.Damage);
                return;
            }
            //decrement state counters
            if (isInvincible)
            {
                invincibleTimeCounter -= Time.deltaTime;
                if (invincibleTimeCounter <= 0.0f)
                {
                    invincibleTimeCounter = 0.0f;
                    isInvincible = false;
                }
            }
        }

        protected virtual void OnDisable()
        {
            animatorController.OnDeathAnimationComplete -= OnDeathAnimationCompleted;
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
            if (isInvincible || mySheet.DamageImmunities.Contains(DamageType.All))
                return;
            if (mySheet.DamageImmunities.Contains(damageType))
                return;
            if (damageAmount <= mySheet.Armor)
                return;
            
            mySheet.CurrentHealth -= (damageAmount - mySheet.Armor);
            //set state for unique damage types
            //if (damageType == DamageType.Fire) { ApplyFire(); }
            //if (damageType == DamageType.Poison) { ApplyPoison(); }
            //if (damageType == DamageType.Ice) { ApplyIce(); }
            hitEffects.PlayFeedbacks();
            OnDamage?.Invoke(this, new DamageEventArgs(damageType));
            isInvincible = true;
            invincibleTimeCounter = defaultInvincibilityTime;
            invincibilityEffects.PlayFeedbacks();
        }

        public virtual void ApplyDamageEffect(Type damageType)
        {

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
                CharacterDeath(DeathType.Falling);
        }

        public virtual bool HealDamage(int healAmount)
        {
            healEffects.PlayFeedbacks();
            return false;
        }
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
                return; 
            
            isDying = true;

            MyRigidbody.velocity = Vector2.zero;
            MyRigidbody.isKinematic = false;
            MyRigidbody.simulated = false;
            
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

        public virtual void OnDeathAnimationCompleted(object sender, DeathEventArgs deathEventArgs)
        {
            spawnPoint.Reset();
        }
        #endregion
    }
}