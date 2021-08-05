using MarblesAndMonsters.Menus.Components;
using LevelManagement.Menus;
using UnityEngine;

namespace MarblesAndMonsters.Menus
{
    public class GameMenu : Menu<GameMenu>
    {
        public DeathCounterController deathCountUI;
        public HealthBarController healthBarController;
        public ScrollCounterController treasureUI;
        public InventoryUIController inventoryUI;
        public KeyChainUIController keychainUI;
        public QuickAccessController quickAccessController;

        protected override void Awake()
        {
            base.Awake();
            deathCountUI = GameObject.FindObjectOfType<DeathCounterController>();
            healthBarController = GameObject.FindObjectOfType<HealthBarController>();
            treasureUI = GameObject.FindObjectOfType<ScrollCounterController>();
            inventoryUI = GameObject.FindObjectOfType<InventoryUIController>();
            keychainUI = GameObject.FindObjectOfType<KeyChainUIController>();
            quickAccessController = GameObject.FindObjectOfType<QuickAccessController>();
        }

        //protected void OnEnable()
        //{
        //    RefreshUI();
        //}

        public void RefreshUI() 
        {
            Debug.Log("RefreshUI()");
            deathCountUI.UpdateDeathCountUI();
            healthBarController.UpdateHealth();
            treasureUI.UpdateTreasureCount();
            keychainUI.Clear();
            quickAccessController.ClearAll();
        }
        
        public void OnPausePressed()
        {
            GameManager.Instance.PauseGame();
        }

        public void OnBackpackPressed()
        {
            Time.timeScale = 0.0f;
            MenuManager.Instance.OpenMenu(MenuTypes.BackpackMenu);
        }
    }
}