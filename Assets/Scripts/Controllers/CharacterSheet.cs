﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters;

namespace MarblesAndMonsters.Characters {
    //  Data Model for the Character Sheet  (MVC pattern)
    //  Characters have stats that define their behavior
    //      Health (integer amount of damage they can take before dying)
    //      Armor (integer amount of damage reduction every time they are struck)
    //      Attacks (integer damage value and type of damage effect, combined in Attack class)
    //      Moves (

    public enum DamageType { Normal, Push, Fire, Poison }
    //  Normal - no special effects
    //  Push - apply force directly away from Self (normal of vector from Self to Target)
    //  Fire - add X Fire tokens (every period while token.count > 0, take a fire damage and remove a token)
    //  Poison - add X Poison tokens (

    //  Class that defines 
    public class Attack: MonoBehaviour
    {
        private int damageModifier;
        private int currentDamageModifier;
        private List<DamageType> damageTypes;

        public int DamageModifier => damageModifier;
        public int CurrentDamageModifier => currentDamageModifier;
        public List<DamageType> DamageTypes => damageTypes;
    }

    public class CharacterSheet: MonoBehaviour
    {
        [SerializeField]
        private int maxHealth;
        private int currentHealth;

        [SerializeField]
        private int strength;
        [SerializeField]
        private int armor;
        [SerializeField]
        private List<Attack> attacks;
        [SerializeField]
        private List<DamageType> damageImmunities;

        [SerializeField]
        private bool respawnFlag;   //if true, character respawn
        [SerializeField]
        private float respawnPeriod;    //seconds before respawn

        protected List<Movement> movements;

        //accessors
        public int Strength { get { return strength; } set { strength = value; } }
        public int Armor { get { return armor; } set { armor = value; } }

        public int CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }
        public int MaxHealth => maxHealth;
        public List<DamageType> DamageImmunities => damageImmunities;
        public List<Attack> Attacks => attacks;
        public List<Movement> Movements => movements;

        private void Awake()
        {
            //grab attached Movement Components
            movements = new List<Movement>(GetComponents<Movement>());
            //grab attached Attack Components
            attacks = new List<Attack>(GetComponents<Attack>());
        }
    }


}