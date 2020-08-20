using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters
{
    public enum DamageType { Normal, Push, Fire, Poison }
    //  Normal - no special effects
    //  Push - apply force directly away from Self (normal of vector from Self to Target)
    //  Fire - add X Fire tokens (every period while token.count > 0, take a fire damage and remove a token)
    //  Poison - add X Poison tokens (

    //  Class that defines 
    [Serializable]
    public class Attack : MonoBehaviour
    {
        [SerializeField]
        private int damageModifier;
        [SerializeField]
        private int currentDamageModifier;
        [SerializeField]
        private List<DamageType> damageTypes;

        public int DamageModifier => damageModifier;
        public int CurrentDamageModifier => currentDamageModifier;
        public List<DamageType> DamageTypes => damageTypes;
    }
}