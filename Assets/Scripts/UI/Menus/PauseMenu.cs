using MarblesAndMonsters.Characters;
using LevelManagement.Menus;
using UnityEngine;
using LevelManagement.DataPersistence;
using LevelManagement;

namespace MarblesAndMonsters.Menus
{
    public class PauseMenu : Menu<PauseMenu>
    {
        public void OnResumePressed()
        {
            _gameManager.UnpauseGame();
        }

        public void OnRestartPressed() {
            Player.Instance.CharacterDeath(DeathType.Damage);
            base.OnBackPressed();   //return to GameMenu
        }

        public void OnMainMenuPressed()
        {
            _levelManager.LoadMainMenuLevel();
            _menuManager.OpenMenu(MenuTypes.MainMenu);
        }

        public void ResetData()
        {
            _dataManager.Clear();
        }

        public override void OnBackPressed()
        {
            Application.Quit();
        }
    }
}
