using LevelManagement.DataPersistence;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.Characters
{

    public class CharacterSheet: MonoBehaviour
    {
        public CharacterBaseStats baseStats;

        int maxHealth = 1;
        int currentHealth = 1;
        int armor;
        public Material DefaultMaterial => baseStats.DefaultMaterial;

        bool respawnFlag;   //if true, character respawn
        float respawnPeriod;    //seconds before respawn

        //character states that could apply to any character
        [SerializeField] bool isAsleep;
        bool isPoisoned;
        bool isBurning;
        bool isFrozen;
        bool isLevitating;
        bool isStealth;
        [SerializeField] bool isBoardMovable = true;

        public event EventHandler OnBurning;
        public event EventHandler OnBurningEnd;
        public event EventHandler OnLevitating;
        public event EventHandler OnLevitatingEnd;
        public event EventHandler OnStealth;
        public event EventHandler OnStealthEnd;

        float sleepTimeCounter;
        float poisonTimeCounter;
        float burnTimeCounter;

        //read only
        public bool RespawnFlag => respawnFlag;
        public float RespawnPeriod => respawnPeriod;
        //accessors
        public int Armor { get { return armor; } set { armor = value; } }
        
        public int CurrentHealth { 
            get { return currentHealth; } 
            set { 
                currentHealth = Mathf.Clamp(value,0,maxHealth);
                //Debug.Log($"{gameObject.name} has health: {currentHealth}");
            } 
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
        public bool IsBoardMovable 
        { 
            get => isBoardMovable;
            set
            {
                isBoardMovable = value;

            }
        }

        public bool IsStealth => isStealth;
        public float SleepTimeCounter { get => sleepTimeCounter; set => sleepTimeCounter = value; }
        public float PoisonTimeCounter { get => poisonTimeCounter; set => poisonTimeCounter = value; }
        public float FireTimeCounter { get => burnTimeCounter; set => burnTimeCounter = value; }

        public List<DamageType> DamageImmunities;

        //read-only accessors
        public Dictionary<SpellName, Spell> Spells;

        #region Unity Functions
        void Awake()
        {
            Spells = new Dictionary<SpellName,Spell>();
            DamageImmunities = new List<DamageType>();
            foreach (Spell _spell in GetComponentsInChildren<Spell>())
            {
                Spells.Add(_spell.SpellName, _spell);
                Debug.Log(string.Format("Spell {0} associated with {1}", _spell.name, _spell.SpellStats.SpellName));
            }
            
        }

        protected void OnEnable()
        {
            if (baseStats)
                SetInitialStats();
        }

        void Update()
        {
            var dT = Time.deltaTime;
            //decrement state counters
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
            for (int i = 0; i < baseStats.DamageImmunities.Count; i++)
            {
                DamageImmunities.Add(baseStats.DamageImmunities[i]);
            }
        }
    }
}