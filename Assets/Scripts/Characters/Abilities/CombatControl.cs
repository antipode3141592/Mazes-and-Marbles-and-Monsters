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

        [SerializeField]
        public AttackStats AttackStats;
        [SerializeField]
        protected Collider2D _attackCollider;
        [SerializeField]
        protected ContactFilter2D _detectionFilter;
        [SerializeField]
        protected ContactFilter2D _lineOfSightFilter;

        protected List<GameObject> _enemies;

        protected List<Collider2D> _collisionCheckResults;
        private List<RaycastHit2D> _hits;

        protected CharacterManager _characterManager;

        public string EnemyTag()
        {
            if (CompareTag("Monster"))
            {
                return "Player";
            } else
            {
                return "Monster";
            }
        }



        protected virtual void Awake()
        {
            _collisionCheckResults = new List<Collider2D>();
            _hits = new List<RaycastHit2D>();
            _enemies = new List<GameObject>();
            _characterManager = FindObjectOfType<CharacterManager>();
        }

        protected virtual void Start()
        {

        }

        public bool FindEnemies(out List<GameObject> enemies)
        {
            enemies = new List<GameObject>();
            foreach (var character in _characterManager.Characters)
            {
                if (character.CompareTag(EnemyTag()))
                {
                    enemies.Add(character.gameObject);
                }
            }
            if (enemies.Count>0) {
                return true;
            }
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
                    if (collider.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable))
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

        public virtual bool GetNearestEnemy(out Transform enemyTransform)
        {
            enemyTransform = null;
            _collisionCheckResults.Clear();
            if (_attackCollider.OverlapCollider(_detectionFilter, _collisionCheckResults) > 0)
            {
                enemyTransform = FindNearestCollision(_collisionCheckResults, origin:transform);
                //List<Transform> transforms = _collisionCheckResults.Find(x => x.transform)
                //enemyTransform = FindNearestCollision(resultsList: );
                return true;
            }

                return false;
        }

        public virtual bool GetNearestEnemyWithLineOfSight(out Transform enemyTransform)
        {
            enemyTransform = null;
            _collisionCheckResults.Clear();
            if (_attackCollider.OverlapCollider(_detectionFilter, _collisionCheckResults) > 0)
            {
                Debug.Log($"{gameObject.name} has found {_collisionCheckResults.Count} objects in collider");
                enemyTransform = FindNearestTransformWithLineOfSight(_collisionCheckResults, transform);
            }
                //    {
                //        var tagCheck = collisionCheckResults.Find(x => x.CompareTag("Player"));
                //        if (tagCheck == null)
                //            return typeof(Aiming);
                //        aiMover.TargetTransform = tagCheck.transform;
                //        Debug.Log($"{_gameObject.name} is targetting {tagCheck.transform.name}");
                //        Vector2 origin = _transform.position;
                //        Vector2 direction = (tagCheck.transform.position - _transform.position);
                //        float distance = direction.magnitude;
                //        //contact filter limits to NPC and Wall layers
                //        int results = Physics2D.Raycast(origin, direction.normalized, _character.Combat.aimingFilter, hits, distance);
                //        if (results <= 1)
                //        {
                //            //hits[0].
                //            _character.Combat.RangedAttack(direction.normalized);
                //            shotsFired++;
            if (enemyTransform != null)
            {
                return true;
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
            //Debug.Log($"there are {results}  hits");
            //foreach (RaycastHit2D hit in _hits)
            //{
            //    Debug.Log($"{hit.transform.name} is along the raycast line");

            //}
            if (results <= 2)   //self and target
            {
                return true;
            }
            return false;
        }

        protected Transform FindNearestCollision(List<Collider2D> resultsList, Transform origin)
        {
            //there's definitely a better way to implement this
            float smallestDistance = 10000f;
            int smallestIndex = 0;
            for (int i = 0; i < resultsList.Count; i++)
            {
                float currentDistance = (resultsList[i].transform.position - origin.position).magnitude;
                if (currentDistance < smallestDistance)
                {
                    smallestDistance = currentDistance;
                    smallestIndex = i;
                }
            }
            return resultsList[smallestIndex].transform;
        }

        protected Transform FindNearestTransformWithLineOfSight(List<Collider2D> resultsList, Transform origin)
        {
            //there's definitely a better way to implement this
            float smallestDistance = 10000f;
            int smallestIndex = -1;
            for (int i = 0; i < resultsList.Count; i++)
            {
                
                if (resultsList[i].CompareTag(EnemyTag()))
                {
                    float currentDistance = (resultsList[i].transform.position - origin.position).magnitude;
                    if (currentDistance < smallestDistance)
                    {
                        if (HasLineOfSight(resultsList[i].transform, out Vector2 direction))
                        {
                            smallestDistance = currentDistance;
                            smallestIndex = i;

                        }
                    }
                    Debug.Log($"{resultsList[i].name} has tag {resultsList[i].tag} and is {currentDistance} units away");
                }
            }
            if (smallestIndex >= 0)
            {
                return resultsList[smallestIndex].transform;
            } else
            {
                return null;
            }
        }
    }

}