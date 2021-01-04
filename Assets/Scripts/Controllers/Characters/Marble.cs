using System.Collections;
using UnityEngine;

namespace MarblesAndMonsters.Characters
{
    public class Marble : CharacterSheetController
    {
        //mables apply a touch attack to everything they collide with
        private void OnCollisionEnter2D(Collision2D other)
        {
            audioSource.Play(); //no matter what is struck, play the hit sound

            IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
            if (damagable != null) 
            { 
                damagable.TakeDamage(mySheet.Strength + mySheet.TouchAttack.DamageModifier, mySheet.TouchAttack.DamageType);   
            }
        }

        protected override IEnumerator DeathAnimation(DeathType deathType)
        {
            Debug.Log(string.Format("DeathAnimation {0} has died of {1}!", gameObject.name, deathType.ToString()));
            yield return new WaitForSeconds(0.5f);  //death animations are 8 frames, current fps is 12
            gameObject.SetActive(false);
        }
    }
}
