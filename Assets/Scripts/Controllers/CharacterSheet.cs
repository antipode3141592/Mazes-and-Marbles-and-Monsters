using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters;
using System;

namespace MarblesAndMonsters.Characters {
    //  Data Model for the Character Sheet  (MVC pattern)
    //  Characters have stats that define their behavior
    //      Health (integer amount of damage they can take before dying)
    //      Armor (integer amount of damage reduction every time they are struck)
    //      Attacks (integer damage value and type of damage effect, combined in Attack class)
    //      Moves (

    

    public class CharacterSheet: MonoBehaviour
    {
        [SerializeField]
        private int maxHealth;
        private int currentHealth;

        [SerializeField]
        private int strength;
        [SerializeField]
        private int armor;
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
        public int CurrentHealth { 
            get { return currentHealth; } 
            set { currentHealth = Mathf.Clamp(value,0,maxHealth); } 
        }
        public int MaxHealth { get { return maxHealth; } set { maxHealth = value; } }

        //read-only accessors
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