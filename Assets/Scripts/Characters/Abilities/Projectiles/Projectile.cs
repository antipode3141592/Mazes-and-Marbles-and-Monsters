using MarblesAndMonsters.Characters;
using MarblesAndMonsters.Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace MarblesAndMonsters
{
    public abstract class Projectile : MonoBehaviour
    {
        public ProjectileStats ProjectileStats;
        public GameObject Caster;
        public Vector3 Direction = new Vector3(0f, 0f, 0f);
        public ProjectilePooler Pooler;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject != Caster)
            {
                CollisionFunction(collision);
                Pooler.Release(this);
            }
        }

        private void Update()
        {
            transform.Translate((Vector3)Direction * Time.deltaTime * ProjectileStats.Speed);
        }

        internal abstract void CollisionFunction(Collision2D collision);

        internal void SetDirection(Vector2 direction)
        {
            Direction = (Vector3)direction;
        }
    }
}