using MarblesAndMonsters.Menus.Components;
using LevelManagement.Menus;
using UnityEngine;

namespace MarblesAndMonsters.Menus
{
    public class GameMenu : Menu<GameMenu>
    {
        public DeathCounterController deathCountUI;
        public HealthBarController healthBarController;
        public TreasureCounterController treasureUI;
        public InventoryUI inventoryUI;

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
            deathCountUI.UpdateDeathCountUI();
            healthBarController.UpdateHealth();
            treasureUI.UpdateTreasureCount();
            inventoryUI.UpdateUI();
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