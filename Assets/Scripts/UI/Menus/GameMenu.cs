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
        public BackpackController backpackController;

        //[SerializeField]
        //private Animator backpackIconAnimator;

        protected void Awake()
        {
            healthBarController = FindObjectOfType<HealthBarController>();
            treasureUI = FindObjectOfType<ScrollCounterController>();
            inventoryUI = FindObjectOfType<InventoryUIController>();
            keychainUI = FindObjectOfType<KeyChainUIController>();
            quickAccessController = FindObjectOfType<QuickAccessController>();
            backpackController = FindObjectOfType<BackpackController>(true); //include inactive
        }

        public void RefreshUI() 
        {
            if (healthBarController)
            {
                healthBarController.UpdateHealth();
            }
            if (treasureUI)
            {
                treasureUI.UpdateTreasureCount();
            }
        }
        
        public void OnPausePressed()
        {
            _gameManager.PauseGame();
        }

        public void OnBackpackPressed()
        {
            _menuManager.OpenMenu(MenuTypes.BackpackMenu);
        }
    }
}