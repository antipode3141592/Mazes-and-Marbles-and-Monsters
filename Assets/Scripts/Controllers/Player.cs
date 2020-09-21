using LevelManagement.Menus;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Items;
using LevelManagement.Data;
using System;

namespace MarblesAndMonsters.Characters
{
    // Player is a special type of Character Sheet Controller
    //  singleton pattern
    public class Player : CharacterSheetController<Player>
    {
        #region Properties
        public ParticleSystem treasureEffect;
        public ParticleSystem addMaxHealthEffect;
        

        private int deathCount;
        private int treasureCount;
        private List<InventoryItem> inventory;

        public List<InventoryItem> Inventory => inventory;//read only accessor
                                                          //HealthBarController healthBarController;

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
                DontDestroyOnLoad(gameObject);
            }

            inventory = new List<InventoryItem>();
            base.Awake();
        }

        protected override void Start()
        {
            if (spawnPoint == null)
            {
                SetSpawnLocation();
            }
            if (DataManager.Instance != null)
            {
                deathCount = DataManager.Instance.PlayerTotalDeathCount > 0 ? DataManager.Instance.PlayerTotalDeathCount : 0;
                treasureCount = DataManager.Instance.PlayerTreasureCount > 0 ? DataManager.Instance.PlayerTreasureCount : 0;
                mySheet.MaxHealth = DataManager.Instance.PlayerMaxHealth > 3 ? DataManager.Instance.PlayerMaxHealth : 3;
            } else
            {
                deathCount = 0;
                treasureCount = 0;
                mySheet.MaxHealth = 3;
            }
            base.Start();
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
            //trigger animation
            //trigger particle effects
            addMaxHealthEffect.Play();
            //
            GameMenu.Instance.AddMaxHealthUI(amount);
        }

        public void AddTreasure(int value)
        {
            treasureCount += value;
            treasureEffect.Play();
            GameMenu.Instance.UpdateTreasureCounter();
        }

        public void RemoveTreasure(int value)
        {
            treasureCount -= value;
            GameMenu.Instance.UpdateTreasureCounter();
        }

        public void AddItemToInventory(InventoryItem itemToAdd)
        {
            inventory.Add(itemToAdd);
            GameMenu.Instance.AddItemToInventory(itemToAdd);
            Debug.Log("Added a " + itemToAdd.name + "to inventory!");
        }

        public void RemoveItemFromInventory(InventoryItem itemToRemove)
        {
            inventory.Remove(itemToRemove);
            GameMenu.Instance.RemoveItemFromInventory(itemToRemove);
        }

        public override void CharacterSpawn()
        {
            ////check for spawn location
            if (spawnPoint == null)
            {
                SetSpawnLocation();
            }
            base.CharacterSpawn();
            treasureCount = DataManager.Instance.PlayerTreasureCount > 0 ? DataManager.Instance.PlayerTreasureCount : 0;
        }

        private void SetSpawnLocation()
        {
            spawnPoint = GameObject.FindObjectOfType<SpawnPoint_Player>();
        }

        public override void CharacterDeath()
        {
            deathCount++;
            Debug.Log(string.Format("CharacterDeath(), deathcount = ", deathCount.ToString()));
            base.CharacterDeath();
            //trigger death on controller
            GameController.Instance.EndLevel(false);
        }

        public override void TakeDamage(int damageAmount, DamageType damageType)
        {
            base.TakeDamage(damageAmount, damageType);
            GameMenu.Instance.ResetHealth();
        }
    }
}
