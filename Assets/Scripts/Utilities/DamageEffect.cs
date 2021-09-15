using System;

namespace MarblesAndMonsters
{

    /// <summary>
    /// 
    /// </summary>
    public abstract class DamageEffect
    {
        protected IDamagable _damagable;
        public abstract Type Type { get; }

        public DamageEffect(IDamagable damagable)
        {
            _damagable = damagable;
        }
    }
}