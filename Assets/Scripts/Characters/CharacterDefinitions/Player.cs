using LevelManagement.DataPersistence;
using MarblesAndMonsters.Events;
using MarblesAndMonsters.Items;
using MarblesAndMonsters.Menus;
using MarblesAndMonsters.Menus.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using LightingSettings = MarblesAndMonsters.Lighting.LightingSettings;

namespace MarblesAndMonsters.Characters
{
    // Player is a special type of Character Sheet Controller
    //  singleton pattern
    public class Player : CharacterControl
    {
        #region Properties
        //special player-only effects
        public ParticleSystem treasureEffect;
        public ParticleSystem addMaxHealthEffect;

        //event stuff
        public event EventHandler<TransformEventArgs> FollowPlayer;
        public event EventHandler OnPlayerDeath;
        public event EventHandler OnDamaged;

        CameraManager _cameraManager;

        int deathCount = 0;
        int treasureCount = 0;

        List<InventorySlot> inventory;
        List<KeyItem> keyChain;

        [SerializeField] LightingSettings lightingSettings;
        GlobalLight globalLight;
        PlayerTorch playerTorch;

        public List<InventorySlot> Inventory => inventory;//read only accessor shorthand

        public List<KeyItem> KeyChain => keyChain;

        protected IMenuManager _menuManager;
        protected IDataManager _dataManager;
        protected GameMenu _gameMenu;

        //singleton stuff
        static Player _instance;
        public static Player Instance   //singleton accessor
        {
            get { return _instance; }
        }

        //accessors
        public int TreasureCount { get { return treasureCount; } set { treasureCount = value; } }
        public int DeathCount { get { return deathCount; } set { deathCount = value; } }

        public int StaffLevel = 2;
        #endregion

        #region Unity Functions
        protected override void Awake()
        {
            //singleton check
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = (Player)this;
            }
            _cameraManager = FindObjectOfType<CameraManager>();
            globalLight = FindObjectOfType<GlobalLight>();
            playerTorch = FindObjectOfType<PlayerTorch>();
            inventory = new List<InventorySlot>();
            keyChain = new List<KeyItem>();

            _menuManager = FindObjectOfType<MenuManager>();
            _dataManager = FindObjectOfType<DataManager>();
            _gameMenu = FindObjectOfType<GameMenu>(true);   //grab inactive

            base.Awake();
        }

        protected override void Start()
        {
            base.Start();  //store character in game manager
            FollowPlayer?.Invoke(this, new TransformEventArgs(transform));
            _cameraManager.FollowObject(transform);
            if (_dataManager != null)
            {
                deathCount = _dataManager.PlayerTotalDeathCount > 0 ? _dataManager.PlayerTotalDeathCount : 0;
                treasureCount = _dataManager.PlayerScrollCount > 0 ? _dataManager.PlayerScrollCount : 0;
                mySheet.MaxHealth = _dataManager.PlayerMaxHealth > 3 ? _dataManager.PlayerMaxHealth : mySheet.baseStats.MaxHealth;
                mySheet.CurrentHealth = mySheet.MaxHealth;
                //unlock spells
                foreach(var spell in _dataManager.UnlockedSpells)
                {
                    MySheet.Spells[spell.SpellName].SpellStats = spell.SpellStats;
                    MySheet.Spells[spell.SpellName].IsUnlocked = true;
                    MySheet.Spells[spell.SpellName].IsQuickSlotAssigned = spell.IsAssigned;
                    MySheet.Spells[spell.SpellName].QuickSlot = spell.QuickSlot;
                }
            } 
            else
            {
                deathCount = 0;
                treasureCount = 0;
                mySheet.MaxHealth = 3;
            }
            AdjustLight();
            if (_gameMenu != null)
            {
                _gameMenu.RefreshUI();
                _gameMenu.quickAccessController.AssignAllSpellSlots();
                UpdateKeyChainUI();
            }
        }

        void AdjustLight()
        {
            if (globalLight.Intensity < 0.9f) 
            {
                playerTorch.gameObject.SetActive(true);
                playerTorch.AdjustLight(lightingSettings.PlayerLightOnIntensity, lightingSettings.PlayerLightOnColor);
            } else
            {
                playerTorch.gameObject.SetActive(false);
            }
        }


        //cleanup for static instance
        protected virtual void OnDestroy()
        {
            
            if (_instance == this)
            {
                _instance = null;
            }
        }
        #endregion

        public void AddMaxHealth(int amount)
        {
            //adjust max health
            mySheet.MaxHealth += amount;
            mySheet.CurrentHealth = mySheet.MaxHealth;
            //trigger animation
            //trigger particle effects
            if (amount > 0)
            {
                addMaxHealthEffect.Play();
            }
            //
            _gameMenu.healthBarController.UpdateHealth();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="healAmount"></param>
        public override bool HealDamage(int healAmount)
        {
            //check for damage, skip if no healing needed
            if (mySheet.CurrentHealth < mySheet.MaxHealth)
            {
                if (healAmount < 0)
                {
                    //heal all
                    mySheet.CurrentHealth = mySheet.MaxHealth;
                }
                else
                {
                    mySheet.CurrentHealth += healAmount;
                }
                _gameMenu.healthBarController.UpdateHealth();
                return true;
            }
            return false;
        }

        public void AddTreasure(int value)
        {
            treasureCount += value;
            //treasureEffect.Play();
            _gameMenu.treasureUI.UpdateTreasureCount();
        }

        public void RemoveTreasure(int value)
        {
            treasureCount -= value;
            _gameMenu.treasureUI.UpdateTreasureCount();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemToAdd"></param>
        public void AddItemToInventory(ItemStats itemStats)
        {
            InventorySlot inventorySlot = new InventorySlot(itemStats);
            inventory.Add(inventorySlot);
            //AddItemtoQuickAccess(inventorySlot);
        }

        /// <summary>
        /// Add a spell the Player's Active Spells and update the Quick Access icons on the Game Menu.
        ///     If 
        /// </summary>
        /// <param name="stats"></param>
        public void AddtoActiveSpells(SpellStatsBase stats)
        {
            for (int i = 0; i < SpellStaffUIController.QuickSlotMax; i++)
            {
                //if any spell is assigned to slot i, skip
                if (MySheet.Spells.Any(x => x.Value.QuickSlot == i && x.Value.IsQuickSlotAssigned))
                {
                    Debug.Log(string.Format("Quickslot {0} is all ready assigned, skipping...", i.ToString()));
                    continue;
                }
                //ActiveSpells.Add(stats.SpellName);
                MySheet.Spells[stats.SpellName].IsQuickSlotAssigned = true;
                MySheet.Spells[stats.SpellName].QuickSlot = i;
                    
                if (_gameMenu)
                {

                    _gameMenu.quickAccessController.AssignSpellSlot(i, stats);
                }
                break;
            }
        }

        //remove all items from inventory
        public void ResetInventoryItems()
        {
            foreach (var item in inventory)
            {
                //
            }
            inventory.Clear();
            keyChain.Clear();
            Debug.Log("Player:  Removed all items from inventory!");
            UpdateKeyChainUI();
            //_gameMenu.quickAccessController.ClearAll();
        }

        public void AddToKeyChain(KeyItem keyToAdd)
        {
            if (keyChain == null) { keyChain = new List<KeyItem>(); }
            keyChain.Add(keyToAdd);
            UpdateKeyChainUI();
            Debug.Log("Player added a " + keyToAdd.name + "to keychain!");
        }

        public void RemoveKeyFromKeyChain(KeyItem keyToRemove)
        {
            keyChain.Remove(keyToRemove);
            UpdateKeyChainUI();
        }

        void UpdateKeyChainUI()
        {
            if (_gameMenu is null)
                return;
            if (keyChain is null)
                return;
            _gameMenu.keychainUI.UpdateUI(keyChain.Select(x => x.KeyStats.InventoryIcon).ToList());
        }

        protected override void PreDeathAnimation()
        {
            base.PreDeathAnimation();
            deathCount++;
            ResetInventoryItems();
            
            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
            if (_gameMenu)
            {
                _gameMenu.quickAccessController.ClearAll();
            }
        }

        public override void OnDeathAnimationCompleted(object sender, DeathEventArgs deathEventArgs)
        {
            Debug.Log($"{gameObject.name} death animation complete, resetting spawnPoint", this);
            _gameManager.LevelLose();
        }

        public override void TakeDamage(int damageAmount, DamageType damageType)
        {
            base.TakeDamage(damageAmount, damageType);
            _gameMenu.healthBarController.UpdateHealth();
        }

        public void TakeDamage(int amount, DamageType damageType, Vector2 attackVector)
        {
            throw new System.NotImplementedException();
        }
        //collision stuff

        void OnCollisionEnter2D(Collision2D collision)
        {
            collisionIntensity = Mathf.Clamp01(collision.relativeVelocity.magnitude * 0.1f);   // divide by 10, clamped to [0,1]
            collisionEffects.PlayFeedbacks(transform.position, collisionIntensity);
        }
    }
}
