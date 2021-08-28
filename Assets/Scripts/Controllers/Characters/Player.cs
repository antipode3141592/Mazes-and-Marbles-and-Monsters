using Cinemachine;
using LevelManagement.Data;
using MarblesAndMonsters.Menus;
using MarblesAndMonsters.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MarblesAndMonsters.Menus.Components;
using MarblesAndMonsters.Actions;

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

        //
        private CinemachineVirtualCamera followCamera;  //some/all of the levels will have a camera follow the player around

        private int deathCount = 0;
        private int treasureCount = 0;

        private List<InventorySlot> inventory;
        private List<KeyItem> keyChain;

        [SerializeField]
        private LightingSettings lightingSettings;
        private GlobalLight globalLight;
        private PlayerTorch playerTorch;

        public List<InventorySlot> Inventory => inventory;//read only accessor shorthand

        public List<KeyItem> KeyChain => keyChain;

        //singleton stuff
        private static Player _instance;
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
                //DontDestroyOnLoad(gameObject);
            }
            globalLight = FindObjectOfType<GlobalLight>();
            playerTorch = FindObjectOfType<PlayerTorch>();
            inventory = new List<InventorySlot>();
            keyChain = new List<KeyItem>();
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();  //store character in game manager
            followCamera = FindObjectOfType<CinemachineVirtualCamera>();
            if (followCamera != null)
            {
                //set the 
                followCamera.Follow = gameObject.transform;
            }
            if (DataManager.Instance != null)
            {
                deathCount = DataManager.Instance.PlayerTotalDeathCount > 0 ? DataManager.Instance.PlayerTotalDeathCount : 0;
                treasureCount = DataManager.Instance.PlayerScrollCount > 0 ? DataManager.Instance.PlayerScrollCount : 0;
                mySheet.MaxHealth = DataManager.Instance.PlayerMaxHealth > 3 ? DataManager.Instance.PlayerMaxHealth : mySheet.baseStats.MaxHealth;
                mySheet.CurrentHealth = mySheet.MaxHealth;
                //unlock spells
                foreach(var spell in DataManager.Instance.UnlockedSpells)
                {
                    Debug.Log(string.Format("Unlock Spell from Datamanager:  {0}, {1}, {2}", spell.SpellStats.SpellName, spell.IsAssigned, spell.QuickSlot));
                    MySheet.Spells[spell.SpellName].SpellStats = spell.SpellStats;
                    MySheet.Spells[spell.SpellName].IsUnlocked = true;
                    MySheet.Spells[spell.SpellName].IsQuickSlotAssigned = spell.IsAssigned;
                    MySheet.Spells[spell.SpellName].QuickSlot = spell.QuickSlot;
                }
                //TODO: add inventory and keychain initializers
                foreach (var key in DataManager.Instance.CollectedKeys)
                {
                    Debug.Log(string.Format("Collected Key = {0}", key.name));
                    keyChain.Add(key);
                }

            } else
            {
                deathCount = 0;
                treasureCount = 0;
                mySheet.MaxHealth = 3;
            }
            AdjustLight();
            if (GameMenu.Instance != null)
            {
                GameMenu.Instance.RefreshUI();
                GameMenu.Instance.quickAccessController.AssignAllSpellSlots();
                UpdateKeyChainUI();
            }
        }

        private void AdjustLight()
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
            GameMenu.Instance.healthBarController.UpdateHealth();
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
                GameMenu.Instance.healthBarController.UpdateHealth();
                return true;
            }
            return false;
        }

        public void AddTreasure(int value)
        {
            treasureCount += value;
            //treasureEffect.Play();
            GameMenu.Instance.treasureUI.UpdateTreasureCount();
        }

        public void RemoveTreasure(int value)
        {
            treasureCount -= value;
            GameMenu.Instance.treasureUI.UpdateTreasureCount();
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
        public void AddtoActiveSpells(SpellStats stats)
        {
            for (int i = 0; i < QuickAccessController.QuickSlotMax; i++)
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
                    
                if (GameMenu.Instance)
                {
                    GameMenu.Instance.quickAccessController.AssignQuickAccess(i, stats);
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
            //GameMenu.Instance.quickAccessController.ClearAll();
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

        private void UpdateKeyChainUI()
        {
            GameMenu.Instance.keychainUI.UpdateUI(keyChain.Select(x => x.KeyStats.InventoryIcon).ToList());
        }

        protected override void PreDeathAnimation()
        {
            base.PreDeathAnimation();
            deathCount++;
            if (GameMenu.Instance)
            {
                GameMenu.Instance.quickAccessController.ClearAll();
            }
        }

        protected override IEnumerator DeathAnimation(DeathType deathType)
        {
            ResetInventoryItems();
            yield return new WaitForSeconds(0.5833f);  //death animations are 7 frames, current fps is 12
            GameManager.Instance.LevelLose();
        }

        public override void TakeDamage(int damageAmount, DamageType damageType)
        {
            base.TakeDamage(damageAmount, damageType);
            animator.SetTrigger(aTriggerDamageNormal);
            GameMenu.Instance.healthBarController.UpdateHealth();
        }

        public void TakeDamage(int amount, DamageType damageType, Vector2 attackVector)
        {
            throw new System.NotImplementedException();
        }

        //collision stuff

        private void OnCollisionEnter2D(Collision2D collision)
        {
            audioSource.clip = MySheet.baseStats.ClipHit;
            audioSource.Play();
        }

        protected override void SetLookDirection()
        {
            Vector2 direction = myRigidbody.velocity;
            if (!Mathf.Approximately(direction.x, 0.0f) || !Mathf.Approximately(direction.y, 0.0f))
            {
                lookDirection = direction;
                lookDirection.Normalize();
            }

            animator.SetFloat(aFloatLookX, lookDirection.x);
            animator.SetFloat(aFloatLookY, lookDirection.y);
        }
    }
}
