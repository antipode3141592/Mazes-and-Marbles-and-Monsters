using MarblesAndMonsters.Characters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters
{
    public class CombatControls : MonoBehaviour
    {
        //attack stuff
        [SerializeField]
        public Collider2D meleeCollider; //trigger collider for melee attack range
        [SerializeField]
        public Collider2D rangedCollider; //trigger collider for ranged attack range
        [SerializeField]
        protected float MeleeAttackRestPeriod = 1.0f;
        public bool MeleeAttackIsAvailable = true;
        
        [SerializeField]
        protected float ProjectileRestPeriod = 1.0f;
        public bool RangedAttackIsAvailable = true;
        
        //filters set in the editor
        [SerializeField] public ContactFilter2D targetDetectFilter;    //for detection of target object
        [SerializeField] public ContactFilter2D aimingFilter; //for gauging line of site to target

        //events
        public event EventHandler OnRangedAttackAvailable;
        public event EventHandler OnRangedAttackCooldown;
        public event EventHandler OnMeleeAttackAvailable;
        public event EventHandler OnMeleeAttackCooldown;

        private CharacterControl character;
        private void Awake()
        {
            character = GetComponent<CharacterControl>();
        }

        public virtual void RangedAttack(Vector2? direction = null)
        {
            if (RangedAttackIsAvailable)
            {
                if (direction.HasValue)
                {
                    Debug.Log($"{name} is preparing to fire a {character.MySheet.baseStats.RangedAttack.ProjectilePrefab.name} at {direction.Value}");
                    RangedAttackIsAvailable = false;
                    StartCoroutine(FireProjectile(0.15f, direction.Value, character.MySheet.baseStats.RangedAttack.ProjectileSpeed));
                    StartCoroutine(ProjectileCooldown());
                }
            }
        }

        IEnumerator FireProjectile(float attackDelay, Vector2 direction, float speed)
        {
            Debug.Log($"Fire at {(speed*direction).ToString()}");
            //start attack animation

            //yield return new WaitForSeconds(attackDelay);
            yield return null;
            Projectile projectile = SpawnProjectile();
            if (projectile)
            {
                projectile.Rigidbody2D.velocity = speed * direction;
            }
        }

        protected virtual Projectile SpawnProjectile()
        {
            Projectile projectile = Instantiate(character.MySheet.baseStats.RangedAttack.ProjectilePrefab, transform.position, Quaternion.identity);
            projectile.Caster = character;
            return projectile;
        }

        protected IEnumerator ProjectileCooldown()
        {
            yield return new WaitForSeconds(ProjectileRestPeriod);
            RangedAttackIsAvailable = true;
        }

        /// <summary>
        /// If Melee attack is available, deal damage to the damagable object and start melee cooldown
        /// </summary>
        /// <param name="damagable"></param>
        public virtual void MeleeAttack(IDamagable damagable)
        {
            if (MeleeAttackIsAvailable)
            {
                if (damagable != null)
                {
                    damagable.TakeDamage(character.MySheet.baseStats.MeleeAttack.DamageModifier, character.MySheet.baseStats.MeleeAttack.DamageType);
                    StartCoroutine(MeleeAttackCooldown());
                }
            }
        }


        protected IEnumerator MeleeAttackCooldown()
        {
            yield return new WaitForSeconds(MeleeAttackRestPeriod);
            MeleeAttackIsAvailable = true;
        }
    }

    //public abstract class CombatControl<T>
    //{

    //}

    //public class MeleeCombat : CombatControl<MeleeCombat>
    //{

    //}

    //public class RangedCombat: CombatControl<RangedCombat>
    //{

    //}
}