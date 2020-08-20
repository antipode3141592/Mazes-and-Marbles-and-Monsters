using MarblesAndMonsters;
using MarblesAndMonsters.UI;
using UnityEngine;

namespace LevelManagement
{
    public class GameMenu : Menu<GameMenu>
    {
        private DeathCounterController deathCountUI;
        private HealthBarController healthBarController;
        private TreasureCounterController treasureUI;
        private InventoryUI inventoryUI;

        protected override void Awake()
        {
            base.Awake();
            deathCountUI = GameObject.FindObjectOfType<DeathCounterController>();
            healthBarController = GameObject.FindObjectOfType<HealthBarController>();
            treasureUI = GameObject.FindObjectOfType<TreasureCounterController>();
            inventoryUI = GameObject.FindObjectOfType<InventoryUI>();
        }

        public void OnEnable()
        {
            RefreshUI();
        }

        public void RefreshUI() 
        {
            UpdateDeathCount();
            ResetHealth();
            UpdateTreasureCounter();
            UpdateInventoryUI();
        }
        
        public void OnPausePressed()
        {
            Time.timeScale = 0; //now STOP!

            PauseMenu.Open();
        }

        public void UpdateDeathCount()
        {
            deathCountUI.UpdateDeathCountUI();
        }

        //public void UpdateHealth()
        //{
        //    healthBarController.AdjustHealth();
        //}

        public void ResetHealth()
        {
            healthBarController.ResetHealth();
        }

        public void AddMaxHealthUI(int amount)
        {
            healthBarController.IncreaseMaxHealth(amount);
        }

        public void UpdateTreasureCounter()
        {
            treasureUI.UpdateTreasureCountUI();
        }

        public void AddItemToInventory(InventoryItem item)
        {
            inventoryUI.AddInventoryItem(item);
        }

        public void RemoveItemFromInventory(InventoryItem item)
        {
            inventoryUI.RemoveInventoryItem(item);
        }

        public void UpdateInventoryUI()
        {
            //update inventory in UI
            inventoryUI.UpdateUI();
        }
    }
}