using UnityEngine;

namespace MarblesAndMonsters.Characters
{
    public class Marble : CharacterSheetController
    {
        //mables apply a touch attack to everything they collide with
        private void OnCollisionEnter2D(Collision2D other)
        {
            IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
            if (damagable != null) 
            { 
                damagable.TakeDamage(mySheet.Strength + mySheet.TouchAttack.DamageModifier, mySheet.TouchAttack.DamageType); 
            }

        }
    }
}
