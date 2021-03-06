﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters
{
    public enum DamageType {All, Normal, Push, Fire, Poison, Ice}
    //  Normal - no special effects, slashes/bites/bumps/scrapes, you know, normal damage
    //  Push - apply impulse force directly away from Self (normal of vector from Self to other Object)
    //  Fire - add X Fire tokens (every period while token.count > 0, take a fire damage and remove a token)
    //  Poison - add X Poison tokens (

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