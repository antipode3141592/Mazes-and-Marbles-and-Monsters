using MarblesAndMonsters.Pooling;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters
{
    public class RangedController : CombatControl<RangedController>
    {
        public Transform CurrentTarget;
        [SerializeField] protected ProjectileStats projectileStats;
        public ProjectilePooler ProjectilePooler;

        protected override void Awake()
        {
            base.Awake();
        }

        public override int TryAttack()
        {
            if (AttackAvailable)
            { 
                if (CurrentTarget == null)
                {
                    Debug.Log("No Target", gameObject);
                    if (FindNearestEnemyInLineOfSight(out var enemy))
                    {
                        CurrentTarget = enemy.transform;
                        Debug.Log($"{CurrentTarget.name} is current target of {name}", gameObject);
                    }
                } else
                {
                    if (HasLineOfSight(CurrentTarget, out Vector2 direction))
                    {
                        AttackAvailable = false;
                        StartCoroutine(FireProjectile(0.1f, direction.normalized));
                        StartCoroutine(AttackCooldown(AttackStats.Cooldown));
                        return 1;
                    }
                }
            }
            //if line of site to nearest target, Fire projectile, return 1
            return 0;
        }

        IEnumerator FireProjectile(float attackDelay, Vector2 direction)
        {
            //start attack animation
            //GameObject _projectileGameObject = Instantiate(projectileStats.projectilePrefab, transform.position, Quaternion.identity);
            //Projectile projectile = _projectileGameObject.GetComponent<Projectile>();
            //projectile.Caster = gameObject;   

            yield return new WaitForSeconds(attackDelay);
            Projectile projectile = ProjectilePooler.Get();
            projectile.transform.position = transform.position;
            projectile.CasterGuid = _characterControl.Guid;
            projectile.CasterTag = _characterControl.gameObject.tag;
            projectile.tag = _characterControl.gameObject.tag;
            projectile.SetDirection(direction);
        }
    }

}