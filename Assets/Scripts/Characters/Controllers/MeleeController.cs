using System;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters
{
    public class MeleeController : CombatControl<MeleeController>
    {
        public override int TryAttack()
        {
            if (AttackAvailable)
            {
                if (GetDamagablesInRange(out List<IDamagable> damagables))
                {
                    foreach (IDamagable damagable in damagables)
                    {
                        DealDamageTo(damagable);
                    }
                    AttackAvailable = false;
                    StartCoroutine(AttackCooldown(AttackStats.Cooldown));
                    return damagables.Count;
                }
            }
            return 0;
        }
    }

}