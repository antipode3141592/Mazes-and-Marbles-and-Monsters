using UnityEngine;

namespace MarblesAndMonsters.Characters
{
    public class Marble : CharacterSheetController<Marble>
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            
            CharacterSheetController otherController = other.gameObject.GetComponent<CharacterSheetController>();
            if (otherController)
            {
                Debug.Log(string.Format("{0} touch attacks {1} for {2} {3} damage!",
                gameObject.name, other.gameObject.name, mySheet.Strength + mySheet.TouchAttack.DamageModifier, mySheet.TouchAttack.DamageType));
                otherController.TakeDamage(mySheet.Strength + mySheet.TouchAttack.DamageModifier, mySheet.TouchAttack.DamageType);
            }
            
        }
    }
}
