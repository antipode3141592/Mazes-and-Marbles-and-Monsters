using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MarblesAndMonsters
{

    //
    public abstract class BoardMovable<T>: BoardMovable where T : BoardMovable<T>
    {
        //all board movable objects will receive a force vector to move
        public virtual void Move(Vector2 force, float forceMultiplier)
        {
            Rigidbody2D rigidbody2D = this.GetComponent<Rigidbody2D>();
            rigidbody2D.AddForce(force * rigidbody2D.mass * forceMultiplier);
        }
    }

    //board movable objects are all attached to unity game objects, so should inherit from MonoBehavior
    public abstract class BoardMovable: MonoBehaviour
    {

    }
}
