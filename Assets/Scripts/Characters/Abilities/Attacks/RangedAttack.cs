using UnityEngine;

namespace MarblesAndMonsters.Actions
{
    [CreateAssetMenu(fileName = "Ranged", menuName = "Attacks/Ranged")]
    public class RangedAttack : Attack
    {
        [SerializeField]
        protected float projectileSpeed;
        [SerializeField]
        protected Projectile projectilePrefab;

        public float ProjectileSpeed => projectileSpeed;
        public Projectile ProjectilePrefab => projectilePrefab;
    }
}