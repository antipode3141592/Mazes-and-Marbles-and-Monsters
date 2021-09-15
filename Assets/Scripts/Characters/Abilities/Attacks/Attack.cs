using System;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters
{
    //  Class that defines parameters of a generic attack
    //      
    [Serializable]
    public abstract class Attack : ScriptableObject
    {
        [SerializeField]
        private int damageModifier;
        [SerializeField]
        private int currentDamageModifier;
        [SerializeField]
        private DamageType damageType;
        [SerializeField]
        private string displayName; //in the case of a beastiary?  perhaps for logging?

        public int DamageModifier => damageModifier;
        public int CurrentDamageModifier => currentDamageModifier;
        public DamageType DamageType => damageType;
        public string DisplayName => displayName;
    }
}