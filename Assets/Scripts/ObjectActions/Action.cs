using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters
{

    //
    public abstract class Action<T> : Action where T : Action<T>
    {
        protected Rigidbody2D _rigidbody;


        

        protected virtual void Awake()
        {
            _rigidbody = this.GetComponent<Rigidbody2D>();
        }

        //all board movable objects will receive a force vector to move
        public virtual void Move(Vector2 force, float forceMultiplier)
        {
            //Rigidbody2D rigidbody2D = this.GetComponent<Rigidbody2D>();
            _rigidbody.AddForce(force * _rigidbody.mass * forceMultiplier);
        }
    }

    //board movable objects are all attached to unity game objects, so should inherit from MonoBehavior
    public abstract class Action : MonoBehaviour
    {

    }
}
