using MarblesAndMonsters.Characters;
using MarblesAndMonsters.Pooling;
using System;
using UnityEngine;

namespace MarblesAndMonsters
{
    public abstract class Projectile : MonoBehaviour
    {
        public ProjectileStats ProjectileStats;
        public Guid CasterGuid;
        public string CasterTag;
        public Vector2 Direction = new Vector2(0f, 0f);
        public ProjectilePooler Pooler;
        Rigidbody2D rigidBody;

        void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            rigidBody.MovePosition(rigidBody.position + Direction * Time.deltaTime * ProjectileStats.Speed);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            var characterControl = collision.gameObject.GetComponent<CharacterControl>();
            if (characterControl is not null && characterControl.Guid == CasterGuid)
                return;
            CollisionFunction(collision);
            Pooler.Release(this);
        }

        internal abstract void CollisionFunction(Collision2D collision);

        internal void SetDirection(Vector2 direction)
        {
            Direction = direction;
        }
    }
}