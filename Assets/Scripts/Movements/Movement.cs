using UnityEngine;
using MarblesAndMonsters.Characters;

namespace MarblesAndMonsters
{
    public abstract class Movement : MonoBehaviour, IMovement
    {
        protected Rigidbody2D _rigidbody;

        //all moveable objects will have a rigidbody
        void Awake()
        {
            _rigidbody = this.GetComponent<Rigidbody2D>();
        }

        //be sure to implement the IMovement interface
        public abstract void Move();
    }
}
