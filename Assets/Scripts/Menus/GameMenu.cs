using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SampleGame;

namespace LevelManagement
{
    public class GameMenu : Menu<GameMenu>
    {
        private DeathCounterController deathCountUI;
        private HealthBarController healthBarController;
        private TreasureCounterController treasureUI;

        protected override void Awake()
        {
            base.Awake();
            deathCountUI = GameObject.FindObjectOfType<DeathCounterController>();
            healthBarController = GameObject.FindObjectOfType<HealthBarController>();
            treasureUI = GameObject.FindObjectOfType<TreasureCounterController>();

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
        }
        
        public void OnPausePressed()
        {
            Time.timeScale = 0; //now STOP!  :-P

            PauseMenu.Open();
        }

        public void UpdateDeathCount()
        {
            deathCountUI.UpdateDeathCountUI();
        }

        public void UpdateHealth(int health)
        {
            healthBarController.AdjustHealth(health);
        }

        public void ResetHealth()
        {
            healthBarController.ResetHealth();
        }

        public void UpdateTreasureCounter()
        {
            treasureUI.UpdateTreasureCountUI();
        }
    }
}