using MarblesAndMonsters.Characters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.DamageEffects
{
    public class Poison : DamageEffect
    {
        public override Type Type => typeof(Poison);

        public Poison(IDamagable damagable) : base(damagable) { }
    }
}