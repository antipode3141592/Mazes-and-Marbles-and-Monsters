using MarblesAndMonsters.Characters;
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

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (CollisionFunction(collision))
                Destroy(gameObject);
        }

        private void Update()
        {
            transform.Translate((Vector3)Direction * Time.deltaTime * ProjectileStats.Speed);
        }

        internal abstract bool CollisionFunction(Collider2D collision);

        internal void SetDirection(Vector2 direction)
        {
            Direction = (Vector3)direction;
        }
    }
}