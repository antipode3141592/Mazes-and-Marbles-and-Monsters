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

    public abstract class CharacterSheetController: MonoBehaviour, IDamagable
    {
        //particle effects
        public ParticleSystem hitEffect;    //plays when stuck/attacked/damaged
        public ParticleSystem healEffect;   //plays when healing (players use a potion, monster regenerates, etc.)
        public ParticleSystem invincibilityEffect;

        //rigidbody and collider references
        protected Rigidbody2D myRigidbody;
        protected List<Collider2D> myColliders;
        protected CharacterSheet mySheet;
        protected SpriteRenderer mySpriteRenderer;

        //input storage
        //protected Vector2 input_acceleration;
        protected float input_horizontal;
        protected float input_vertical;

        //animation control
        protected Animator animator;
        protected float _speed;
        protected Vector2 lookDirection = new Vector2(1,0); //default look right

        protected bool isDying = false;   //similar to invincibility flag for ensuring multiple death calls won't be evaluated

        [SerializeField]
        protected SpawnPoint spawnPoint;

        protected bool Respawn = false;

        public CharacterSheet MySheet => mySheet; //read-only accessor for accessing stats directly (for hp, attack/def values, etc)


        #region Unity Scripts
        protected virtual void Awake()
        {
            //cache some components
            mySheet = GetComponent<CharacterSheet>();
            myColliders = new List<Collider2D>(GetComponents<Collider2D>());
            myRigidbody = GetComponent<Rigidbody2D>();
            mySpriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        protected virtual void Start()
        {
            mySheet.CurrentHealth = mySheet.MaxHealth;
            if (GameController.Instance.StoreCharacter(this))
            {
                Debug.Log(String.Format("{0} has been added to Characters on GameController", this.gameObject.name));
            }
        }

        protected virtual void OnEnable()
        {
            mySheet.CurrentHealth = mySheet.MaxHealth;
            isDying = false;
        }

        //saddest state machine
        protected virtual void Update()
        {
            //default Update action

            //grab acceleration input
            SetLookDirection();

            //state checks:
            //  invincible
            if (mySheet.IsInvincible)
            {
                mySheet.InvincibleTimeCounter -= Time.deltaTime;
                if (mySheet.InvincibleTimeCounter <= 0.0f)
                {
                    mySheet.IsInvincible = false;
                    //disable invincibility effect
                    invincibilityEffect.Stop();
                }
            }
            //  fire - if firetoken.count > 0, take 1 damage and remove fire token
            //  poison - if poison... "  "
            //  frozen - ... "   "
            //  death for 0 or less hp
            //if (mySheet.CurrentHealth <= 0)
            //{
            //    Debug.Log("death check from charactersheetcontroller update()");
            //    CharacterDeath(DeathType.Damage);
            //}
        }

        protected virtual void FixedUpdate()
        {
            animator.SetFloat("Speed", myRigidbody.velocity.magnitude);
        }

        protected virtual void OnDisable()
        {   
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

        #region Damage and Effects
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
                if (damageType == DamageType.Fire) { ApplyFire(); }
                if (damageType == DamageType.Poison) { ApplyPoison(); }
                if (damageType == DamageType.Ice) { ApplyIce(); }
                
                //check for death, if still alive, play particle effect and hit animation
                if (mySheet.CurrentHealth <= 0)
                {
                    Debug.Log("Death trigger from TakeDamage()");
                    CharacterDeath(DeathType.Damage);
                }
                else
                {
                    hitEffect.Play();   //particles
                    animator.SetTrigger("DamageNormal");
                    ApplyInvincible();
                }
            }
            //
        }

        public virtual void ApplyInvincible()
        {
            mySheet.IsInvincible = true;
            //apply invincibility effect
            invincibilityEffect.Play();
            mySheet.InvincibleTimeCounter = GameController.Instance.DefaultEffectTime;
        }

        internal virtual void ApplyFire()
        {
            //add 
        }

        internal virtual void ApplyPoison()
        {
            throw new System.NotImplementedException();
        }

        internal virtual void ApplyIce()
        {
            throw new System.NotImplementedException();
        }

        public virtual void HealDamage(int healAmount)
        {

        }

        ////Adjust Health
        ////  adds value to current health
        //public virtual void AdjustHealth(int value)
        //{
        //    //add health or subtract health?
        //    if (value >= 0)
        //    {
        //        //check for damage
        //        if (mySheet.CurrentHealth < mySheet.MaxHealth)
        //        {
        //            //add health
        //            mySheet.CurrentHealth += value;
        //            //add health animation

        //            //add health particle effect
        //            healEffect.Play();
        //            //update UI
        //            //GameMenu.Instance.UpdateHealth()
        //        } else
        //        {
        //            //no effect
                    
        //        }

        //    } else
        //    {
        //        //subtract health;
        //        mySheet.CurrentHealth -= value;
        //        //damage animation;

        //        //death check;
        //        if (MySheet.CurrentHealth <= 0) 
        //        {
        //            CharacterDeath();
        //        }
                

        //    }
        //}
        #endregion

        #region Life and Death
        internal virtual void SetSpawnPoint(SpawnPoint _spawnPoint)
        {
            spawnPoint = _spawnPoint;
        }

        public virtual void CharacterSpawn()
        {
            gameObject.transform.position = mySheet.SpawnPoint;
            mySheet.Wakeup();
            foreach (Collider2D collider in myColliders)
            {
                collider.enabled = true;
            }
            //myRigidbody.WakeUp();
            mySheet.CurrentHealth = mySheet.MaxHealth;
        }

        public virtual void CharacterSpawn(Vector3 spawnPosition)
        {
            gameObject.transform.position = spawnPosition;
            mySheet.Wakeup();
            foreach (Collider2D collider in myColliders)
            {
                collider.enabled = true;
            }
            //myRigidbody.WakeUp();
            mySheet.CurrentHealth = mySheet.MaxHealth;
        }

        public virtual void CharacterDeath()
        {
            //stop movement immediately
            myRigidbody.velocity = Vector2.zero;
            mySheet.PutToSleep();   //go to sleep so Move() is not called on character
            //turn off colliders so other objects can pass through
            foreach (Collider2D collider in myColliders)
            {
                collider.enabled = false;
            }
            //death animation
            StartCoroutine(DeathAnimation());
            //DeathAnimation();
        }

        public virtual void CharacterDeath(DeathType deathType)
        {
            if (isDying) { return; }
            else
            {
                isDying = true;

                //Debug.Log(string.Format("CharacterDeath(DeathType deathType):  {0} has died by {1}", gameObject.name, deathType.ToString()));
                switch (deathType)
                {
                    case DeathType.Falling:
                        animator.SetTrigger("Falling");
                        break;
                    case DeathType.Damage:
                        animator.SetTrigger("DeathbyDamage");
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

        //plays the death animation at a specific location
        //  common usage:  freeze position of character directly over pit before death animation
        public virtual void CharacterDeath(DeathType deathType, Vector2 position, Quaternion rotation)
        {
            //set position and rotation to the inputs

            CharacterDeath(deathType);
        }

        protected virtual IEnumerator DeathAnimation()
        //private void DeathAnimation()
        {
            //Debug.Log(string.Format("{0} has died!", gameObject.name));
            yield return new WaitForSeconds(0.5f);
            gameObject.SetActive(false);
        }

        protected virtual IEnumerator DeathAnimation(DeathType deathType)
        {
            //Debug.Log(string.Format("DeathAnimation {0} has died of {1}!", gameObject.name, deathType.ToString()));
            yield return new WaitForSeconds(0.7f);  //death animations are 8 frames, current fps is 12
            gameObject.SetActive(false);
        }
        #endregion

        #region Animation Stuff

        //set the look direction based on the accerometer input
        //  look direction is independent of movement calculations in FixedUpdate
        //  helps to determine 
        protected virtual void SetLookDirection()
        {
            Vector2 input_acceleration = GameController.Instance.Input_Acceleration;
            if (!Mathf.Approximately(input_acceleration.x, 0.0f) || !Mathf.Approximately(input_acceleration.y, 0.0f))
            {
                lookDirection = input_acceleration;
                lookDirection.Normalize();
            }

            animator.SetFloat("Look X", lookDirection.x);
            animator.SetFloat("Look Y", lookDirection.y);
            //animator.SetFloat("Speed", input_acceleration.magnitude);
            
        }
        #endregion
    }
}