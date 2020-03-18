using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters;

namespace MarblesAndMonsters
{
    public class MoveTowardPlayer : BoardMovable<MoveTowardPlayer>
    {
        [SerializeField]
        private float speedModifier = 0.5f;
        [SerializeField]
        protected bool isAwake = true; 
        private Player playerController;


        

        protected override void Awake()
        {
            base.Awake();
            playerController = GameObject.FindObjectOfType<Player>();
        }

        public override void Move(Vector2 force, float forceMultiplier)
        {
            base.Move(force, forceMultiplier);
            if (isAwake)
            {
                Vector2 directionToPlayer = playerController.transform.position - gameObject.transform.position;
                directionToPlayer = directionToPlayer / directionToPlayer.magnitude; //normalize
                rigidbody2D.AddForce(directionToPlayer * speedModifier * forceMultiplier * rigidbody2D.mass);
            }
        }
    }
}