using LevelManagement.Menus;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Items;

namespace MarblesAndMonsters.Characters
{
    // Player is a special type of Character Sheet Controller
    //  singleton pattern
    public class Player : CharacterSheetController<Player>
    {
        public ParticleSystem treasureEffect;
        public ParticleSystem addMaxHealthEffect;

        private int deathCount;

        private int treasureCount;

        private List<InventoryItem> inventory;

        public List<InventoryItem> Inventory => inventory;//read only accessor
                                                          //HealthBarController healthBarController;

        private static Player _instance;

        public static Player Instance   //singleton accessor
        {
            get { return _instance; }
        }

        public int TreasureCount { get { return treasureCount; } set { treasureCount = value; } }
        public int DeathCount { get { return deathCount; } set { deathCount = value; } }

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
            base.Awake();
            inventory = new List<InventoryItem>();
            //healthBarController = GameObject.FindObjectOfType<HealthBarController>();
        }

        protected override void Start()
        {
            base.Start();
            GameMenu.Instance.RefreshUI();
        }

        //cleanup for static instance
        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }

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

        public override void CharacterDeath()
        {
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
