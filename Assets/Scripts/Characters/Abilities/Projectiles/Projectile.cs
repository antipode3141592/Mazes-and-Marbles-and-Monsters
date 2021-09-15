using MarblesAndMonsters.Characters;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace MarblesAndMonsters
{
    public abstract class Projectile : MonoBehaviour
    {
        public CharacterControl Caster;
        public float EffectDuration;
        public Rigidbody2D Rigidbody2D;
        [SerializeField]protected Vector2 _velocity;

        [Inject]
        public void Construct()
        {
            Reset(Vector2.zero);
        }

        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        void Reset(Vector2 velocity)
        {
            transform.position = Vector2.zero;
            _velocity = velocity;
        }

        public class Pool : MonoMemoryPool<Vector2, Projectile>
        {
            protected override void Reinitialize(Vector2 velocity, Projectile projectile)
            {
                projectile.Reset(velocity);
            }
        }
    }

    public class ProjectilePooler
    {
        readonly Projectile.Pool _projectilePooler;
        readonly List<Projectile> _projectiles = new List<Projectile>();

        public ProjectilePooler(Projectile.Pool projectilePooler)
        {
            _projectilePooler = projectilePooler;
        }

        public void AddProjectile()
        {
            float maxSpeed = 10.0f;
            float minSpeed = 1.0f;

            _projectiles.Add(_projectilePooler.Spawn(
                Random.onUnitSphere * Random.Range(minSpeed, maxSpeed)));
        }

        public void RemoveFoo()
        {
            var foo = _projectiles[0];
            _projectilePooler.Despawn(foo);
            _projectiles.Remove(foo);
        }
    }
}