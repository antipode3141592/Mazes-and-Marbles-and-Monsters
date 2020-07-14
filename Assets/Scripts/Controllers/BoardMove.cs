using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters
{

    //  default board movement action, applies force during Update proportional to device tilt vector
    //      bool    _moving     whether or not the board moves the object (can still be pushed)
    //      float   _forceMultiplier    the default acceleration (m/s^2)
    public class BoardMove: Movement<BoardMove>
    {
        
        protected bool _moving = true;  //default to moving state

    

        public bool Moving { get { return _moving; } set { _moving = value; } }
        

        //protected virtual void Awake()
        //{
        //    _rigidbody = this.GetComponent<Rigidbody2D>();
        //}

        //protected virtual void Update()
        //{
        //    if (gameObject.activeInHierarchy && _moving)
        //    {
        //        //get controller input
        //        Vector2 boardVector = new Vector2(Input.acceleration.x, Input.acceleration.y);
        //        Move(boardVector, _forceMultiplier);
        //    }
        //}

        //all board movable objects will receive a force vector to move
        public override void Move()
        {
            //Rigidbody2D rigidbody2D = this.GetComponent<Rigidbody2D>();
            if (gameObject.activeInHierarchy && _moving)
            {
                base.Move();
                //get controller input
                //Vector2 boardVector = new Vector2(Input.acceleration.x, Input.acceleration.y);
            }
            
        }
    }


    public abstract class Movement<T>: Movement where T : Movement<T>
    {
        
    }

    public abstract class Movement: MonoBehaviour
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
