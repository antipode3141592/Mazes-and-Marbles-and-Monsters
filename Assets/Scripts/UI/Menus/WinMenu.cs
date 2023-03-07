using LevelManagement.Menus;
using UnityEngine;

namespace MarblesAndMonsters.Menus
{
    public class WinMenu : Menu<WinMenu>
    {
        public void OnNextLevelPressed()
        {
            _gameManager.ShouldLoadNextLevel = true;
            base.OnBackPressed(); //return to GameMenu
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