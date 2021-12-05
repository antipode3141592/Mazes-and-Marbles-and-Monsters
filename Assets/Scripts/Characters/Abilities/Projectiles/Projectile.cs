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
        public Vector2 Direction = new Vector2(0f, 0f);
        public ProjectilePooler Pooler;
        Rigidbody2D rigidBody;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }

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
            rigidBody.MovePosition(rigidBody.position + Direction * Time.deltaTime * ProjectileStats.Speed);
            //transform.Translate((Vector3)Direction * Time.deltaTime * ProjectileStats.Speed);
        }

        internal abstract void CollisionFunction(Collision2D collision);

        internal void SetDirection(Vector2 direction)
        {
            Direction = direction;
        }
    }
}