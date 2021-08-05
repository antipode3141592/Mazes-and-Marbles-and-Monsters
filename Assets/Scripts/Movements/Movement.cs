using UnityEngine;
using MarblesAndMonsters.Characters;

namespace MarblesAndMonsters
{

    //  must be a component of an object with a character control (and therefor, a character sheet)
    public abstract class Movement : MonoBehaviour, IMovement
    {
        protected Rigidbody2D _rigidbody;
        protected CharacterControl characterControl;

        //all moveable objects will have a rigidbody
        void Awake()
        {
            _rigidbody = this.GetComponent<Rigidbody2D>();
            characterControl = GetComponentInParent<CharacterControl>();
        }

        //be sure to implement the IMovement interface
        public abstract void Move();
    }
}
