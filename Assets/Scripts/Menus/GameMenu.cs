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

        [SerializeField]
        private Animator backpackIconAnimator;

        protected override void Awake()
        {
            base.Awake();
            //deathCountUI = FindObjectOfType<DeathCounterController>();
            healthBarController = FindObjectOfType<HealthBarController>();
            treasureUI = FindObjectOfType<ScrollCounterController>();
            inventoryUI = FindObjectOfType<InventoryUIController>();
            keychainUI = FindObjectOfType<KeyChainUIController>();
            quickAccessController = FindObjectOfType<QuickAccessController>();
            backpackController = FindObjectOfType<BackpackController>(true); //include inactive
            
        }

        public void RefreshUI() 
        {
            Debug.Log("RefreshUI()");
            //deathCountUI.UpdateDeathCountUI();
            healthBarController.UpdateHealth();
            treasureUI.UpdateTreasureCount();
            //keychainUI.Clear();
            //quickAccessController.ClearAll();
        }
        
        public void OnPausePressed()
        {
            GameManager.Instance.PauseGame();
        }

        public void OnBackpackPressed()
        {
            MenuManager.Instance.OpenMenu(MenuTypes.BackpackMenu);
        }
    }
}