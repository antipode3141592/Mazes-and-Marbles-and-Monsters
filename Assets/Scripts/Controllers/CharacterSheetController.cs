using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Characters;

namespace MarblesAndMonsters.Characters
{
    //Controller for characters (MVC pattern)
    public abstract class CharacterSheetController<T> : CharacterSheetController where T : CharacterSheetController<T>
    {
        //[SerializeField]
        protected CharacterSheet mySheet;
        public CharacterSheet MySheet => mySheet;

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

            //move
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
                    //add health animation
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
            //death animation
            //respawn check
            return false;
        }
    }

    public abstract class CharacterSheetController: MonoBehaviour
    {

    }
}