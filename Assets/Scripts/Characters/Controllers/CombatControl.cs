using MarblesAndMonsters.Characters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MarblesAndMonsters
{
    public abstract class CombatControl<T> : MonoBehaviour, ICombatControl
    {
        public bool AttackAvailable;
        public EventHandler OnAttackAvailable;

        [SerializeField] public AttackStats AttackStats;
        [SerializeField] protected Collider2D _attackCollider;
        [SerializeField] protected ContactFilter2D _detectionFilter;
        [SerializeField] protected ContactFilter2D _lineOfSightFilter;

        protected List<GameObject> _enemies;
        protected List<Collider2D> _collisionCheckResults;
        List<RaycastHit2D> _hits;
        protected CharacterManager _characterManager;
        protected CharacterControl _characterControl;

        public string EnemyTag()
        {
            if (CompareTag("Monster"))
                return "Player";
            else
                return "Monster";
        }

        protected virtual void Awake()
        {
            _collisionCheckResults = new List<Collider2D>();
            _hits = new List<RaycastHit2D>();
            _enemies = new List<GameObject>();
            _characterControl = GetComponent<CharacterControl>();
            _characterManager = FindObjectOfType<CharacterManager>();
        }

        public bool FindEnemies(out List<GameObject> enemies)
        {
            enemies = new List<GameObject>();
            foreach (var character in _characterManager.Characters.FindAll(x => x != null))
            {
                if (character.isActiveAndEnabled && !character.MySheet.IsStealth && character.CompareTag(EnemyTag()))
                    enemies.Add(character.gameObject);
            }
            if (enemies.Count>0) {
                return true;
            }
            return false;
        }

        public bool FindNearestEnemyInLineOfSight(out GameObject enemyInLineOfSight)
        {
            if (FindEnemies(out _enemies))
            {
                var SortedByDistance = _enemies.OrderBy(x => x.transform.position - transform.position);
                foreach (var a in SortedByDistance)
                {
                    if (HasLineOfSight(a.transform, out var direction))
                    {
                        enemyInLineOfSight = a;
                        return true;
                    }
                }
            }
            enemyInLineOfSight = null;
            return false;
        }

        public virtual void DealDamageTo(IDamagable damagableObject)
        {
            damagableObject.TakeDamage(AttackStats.DamageModifier, AttackStats.DamageType);
        }

        public abstract int TryAttack();

        public virtual IEnumerator AttackCooldown(float restPeriod)
        {
            yield return new WaitForSeconds(restPeriod);
            AttackAvailable = true;
            OnAttackAvailable?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="damagables">list of objects within collider which implement IDamagable</param>
        /// <returns>true if any damagables in range, else false</returns>
        public virtual bool GetDamagablesInRange(out List<IDamagable> damagables)
        {
            _collisionCheckResults.Clear();
            damagables = new List<IDamagable>();
            if (_attackCollider.OverlapCollider(_detectionFilter, _collisionCheckResults) > 0)
            {
                foreach (Collider2D collider in _collisionCheckResults)
                {
                    if (!collider.gameObject.CompareTag(tag) && collider.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable))
                    {
                        damagables.Add(damagable);
                    }
                }
                if (damagables.Count > 0)
                {
                    return true;
                } 
            }
            return false;
        }

        public bool HasLineOfSight(Transform target, out Vector2 direction)
        {

            Vector2 origin = transform.position;
            direction = target.position - transform.position;
            float distance = direction.magnitude;
            _hits.Clear();
            int results = Physics2D.Raycast(origin, direction.normalized, _lineOfSightFilter, _hits, distance);
            if (results <= 2)   //self and target
            {
                return true;
            }
            return false;
        }
    }

}