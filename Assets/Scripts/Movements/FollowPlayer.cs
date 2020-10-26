using UnityEngine;
using MarblesAndMonsters.Characters;

namespace MarblesAndMonsters.Actions
{
    //a simple movement that pushes the character toward the player
    //  no obstacle avoidance or smarts of any kind
    public class FollowPlayer : Movement
    {
        [SerializeField]
        private float speedModifier = 0.5f;

        public override void Move()
        {
            Vector2 directionToPlayer = Player.Instance.transform.position - gameObject.transform.position;
            directionToPlayer = directionToPlayer / directionToPlayer.magnitude; //normalize
            _rigidbody.AddForce(directionToPlayer * speedModifier * 9.81f * _rigidbody.mass);
        }
    }
}