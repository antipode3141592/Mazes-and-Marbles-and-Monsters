using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters;
using MarblesAndMonsters.Characters;

namespace MarblesAndMonsters.Actions
{
    public class FollowPlayer : Action<FollowPlayer>
    {
        [SerializeField]
        private float speedModifier = 0.5f;

        //private ActionController _actionController;
        
        //private Player playerController;

        //protected override void Awake()
        //{
        //    base.Awake();
        //    playerController = GameObject.FindObjectOfType<Player>();
        //    _actionController = gameObject.GetComponent<ActionController>();
        //}

        public override void Move(Vector2 force, float forceMultiplier)
        {
            //base.Move(force, forceMultiplier);  //apply normal board action (accelerate based on tilt)
            //if (PlayerExit.in.isAwake)
            //{
            //    base.Move(force, forceMultiplier);  //apply normal board action (accelerate based on tilt)
            //    Vector2 directionToPlayer = playerController.transform.position - gameObject.transform.position;
            //    directionToPlayer = directionToPlayer / directionToPlayer.magnitude; //normalize
            //    _rigidbody.AddForce(directionToPlayer * speedModifier * forceMultiplier * _rigidbody.mass);
            //}
        }
    }
}