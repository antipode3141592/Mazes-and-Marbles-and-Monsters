using System;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters
{
    //  Class that defines parameters of a generic attack
    //      
    [Serializable]
    public abstract class AttackStats : ScriptableObject
    {
        [SerializeField]
        protected int damageModifier;
        [SerializeField]
        protected int currentDamageModifier;
        [SerializeField]
        protected DamageType damageType;
        [SerializeField]
        protected string displayName; //in the case of a beastiary?  perhaps for logging?
        [SerializeField]
        protected float cooldown;


        public int DamageModifier => damageModifier;
        public int CurrentDamageModifier => currentDamageModifier;
        public DamageType DamageType => damageType;
        public string DisplayName => displayName;

        public float Cooldown => cooldown;
    }
}