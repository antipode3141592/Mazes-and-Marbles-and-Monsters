using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters;
using MarblesAndMonsters.Characters;

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
        public CharacterSheet MySheet => mySheet; //read-only accessor for accessing stats directly (for hp, attack/def values, etc)

        protected virtual void Awake()
        {
            //grab CharacterSheet reference
            mySheet = this.GetComponent<CharacterSheet>();
        }

        protected virtual void Update()
        {
            //default Update action

            //state checks:
            //  fire - if firetoken.count > 0, take 1 damage and remove fire token
            //  poison - if poison
            //  death - after all other state checks, check for death

            //movement
            
            if (mySheet.Movements.Count > 0)
            {
                foreach (Movement movement in mySheet.Movements)
                {
                    movement.Move();
                }
            }
            
        }

        //adjust health
        public void AdjustHealth(int value)
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

                    //ad health particle effect
                    healEffect.Play();
                } else
                {
                    //no effect
                    
                }

            } else
            {
                //subtract health;
                //death check;
                //damage animation;

            }
        }

        public bool CharacterDeath()
        {
            //check for Player

            //death animation
            //respawn check
            
            //trigger death on controller
            GameController.Instance.EndLevel(false);
            return false;
        }
    }

    public abstract class CharacterSheetController: MonoBehaviour
    {

    }
}