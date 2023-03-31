using LevelManagement.Menus;
using UnityEngine;

namespace MarblesAndMonsters.Menus
{
    public class DefeatMenu : Menu<DefeatMenu>
    {
        public void OnRestartPressed()
        {
            _gameManager.ShouldBeginLevel = true;
            _gameManager.EnterLocation();
        }

        public void OnMainMenuPressed()
        {
            _levelManager.LoadMainMenuLevel();
            _menuManager.OpenMenu(MenuTypes.MainMenu);
        }

        public override void OnBackPressed()
        {
            Application.Quit();
        }
    }
}
