using UnityEngine;

namespace MarblesAndMonsters.Actions
{
    public class FollowPlayer : Movement
    {
        [SerializeField]
        private float speedModifier = 0.5f;

        public override void Move()
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