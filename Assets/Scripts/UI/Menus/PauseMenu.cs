using MarblesAndMonsters.Characters;
using LevelManagement.Menus;
using UnityEngine;
using LevelManagement.DataPersistence;
using LevelManagement;
using Chronos;

namespace MarblesAndMonsters.Menus
{
    public class PauseMenu : Menu<PauseMenu>
    {
        Clock _rootClock;

        public Clock RootClock
        {
            get
            {
                if (_rootClock is null)
                    _rootClock = Timekeeper.instance.Clock("Root");
                return _rootClock;
            }
        }

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
