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
                                                      //HealthBarController healthBarController;
        public List<KeyItem> KeyChain => keyChain;

        //public int[] QuickAccess => quickAccess;
        public List<SpellName> ActiveSpells;

        //singleton stuff
        private static Player _instance;
        public static Player Instance   //singleton accessor
        {
            get { return _instance; }
        }

        //accessors
        public int TreasureCount { get { return treasureCount; } set { treasureCount = value; } }
        public int DeathCount { get { return deathCount; } set { deathCount = value; } }
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
            //quickAccess = { -1, -1};
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
                //TODO: add inventory and keychain initializers

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
        public void AddItemToInventory(SpellStats itemStats)
        {
            InventorySlot inventorySlot = new InventorySlot(itemStats);
            inventory.Add(inventorySlot);
            //AddItemtoQuickAccess(inventorySlot);
        }

        public void AddItemtoQuickAccess(InventorySlot inventorySlot)
        {
            for (int i = 0; i < QuickAccessController.QuickSlotMax; i++)
            {
                if (!inventory.Exists(x => x.QuickAccessSlot == i))
                {
                    inventorySlot.QuickAccessSlot = i;
                    if (GameMenu.Instance)
                    {
                        GameMenu.Instance.quickAccessController.AssignQuickAccess(i, inventorySlot.ItemStats);
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// Add a spell the Player's Active Spells and update the Quick Access icons on the Game Menu.
        ///     If 
        /// </summary>
        /// <param name="stats"></param>
        public void AddtoActiveSpells(SpellStats stats)
        {
            // if the spell is all ready in the active list, skip
            if (!ActiveSpells.Exists(x => x == stats.SpellName))
            {
                for (int i = 0; i < QuickAccessController.QuickSlotMax; i++)
                {
                    //if any spell is assigned to slot i, skip
                    if (MySheet.Spells.Any(x => x.Value.QuickSlot == i && x.Value.IsQuickSlotAssigned))
                    {
                        Debug.Log(string.Format("Quickslot {0} is all ready assigned, skipping...", i.ToString()));
                        continue;
                    }
                    ActiveSpells.Add(stats.SpellName);
                    MySheet.Spells[stats.SpellName].IsQuickSlotAssigned = true;
                    MySheet.Spells[stats.SpellName].QuickSlot = i;
                    
                    if (GameMenu.Instance)
                    {
                        GameMenu.Instance.quickAccessController.AssignQuickAccess(i, stats);
                    }
                    break;
                }
            } else
            {
                Debug.Log(string.Format("The spell {0} is all ready in Player's Active Spells", stats.SpellName.ToString()));
            }
        }

        /// <summary>
        /// debug function for printing contents of Inventory to console
        /// </summary>
        public void PrintInventory()
        {
            foreach(var item in inventory)
            {
                Debug.Log(string.Format("PrintInventory() =>  Item {0}: quantity = {1}, id = {2}", item.ItemStats.name, item.Quantity, item.Id));
            }
        }

        //public void RemoveItemFromInventory(InventoryItem itemToRemove)
        //{
        //    inventory.Remove(itemToRemove);
        //}

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

        /// <summary>
        /// check Invetory for 
        /// </summary>
        /// <param name="Id">item Id to be consumed</param>
        /// <returns>true if item is correctly consumed, false if something went wrong</returns>
        //public bool ConsumeItem(string Id) 
        //{
        //    if (inventory.Exists(x => x.Id == Id))
        //    {
        //        //var newQuantity = inventory.Find(x => x.Id == Id).Quantity--;
        //        var quickSlot = inventory.Find(x => x.Id == Id).QuickAccessSlot;
        //        inventory[quickSlot].Quantity--;
        //        //if quantity is zero (or less, somehow?) remove the item from the quickslot and then remove from inventory
        //        if (inventory[quickSlot].Quantity <= 0)
        //        {
        //            GameMenu.Instance.quickAccessController.UnassignQuickAccess(quickSlot, inventory[quickSlot].ItemStats);
        //            inventory.Remove(inventory.Find(x => x.Id == Id));
        //        } else {
        //            //update QuickAccessSlot Quantity
        //            GameMenu.Instance.quickAccessController.UpdateQuantity(quickSlot, inventory[quickSlot].Quantity);
        //        }
        //        return true;
        //    }
        //    return false;
        //}


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

        private void UpdateInventoryUI()
        {
            GameMenu.Instance.inventoryUI.UpdateUI(inventory.Select(x => x.ItemStats.InventoryIcon).ToList());
        }

        //public void UseForceBubble(float duration)
        //{
        //    forceBubble.Activate(duration);
        //    ApplyInvincible(duration);
        //}

        private void SetSpawnLocation()
        {
            spawnPoint = GameObject.FindObjectOfType<SpawnPoint_Player>();
            //mySheet.SetSpawnLocation(spawnPoint.transform);
        }

        //there's probably a better way to abstract the CharacterDeath to not copy/paste the base function
        public override void CharacterDeath(DeathType deathType)
        {
            if (isDying) { return; }
            else
            {
                isDying = true;
                myRigidbody.velocity = Vector2.zero;
                deathCount++;
                if (GameMenu.Instance)
                {
                    GameMenu.Instance.quickAccessController.ClearAll();
                }
                switch (deathType)
                {
                    case DeathType.Falling:
                        animator.SetBool(aTriggerFalling, true);
                        audioSource.clip = MySheet.baseStats.ClipDeathFall;
                        audioSource.Play();
                        break;
                    case DeathType.Damage:
                        animator.SetBool(aTriggerDeathByDamage, true);
                        break;
                    case DeathType.Fire:
                        break;
                    case DeathType.Poison:
                        break;
                    default:
                        Debug.LogError("Unhandled deathtype enum!");
                        break;
                }
                StartCoroutine(DeathAnimation(deathType));
            }
        }

        protected override IEnumerator DeathAnimation(DeathType deathType)
        {
            
            ResetInventoryItems();
            float animationLength = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
            string animationName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

            Debug.Log(string.Format("DeathAnimation {0} has died of {1}!  the animation named {2} takes {3:#,###.###} sec", 
                gameObject.name, deathType.ToString(), animationName ,animationLength));
            yield return new WaitForSeconds(0.75f);  //death animations are 8 frames, current fps is 12
            GameManager.Instance.LevelLose();
        }

        public override void TakeDamage(int damageAmount, DamageType damageType)
        {
            base.TakeDamage(damageAmount, damageType);
            animator.SetBool(aTriggerDamageNormal, true);
            GameMenu.Instance.healthBarController.UpdateHealth();
            animator.SetBool(aTriggerDamageNormal, false);
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
