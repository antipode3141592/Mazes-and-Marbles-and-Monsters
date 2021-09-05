using System;
using System.Collections;
using System.Collections.Generic;
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

        //rigidbody and collider references
        public Rigidbody2D myRigidbody;
        //protected List<Collider2D> myColliders;
        [SerializeField]
        public Collider2D meleeCollider; //trigger collider for melee attack range
        [SerializeField]
        public Collider2D rangedCollider; //trigger collider for ranged attack range
        protected CharacterSheet mySheet;
        protected SpriteRenderer mySpriteRenderer;

        //input storage
        protected float input_horizontal;
        protected float input_vertical;

        public float ForceMultiplier = 1.0f;

        //animation control
        protected Animator animator;
        protected float _speed;
        protected Vector2 lookDirection = new Vector2(1,0); //default look right
        protected int aFloatSpeed;
        protected int aFloatLookX;
        protected int aFloatLookY;
        protected int aTriggerDamageNormal;
        protected int aTriggerFalling;
        protected int aTriggerDeathByDamage;

        //attack stuff
        [SerializeField]
        protected float ProjectileRestPeriod = 1.0f;
        public bool RangedAttackIsAvailable = true;
        [SerializeField]
        protected float TouchAttackRestPeriod = 1.0f;
        public bool TouchAttackIsAvailable = true;

        //sound control
        protected AudioSource audioSource;

        public bool isDying = false;   //similar to invincibility flag for ensuring multiple death calls won't be evaluated

        protected SpawnPoint spawnPoint;

        protected bool Respawn = false;

        public CharacterSheet MySheet => mySheet; //read-only accessor for accessing stats directly (for hp, attack/def values, etc)
        #endregion

        #region Unity Scripts
        protected virtual void Awake()
        {
            //cache some components
            mySheet = GetComponent<CharacterSheet>();
            //myColliders = new List<Collider2D>(GetComponents<Collider2D>());
            myRigidbody = GetComponent<Rigidbody2D>();
            mySpriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();

            aFloatSpeed = Animator.StringToHash("Speed");
            aFloatLookX = Animator.StringToHash("Look X");
            aFloatLookY = Animator.StringToHash("Look Y");
            aTriggerDamageNormal = Animator.StringToHash("DamageNormal");
            aTriggerFalling = Animator.StringToHash("Falling");
            aTriggerDeathByDamage = Animator.StringToHash("DeathByDamage");   
        }

        protected virtual void OnEnable()
        {
            ResetHealth();
            isDying = false;
            //comment: subscribe!
            MySheet.OnBurning += FireOnHandler;
            MySheet.OnBurningEnd += FireOffHandler;
            MySheet.OnInvincible += InvincibileOnHandler;
            MySheet.OnInvincibleEnd += InvincibileOffHandler;
            //MySheet.OnLevitating += LevitateOnHandler;
            //MySheet.OnLevitatingEnd += LevitateOffHandler;
        }

        protected virtual void Start()
        {
            if (GameManager.Instance.StoreCharacter(this))
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
            //grab acceleration input
            SetLookDirection();
            animator.SetFloat(aFloatSpeed, myRigidbody.velocity.magnitude);
        }

        protected virtual void OnDisable()
        {
            MySheet.OnBurning -= FireOnHandler;
            MySheet.OnBurningEnd -= FireOffHandler;
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
            try
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
                    if (damageType == DamageType.Fire) { ApplyFire(); }
                    if (damageType == DamageType.Poison) { ApplyPoison(); }
                    if (damageType == DamageType.Ice) { ApplyIce(); }

                    //check for death, if still alive, play particle effect and hit animation
                    if (mySheet.CurrentHealth <= 0)
                    {
                        //Debug.Log("Death trigger from TakeDamage()");
                        CharacterDeath(DeathType.Damage);
                    }
                    else
                    {
                        hitEffect.Play();   //particles
                        animator.SetTrigger(aTriggerDamageNormal);
                        ApplyInvincible(GameManager.Instance.DefaultInvincibilityTime);
                    }
                }
                //
            }catch(Exception ex)
            {
                Debug.LogException(ex);
            }
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

        internal virtual void ApplyFire()
        {
            //add 
        }

        void FireOnHandler(object sender, EventArgs e)
        {
            if (fireEffect)
            {
                fireEffect.Play();
            }
        }

        void FireOffHandler(object sender, EventArgs e)
        {
            if (fireEffect)
            {
                fireEffect.Stop();
            }
        }

        internal virtual void ApplyPoison()
        {
            throw new System.NotImplementedException();
        }

        internal virtual void ApplyIce()
        {
            throw new System.NotImplementedException();
        }

        public void ApplyImpulse(Vector2 force)
        {
            myRigidbody.AddForce(force, ForceMode2D.Impulse);
        }

        public void ApplyForce(Vector2 force)
        {
            myRigidbody.AddForce(force, ForceMode2D.Force);
        }

        public void ApplyFalling(Vector3 position)
        {
            if (!MySheet.IsLevitating)
            {
                myRigidbody.MovePosition(position);
                CharacterDeath(DeathType.Falling);
            }
        }

        public virtual bool HealDamage(int healAmount)
        {
            return false;
        }

        public virtual void DealDamageTo(IDamagable damagable)
        {
            damagable.TakeDamage(mySheet.baseStats.TouchAttack.DamageModifier, mySheet.baseStats.TouchAttack.DamageType);
        }

        public virtual void RangedAttack()
        {

        }

        public virtual void MeleeAttack()
        {

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
            //Debug.Log(string.Format("{0} - Current Health: {1}, Max Health: {2}", this.gameObject.name, mySheet.CurrentHealth, mySheet.MaxHealth));
        }

        public virtual void CharacterDeath(DeathType deathType)
        {
            if (isDying) 
            {
                return; 
            }
            else
            {
                isDying = true;
                myRigidbody.velocity = Vector2.zero;
                PreDeathAnimation();
                switch (deathType)
                {
                    case DeathType.Falling:
                        animator.SetTrigger(aTriggerFalling);
                        audioSource.clip = MySheet.baseStats.ClipDeathFall;
                        audioSource.Play();
                        break;
                    case DeathType.Damage:
                        animator.SetTrigger(aTriggerDeathByDamage);
                        break;
                    case DeathType.Fire:
                        break;
                    case DeathType.Poison:
                        break;
                    default:
                        Debug.LogError("Unhandled deathtype enum!");
                        break;
                }
                StartCoroutine(DeathAnimation(deathType));
            }
        }

        /// <summary>
        /// Hook for things you want to be done just before the death animation is played
        /// </summary>
        protected virtual void PreDeathAnimation()
        {

        }

        //plays the death animation at a specific location
        //  common usage:  freeze position of character directly over pit before death animation
        public virtual void CharacterDeath(DeathType deathType, Vector2 position, Quaternion rotation)
        {
            //set position and rotation to the inputs
            CharacterDeath(deathType);
        }

        protected virtual IEnumerator DeathAnimation(DeathType deathType)
        {
            yield return new WaitForSeconds(0.5f);  //death animations are 8 frames, current fps is 12
            Destroy(this);
        }
        #endregion

        #region Animation Stuff

        //set the look direction based on the accerometer input
        //  look direction is independent of movement calculations in FixedUpdate
        //  helps to determine 
        protected virtual void SetLookDirection()
        {
            Vector2 input_acceleration = GameManager.Instance.Input_Acceleration;
            if (!Mathf.Approximately(input_acceleration.x, 0.0f) || !Mathf.Approximately(input_acceleration.y, 0.0f))
            {
                lookDirection = input_acceleration;
                lookDirection.Normalize();
            }

            animator.SetFloat(aFloatLookX, lookDirection.x);
            animator.SetFloat(aFloatLookY, lookDirection.y);
        }

        public virtual void UpdateSpriteMaterial(Material material)
        {
            mySpriteRenderer.material = material;
        }

        public virtual Material GetCurrentMaterial()
        {
            return mySpriteRenderer.material;
        }

        public virtual void ResetMaterial()
        {
            mySpriteRenderer.material = MySheet.DefaultMaterial;
        }

        public virtual void SetAnimationSpeed(float targetSpeed)
        {
            animator.speed = targetSpeed;
        }

        public virtual void SetBodyType(RigidbodyType2D rigidbodyType2D)
        {
            myRigidbody.bodyType = rigidbodyType2D;
        }

        protected IEnumerator ProjectileCooldown()
        {
            yield return new WaitForSeconds(ProjectileRestPeriod);
            RangedAttackIsAvailable = true;
        }

        protected IEnumerator TouchAttackCooldown()
        {
            yield return new WaitForSeconds(TouchAttackRestPeriod);
            TouchAttackIsAvailable = true;
        }
        #endregion
    }
}