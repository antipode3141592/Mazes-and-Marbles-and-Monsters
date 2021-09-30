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

        internal override bool CollisionFunction(Collider2D collision)
        {
            if (!collision.isTrigger && collision.gameObject != Caster.gameObject)
            {
                Debug.Log(string.Format("{0} has struck {1}", name, collision.name));
                var _entangleObject = Instantiate<EntangleObject>(entanglePrefab, transform.position, Quaternion.identity);
                _entangleObject.SetCaster(Caster);
                _entangleObject.SetDeathTime(ProjectileStats.EffectDuration);
                return true;
            }
            return false;
        }
    }
}