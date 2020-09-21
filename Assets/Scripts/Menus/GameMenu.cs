using LevelManagement.Menus.Components;
using MarblesAndMonsters;
using MarblesAndMonsters.Items;
using UnityEngine;

namespace LevelManagement.Menus
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
         protected void Start()
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
            GameController.Instance.PauseGame();
        }

        public void UpdateDeathCount()
        {
            deathCountUI.UpdateDeathCountUI();
        }

        public void UpdateHealth(int amount)
        {
            healthBarController.AdjustHealth(amount);
        }

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