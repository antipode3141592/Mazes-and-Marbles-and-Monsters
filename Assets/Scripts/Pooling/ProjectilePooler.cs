using UnityEngine;

namespace MarblesAndMonsters.Pooling
{
    public class ProjectilePooler : Pooler<Projectile>
    {
        [SerializeField] Projectile _projectilePrefab;

        private void Start()
        {
            InitPool(_projectilePrefab, initial: 3, max: 6, collectionChecks: true) ; // Initialize the pool
            Debug.Log($"{_projectilePrefab.name}'s pool has been initialized");
        }

        protected override void OnTakeFromPool(Projectile projectile)
        {
            Debug.Log($"{projectile.name} taken from pool!");
            base.OnTakeFromPool(projectile);
            projectile.Direction = Vector3.zero;
            projectile.Pooler = this;
        }

        protected override void OnReturnedToPool(Projectile projectile)
        {
            Debug.Log($"{projectile.name} returned to pool!");
            base.OnReturnedToPool(projectile);
            projectile.CasterGuid = System.Guid.Empty;
            projectile.CasterTag = string.Empty;
        }
    }
}