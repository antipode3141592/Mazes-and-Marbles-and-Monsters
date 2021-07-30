﻿using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Actions;
using System;

namespace MarblesAndMonsters.Characters
{
    //  Data Model for the Character Stats and Behaviors
    //  Characters have stats that define their behavior
    //      Health (integer amount of damage they can take before dying)
    //      Armor (integer amount of damage reduction every time they are struck)
    //      Attacks (integer damage value and type of damage effect, combined in Attack class)
    //      Moves (
    //  Dependencies:
    //      CharacterBaseStats - each type of character has a corresponding scriptable object containing the base stats

    public class CharacterStateEventArgs: EventArgs
    {
        public string Message { get; set; }
        CharacterStateEventArgs(string message)
        {
            Message = message;
        }
    }

    public class CharacterSheet: MonoBehaviour
    {
        public CharacterBaseStats baseStats;

        private int maxHealth;
        private int currentHealth;
        //private int startingHealth;
        //private int maxHealthLimit = 10;

        private int armor;

        private List<DamageType> damageImmunities;

        private bool respawnFlag;   //if true, character respawn
        //[SerializeField]
        private float respawnPeriod;    //seconds before respawn
        [SerializeField]
        private bool isAsleep;
        private bool isPoisoned;
        private bool isBurning;
        private bool isFrozen;
        private bool isInvincible;
        private bool isLevitating;

        public event EventHandler OnBurning;
        public event EventHandler OnBurningEnd;
        public event EventHandler OnInvincible;
        public event EventHandler OnInvincibleEnd;
        public event EventHandler OnLevitating;
        public event EventHandler OnLevitatingEnd;

        private float sleepTimeCounter;
        private float poisonTimeCounter;
        private float burnTimeCounter;
        private float invincibleTimeCounter;
        private float levitatingTimeCounter;

        protected List<Movement> movements;

        //read only
        public bool RespawnFlag => respawnFlag;
        public float RespawnPeriod => respawnPeriod;
        //accessors
        public int Armor { get { return armor; } set { armor = value; } }
        public int CurrentHealth { 
            get { return currentHealth; } 
            set { currentHealth = Mathf.Clamp(value,0,maxHealth); } 
        }

        public int MaxHealth { get { return maxHealth; } set { maxHealth = value; } }

        public bool IsAsleep => isAsleep;
        public bool IsPoisoned { get; set; }
        public bool IsBurning 
        { 
            get {
                return isBurning;
            }
            set {
                isBurning = value;
                if (value)
                {
                    OnBurning?.Invoke(this, EventArgs.Empty);
                } else
                {
                    OnBurningEnd?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        public bool IsFrozen => isFrozen;
        public bool IsInvincible 
        { 
            get { return isInvincible; } 
            set {
                isInvincible = value; 
                if (value)
                {
                    OnInvincible?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    OnInvincibleEnd?.Invoke(this, EventArgs.Empty);
                }
            } 
        }
        public bool IsLevitating 
        { 
            get { return isLevitating; } 
            set { 
                isLevitating = value;
                if (value)
                {
                    OnLevitating?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    OnLevitatingEnd?.Invoke(this, EventArgs.Empty);
                }
            } 
        }
        public float SleepTimeCounter { get => sleepTimeCounter; set => sleepTimeCounter = value; }
        public float PoisonTimeCounter { get => poisonTimeCounter; set => poisonTimeCounter = value; }
        public float FireTimeCounter { get => burnTimeCounter; set => burnTimeCounter = value; }
        public float InvincibleTimeCounter { get => invincibleTimeCounter; set => invincibleTimeCounter = value; }
        public float LevitatingTimeCounter { get => levitatingTimeCounter; set => levitatingTimeCounter = value; }

        //read-only accessors
        public List<DamageType> DamageImmunities => damageImmunities;
        public List<Movement> Movements => movements;



        #region Unity Functions
        private void Awake()
        {
            //grab attached Movement Components
            movements = new List<Movement>(GetComponents<Movement>());
            if (baseStats)
            {
                SetInitialStats();
            }
        }

        private void Update()
        {
            var dT = Time.deltaTime;
            
            //decrement state counters
            if (IsInvincible)
            {
                invincibleTimeCounter -= dT;
                if (invincibleTimeCounter < 0.0f)
                {
                    invincibleTimeCounter = 0.0f;
                    IsInvincible = false;
                }
            }
            if (IsLevitating)
            {
                levitatingTimeCounter -= dT;
                if (levitatingTimeCounter < 0.0f)
                {
                    levitatingTimeCounter = 0.0f;
                    IsLevitating = false;
                }
            }
            if (IsPoisoned)
            {
                poisonTimeCounter -= dT;
                if (poisonTimeCounter < 0.0f)
                {
                    poisonTimeCounter = 0.0f;
                    IsPoisoned = false;
                }
            }
            if (IsBurning)
            {
                burnTimeCounter -= dT;
                if (burnTimeCounter < 0.0f)
                {
                    burnTimeCounter = 0.0f;
                    IsBurning = false;
                }
            }
        }
        #endregion



        public void SetInitialStats()
        {
            armor = baseStats.Armor;
            maxHealth = baseStats.MaxHealth;
            currentHealth = baseStats.MaxHealth;

            respawnFlag = baseStats.RespawnFlag;
            respawnPeriod = baseStats.RespawnPeriod;
            damageImmunities = baseStats.DamageImmunities;
        }
    }
}