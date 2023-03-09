using UnityEngine;

namespace MarblesAndMonsters.Characters
{
    /// <summary>
    /// Rollers simply react to the board movement and damage things that stay in its triggers
    /// </summary>
    public class Roller : CharacterControl
    {
        MeleeController meleeController;
        protected override void Awake()
        {
            base.Awake();
            meleeController = GetComponent<MeleeController>();
        }

        //rollers only apply touch attack damage when their triggers are entered 
        //(so they may be safely touched on the side, which is much less squish-inducing)
        void OnTriggerStay2D(Collider2D other)
        {
            if(other.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable))
            {
                meleeController.DealDamageTo(damagable);
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable))
            {
                meleeController.DealDamageTo(damagable);
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            collisionIntensity = Mathf.Clamp01(collision.relativeVelocity.magnitude * 0.1f);   // divide by 10, clamped to [0,1]
            collisionEffects.PlayFeedbacks(transform.position, collisionIntensity);
        }
    }
}
