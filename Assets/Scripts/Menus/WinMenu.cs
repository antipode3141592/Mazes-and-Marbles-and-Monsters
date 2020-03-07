using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SampleGame;

namespace LevelManagement
{
    public class WinMenu : Menu<WinMenu>
    {
        public void OnNextLevelPressed()
        {
            base.OnBackPressed(); //return to game menu
            LevelLoader.LoadNextLevel();
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