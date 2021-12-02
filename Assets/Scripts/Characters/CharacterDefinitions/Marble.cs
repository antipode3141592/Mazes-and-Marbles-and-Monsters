using System.Collections;
using UnityEngine;

namespace MarblesAndMonsters.Characters
{
    /// <summary>
    /// The basic monster unit, a Marble is small object that reacts to board movement,
    ///     deals damage to damagable objects that it collides with, and is immune to all damage.
    ///     They are not immune to falling or other instant kill effects
    /// </summary>
    public class Marble : CharacterControl
    {
        private MeleeController meleeController;
        protected override void Awake()
        {
            base.Awake();
            meleeController = GetComponent<MeleeController>();
        }
        //mables apply a touch attack to everything they collide with, no need for state controller or cooldowns

        private void OnCollisionEnter2D(Collision2D collision)
        {
            audioSource.clip = MySheet.baseStats.ClipHit;
            audioSource.Play(); //no matter what is struck, play the hit sound

            if (collision.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable))
            {
                meleeController.DealDamageTo(damagable);
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable))
            {
                meleeController.DealDamageTo(damagable);
            }
        }
    }
}
