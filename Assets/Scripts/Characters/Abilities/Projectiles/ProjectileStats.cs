using UnityEngine;

namespace MarblesAndMonsters
{
    [CreateAssetMenu(menuName = "Stats/Projectile Stats")]
    public class ProjectileStats : ScriptableObject
    {
        public GameObject projectilePrefab;
        public float Speed = 5f;
        public float EffectDuration;
        public int Damage;
        public DamageType DamageType;
    }
}