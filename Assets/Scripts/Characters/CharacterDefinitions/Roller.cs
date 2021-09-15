using UnityEngine;

namespace MarblesAndMonsters.Characters
{
    /// <summary>
    /// Rollers simply react to the board movement and damage things that stay in its triggers
    /// </summary>
    public class Roller : CharacterControl
    {
        //rollers only apply touch attack damage when their triggers are entered 
        //(so they may be safely touched on the side, which is much less squish-inducing)
        private void OnTriggerStay2D(Collider2D other)
        {
            if(other.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable))
            {
                Combat.MeleeAttack(damagable);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable))
            {
                Combat.MeleeAttack(damagable);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            audioSource.clip = MySheet.baseStats.ClipHit;
            audioSource.Play(); //no matter what is struck, play the hit sound
        }
    }
}
