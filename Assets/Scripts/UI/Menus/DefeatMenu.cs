using LevelManagement;
using LevelManagement.Menus;
using MarblesAndMonsters.Characters;
using UnityEngine;

namespace MarblesAndMonsters.Menus
{
    public class DefeatMenu : Menu<DefeatMenu>
    {
        //restarting the level means killing the PC and resetting all items/monsters/obstacles
        public void OnRestartPressed()
        {
            _gameManager.ShouldBeginLevel = true;
            base.OnBackPressed();   //returns to the GameMenu
            //Player.Instance.CharacterDeath(DeathType.Damage);
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