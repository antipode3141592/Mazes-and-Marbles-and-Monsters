using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Actions;

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
        private bool isAflame;
        private bool isFrozen;
        private bool isInvincible;
        //[SerializeField]
        //private Vector3 spawnPoint;

        private float sleepTimeCounter;
        private float poisonTimeCounter;
        private float fireTimeCounter;
        private float invincibleTimeCounter;

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
        public bool IsPoisoned => isPoisoned;
        public bool IsAflame => isAflame;
        public bool IsFrozen => isFrozen;
        public bool IsInvincible { get { return isInvincible; } set { isInvincible = value; } }

        public float SleepTimeCounter { get => sleepTimeCounter; set => sleepTimeCounter = value; }
        public float PoisonTimeCounter { get => poisonTimeCounter; set => poisonTimeCounter = value; }
        public float FireTimeCounter { get => fireTimeCounter; set => fireTimeCounter = value; }
        public float InvincibleTimeCounter { get => invincibleTimeCounter; set => invincibleTimeCounter = value; }


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

        #endregion

        public void Wakeup()
        {
            isAsleep = false;
        }

        public void PutToSleep()
        {
            isAsleep = true;
        }

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