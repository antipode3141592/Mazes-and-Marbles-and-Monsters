using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICombatControl
    {
        public void DealDamageTo(IDamagable damagableObject);

        public int TryAttack();
        public bool GetDamagablesInRange(out List<IDamagable> damagables);

        public bool FindNearestEnemyInLineOfSight(out GameObject enemyInLineOfSight);

        public IEnumerator AttackCooldown(float restPeriod);
    }

}