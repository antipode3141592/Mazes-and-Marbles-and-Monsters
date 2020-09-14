using UnityEngine;
using MarblesAndMonsters;

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
            //LevelLoader.ReloadLevel();
            GameController.Instance.EndLevel(false);    
            base.OnBackPressed();   //return to GameMenu
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
