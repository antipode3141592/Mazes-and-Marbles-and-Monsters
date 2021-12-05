using MarblesAndMonsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MarblesAndMonsters.Projectiles
{
    public class ArrowProjectile : Projectile
    {
        internal override void CollisionFunction(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable))
            {
                damagable.TakeDamage(ProjectileStats.Damage, ProjectileStats.DamageType);
            }
        }
    }
}
