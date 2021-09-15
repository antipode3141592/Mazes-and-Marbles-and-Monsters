using MarblesAndMonsters.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.Spells
{
    /// <summary>
    /// Projectile that creates an EntangleObject when it reaches its target or apply 
    /// </summary>
    public class EntangleProjectile : Projectile
    {
        public EntangleObject entanglePrefab;
        protected EntangleObject _entangleObject;
        

        //if it collides with anything, create a EntangleObject
        //  this projectile exists on the Projectile layer, which only collides with NPC, PC, and Walls layers
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.isTrigger && collision.gameObject != Caster.gameObject)
            {
                Debug.Log(string.Format("{0} has struck {1}", name, collision.name));
                _entangleObject = Instantiate<EntangleObject>(entanglePrefab, transform.position, Quaternion.identity);
                _entangleObject.SetCaster(Caster);
                _entangleObject.SetDeathTime(EffectDuration);
                //Destroy(gameObject);
                gameObject.SetActive(false);
            }
        }
    }
}