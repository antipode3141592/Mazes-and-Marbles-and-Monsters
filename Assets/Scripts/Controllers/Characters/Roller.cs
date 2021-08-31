using UnityEngine;

namespace MarblesAndMonsters.Characters
{
    public class Roller : CharacterControl
    {
        //rollers only apply touch attack damage when their triggers are entered 
        //(so they may be safely touched on the side, which is much less squish-inducing)
        private void OnTriggerStay2D(Collider2D other)
        {
            if (TouchAttackIsAvailable)
            {
                IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
                if (damagable != null)
                {
                    DealDamageTo(damagable);
                    StartCoroutine(TouchAttackCooldown());
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            audioSource.clip = MySheet.baseStats.ClipHit;
            audioSource.Play(); //no matter what is struck, play the hit sound
        }
    }
}
