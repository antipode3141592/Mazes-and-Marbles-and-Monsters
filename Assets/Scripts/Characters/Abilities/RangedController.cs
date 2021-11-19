using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters
{
    public class RangedController : CombatControl<RangedController>
    {
        public Transform CurrentTarget;
        [SerializeField]
        protected ProjectileStats projectileStats;

        public override int TryAttack()
        {
            if (AttackAvailable)
            { 
                if (CurrentTarget == null)
                {
                    Debug.Log("No Target");
                    if (GetNearestEnemyWithLineOfSight(out Transform enemy))
                    {
                        CurrentTarget = enemy;
                        Debug.Log($"{CurrentTarget.name} is current target");
                    }
                } else
                {
                    if (CurrentTarget != null && HasLineOfSight(CurrentTarget, out Vector2 direction))
                    {
                        StartCoroutine(FireProjectile(0.1f, direction, projectileStats.Speed));
                        StartCoroutine(AttackCooldown(AttackStats.Cooldown));
                        return 1;
                    }
                }
            }
            //if line of site to nearest target, Fire projectile, return 1
            return 0;
        }

        

        IEnumerator FireProjectile(float attackDelay, Vector2 direction, float speed)
        {
            Debug.Log($"Fire at {(speed * direction).ToString()}");
            //start attack animation

            //yield return new WaitForSeconds(attackDelay);
            yield return null;
            GameObject _projectileGameObject = Instantiate(projectileStats.projectilePrefab, transform.position, Quaternion.identity);
            Projectile projectile = _projectileGameObject.GetComponent<Projectile>();
            projectile.Caster = gameObject;
            projectile.Rigidbody2D.velocity = speed * direction;
        }
    }

}