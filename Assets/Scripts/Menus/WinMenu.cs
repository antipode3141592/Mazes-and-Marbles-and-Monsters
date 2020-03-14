using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SampleGame;

namespace LevelManagement
{
    public class WinMenu : Menu<WinMenu>
    {
        private LevelLoader levelLoader;

        protected override void Awake()
        {
            base.Awake();
            levelLoader = GameObject.FindObjectOfType<LevelLoader>();
        }

        public void OnNextLevelPressed()
        {
            base.OnBackPressed(); //return to game menu
            //LevelLoader.LoadNextLevel();
            levelLoader.LoadNextLevel();
        }

        public void OnRestartPressed() 
        {
            base.OnBackPressed();
            LevelLoader.ReloadLevel();
        }

        public void OnMainMenuPressed()
        {
            LevelLoader.LoadMainMenuLevel();
            MainMenu.Open();
        }

        public override void OnBackPressed()
        {
            Application.Quit();
        }
    }
}