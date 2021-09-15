using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters {
    //for all things that receive damage in the game
    public interface IDamagable
    {
        //apply damage to object implementing this interface
        public void TakeDamage(int damageAmount, DamageType damageType);
    }
}