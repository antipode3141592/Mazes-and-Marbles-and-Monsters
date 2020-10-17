using UnityEngine;
using MarblesAndMonsters;
using MarblesAndMonsters.Characters;
using LevelManagement.Data;

namespace LevelManagement.Menus
{
    public class PauseMenu : Menu<PauseMenu>
    {
        public void OnResumePressed()
        {
            GameController.Instance.UnpauseGame();
            base.OnBackPressed();   //return to GameMenu
        }

        public void OnRestartPressed() {
            Player.Instance.CharacterDeath();
            base.OnBackPressed();   //return to GameMenu
        }

        public void OnMainMenuPressed()
        {
            LevelLoader.LoadMainMenuLevel();
            MainMenu.Open();
        }

        public void ResetData()
        {
            //TODO should confirm first
            DataManager.Instance.Clear();
            //update UI
            //UpdateCurrentGameStats();
        }
        public override void OnBackPressed()
        {
            Application.Quit();
        }
    }
}
