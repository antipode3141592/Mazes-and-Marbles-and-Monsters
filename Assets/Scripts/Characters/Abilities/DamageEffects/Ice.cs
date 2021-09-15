using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.DamageEffects
{
    public class Ice : DamageEffect
    {
        public override Type Type => typeof(Ice);

        public Ice(IDamagable damagable) : base(damagable) { }
    }
}