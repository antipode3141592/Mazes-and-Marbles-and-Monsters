using MarblesAndMonsters.Characters;
using LevelManagement.Menus;
using UnityEngine;
using LevelManagement.Data;
using LevelManagement;

namespace MarblesAndMonsters.Menus
{
    public class PauseMenu : Menu<PauseMenu>
    {
        public void OnResumePressed()
        {
            GameController.Instance.UnpauseGame();
            base.OnBackPressed();   //return to GameMenu
        }

        public void OnRestartPressed() {
            Player.Instance.CharacterDeath(DeathType.Damage);
            base.OnBackPressed();   //return to GameMenu
        }

        public void OnMainMenuPressed()
        {
            LevelLoader.LoadMainMenuLevel();
            //MainMenu.Open();
            MenuManager.Instance.OpenMenu(MenuTypes.MainMenu);
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
