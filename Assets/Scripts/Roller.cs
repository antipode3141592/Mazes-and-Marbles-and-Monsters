using UnityEngine;

namespace MarblesAndMonsters.Characters
{
    public class Roller : CharacterSheetController<Roller>
    {
        //rollers only apply touch attack damage when their triggers are entered 
        //(so they may be safely touched on the side, which is much less squish-inducing)
        private void OnTriggerEnter2D(Collider2D other)
        {

            CharacterSheetController otherController = other.gameObject.GetComponent<CharacterSheetController>();
            if (otherController)
            {
                //Debug.Log(string.Format("{0} touch attacks {1} for {2} {3} damage!",
                //gameObject.name, other.gameObject.name, mySheet.Strength + mySheet.TouchAttack.DamageModifier, mySheet.TouchAttack.DamageType));
                otherController.TakeDamage(mySheet.Strength + mySheet.TouchAttack.DamageModifier, mySheet.TouchAttack.DamageType);
            }

        }
    }
}
