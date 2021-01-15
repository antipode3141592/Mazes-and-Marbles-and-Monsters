using UnityEngine;

namespace MarblesAndMonsters.Characters
{
    public class Roller : CharacterControl
    {
        //rollers only apply touch attack damage when their triggers are entered 
        //(so they may be safely touched on the side, which is much less squish-inducing)
        private void OnTriggerEnter2D(Collider2D other)
        {
            IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(mySheet.Strength + mySheet.TouchAttack.DamageModifier, mySheet.TouchAttack.DamageType);
            }
        }
    }
}
