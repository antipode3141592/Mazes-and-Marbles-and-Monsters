using MarblesAndMonsters.Characters;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace MarblesAndMonsters
{
    public abstract class Projectile : MonoBehaviour
    {
        public ProjectileStats ProjectileStats;
        public Rigidbody2D Rigidbody2D;
        public GameObject Caster;

        protected void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (CollisionFunction(collision))
                Destroy(gameObject);
        }

        internal abstract bool CollisionFunction(Collider2D collision);
    }
}