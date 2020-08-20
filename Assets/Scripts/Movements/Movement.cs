using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters
{


    public abstract class Movement<T> : Movement where T : Movement<T>
    {

    }

    public abstract class Movement : MonoBehaviour
    {
        protected Rigidbody2D _rigidbody;

        [SerializeField]
        protected float _forceMultiplier = 9.81f; //default to 1g

        protected virtual void Awake()
        {
            _rigidbody = this.GetComponent<Rigidbody2D>();
        }
        public virtual void Move()
        {
            _rigidbody.AddForce((Vector2)Input.acceleration * _rigidbody.mass * _forceMultiplier);
        }
    }
}
