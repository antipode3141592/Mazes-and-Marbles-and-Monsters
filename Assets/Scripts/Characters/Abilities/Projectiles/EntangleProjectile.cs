using MarblesAndMonsters.Characters;
using MarblesAndMonsters.Spells;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.Projectiles
{
    /// <summary>
    /// Projectile that creates an EntangleObject when it reaches its target or apply Mar
    /// </summary>
    public class EntangleProjectile : Projectile
    {
        public EntangleObject entanglePrefab;

        internal override void CollisionFunction(Collision2D collision)
        {
            Debug.Log(string.Format("{0} has struck {1}", name, collision.gameObject.name));
            var _entangleObject = Instantiate<EntangleObject>(entanglePrefab, transform.position, Quaternion.identity);
            _entangleObject.tag = CasterTag;
            _entangleObject.CasterGuid = CasterGuid;
            _entangleObject.SetDeathTime(ProjectileStats.EffectDuration);
        }
    }
}