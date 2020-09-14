using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.Characters
{
    //Controller for characters (MVC pattern)
    //  Executes actions based on the attached character sheet (Unity component)
    //  Core Loop:
    //      1) State Check (fire, poison, sleep, etc)
    //      2) Movement

    public abstract class CharacterSheetController<T> : CharacterSheetController where T : CharacterSheetController<T>
    {
        //particle effects
        public ParticleSystem hitEffect;    //plays when stuck/attacked/damaged
        public ParticleSystem healEffect;   //plays when healing (players use a potion, monster regenerates, etc.)

        //[SerializeField]
        protected CharacterSheet mySheet;
        protected List<Collider2D> myColliders;
        protected bool Respawn = false;
        

        public CharacterSheet MySheet => mySheet; //read-only accessor for accessing stats directly (for hp, attack/def values, etc)

        protected virtual void Awake()
        {
            //grab CharacterSheet reference
            mySheet = GetComponent<CharacterSheet>();
            myColliders = new List<Collider2D>(GetComponents<Collider2D>());
        }

        protected virtual void Start()
        {
            mySheet.CurrentHealth = mySheet.MaxHealth;
        }

        //saddest state machine
        protected virtual void Update()
        {
            //default Update action
            
            //state checks:
            //  invincible
            if (mySheet.IsInvincible)
            {
                mySheet.InvincibleTimeCounter -= Time.deltaTime;
                if (mySheet.InvincibleTimeCounter <= 0.0f)
                {
                    mySheet.IsInvincible = false;
                }
            }
            //  fire - if firetoken.count > 0, take 1 damage and remove fire token
            //  poison - if poison... "  "
            //  frozen - ... "   "
            //  death for 0 or less hp
            if (mySheet.CurrentHealth <= 0)
            {
                CharacterDeath();
            }


        }

        protected virtual void FixedUpdate()
        {
            //movements, if not asleep
            if (!mySheet.IsAsleep)
            {
                if (mySheet.Movements.Count > 0)
                {
                    foreach (Movement movement in mySheet.Movements)
                    {
                        movement.Move();
                    }
                }
            }
        }

        //protected virtual void OnDisable()
        //{
        //    if (Respawn)
        //    {
        //        StartCoroutine(RespawnCharacter());
        //    }
        //}

        

        public override void TakeDamage(int damageAmount, DamageType damageType)
        {
            //check for invincibility
            if (mySheet.IsInvincible)
            {
                Debug.Log(string.Format("{0} is invincible!", gameObject.name));
            }
            //check immunity to damage type
            else if (mySheet.DamageImmunities.Contains(damageType))
            {
                Debug.Log(string.Format("{0} is immune to {1} and takes no damage!", gameObject.name, damageType));
            }
            //check damage > armor
            else if (damageAmount <= mySheet.Armor)
            {
                Debug.Log(string.Format("{0}'s armor absorbs all damage!", gameObject.name));
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
                //play hit effect
                hitEffect.Play();
                //become invincible!
                ApplyInvincible();
            }
            //
        }

        public void ApplyInvincible()
        {
            mySheet.IsInvincible = true;
            mySheet.InvincibleTimeCounter = GameController.Instance.DefaultEffectTime;
        }

        internal override void ApplyFire()
        {
            //add 
        }

        internal override void ApplyPoison()
        {
            throw new System.NotImplementedException();
        }

        internal override void ApplyIce()
        {
            throw new System.NotImplementedException();
        }

        public override void HealDamage(int healAmount)
        {

        }

        //Adjust Health
        //  adds value to current health
        public virtual void AdjustHealth(int value)
        {
            //add health or subtract health?
            if (value >= 0)
            {
                //check for damage
                if (mySheet.CurrentHealth < mySheet.MaxHealth)
                {
                    //add health
                    mySheet.CurrentHealth += value;
                    //add health animation

                    //add health particle effect
                    healEffect.Play();
                    //update UI
                    //GameMenu.Instance.UpdateHealth()
                } else
                {
                    //no effect
                    
                }

            } else
            {
                //subtract health;
                mySheet.CurrentHealth -= value;
                //damage animation;

                //death check;
                if (MySheet.CurrentHealth <= 0) 
                {
                    CharacterDeath();
                }
                

            }
        }

        public override void CharacterSpawn()
        {
            gameObject.SetActive(true);
            gameObject.transform.position = mySheet.SpawnPoint.position;
            gameObject.transform.rotation = mySheet.SpawnPoint.rotation;
            foreach (Collider2D collider in myColliders)
            {
                collider.enabled = true;
            }
            mySheet.CurrentHealth = mySheet.MaxHealth;
        }

        public override void CharacterDeath()
        {
            //turn off colliders
            foreach (Collider2D collider in myColliders)
            {
                collider.enabled = false;
            }
            //death animation
            StartCoroutine(DeathAnimation());
        }

        public override void CharacterReset()
        {
            gameObject.SetActive(false);
        }

        //private IEnumerator RespawnCharacter()
        //{
        //    yield return new WaitForSeconds(mySheet.RespawnPeriod);
        //    CharacterSpawn();
        //}

        private IEnumerator DeathAnimation()
        {
            Debug.Log(string.Format("{0} has died!", gameObject.name));
            yield return new WaitForSeconds(0.5f);
            
            if (mySheet.RespawnFlag)
            {
                GameController.Instance.RespawnCharacter(this, mySheet.RespawnPeriod);
            }
            gameObject.SetActive(false);
        }
    }

    public abstract class CharacterSheetController: MonoBehaviour
    {
        public abstract void CharacterReset();
        public abstract void CharacterDeath();

        public abstract void CharacterSpawn();
        public abstract void TakeDamage(int damageAmount, DamageType damageType);
        public abstract void HealDamage(int healAmount);

        internal abstract void ApplyFire();
        internal abstract void ApplyPoison();
        internal abstract void ApplyIce();


    }
}