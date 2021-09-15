using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.DamageEffects
{
    public class Fire : DamageEffect
    {
        public override Type Type => typeof(Fire);

        public Fire(IDamagable damagable) : base(damagable) { }
    }
}