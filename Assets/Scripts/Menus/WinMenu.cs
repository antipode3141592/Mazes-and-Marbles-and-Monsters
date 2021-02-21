using LevelManagement;
using LevelManagement.Menus;
using MarblesAndMonsters.Characters;
using UnityEngine;

namespace MarblesAndMonsters.Menus
{
    public class WinMenu : Menu<WinMenu>
    {
        private LevelManager levelLoader;

        protected override void Awake()
        {
            base.Awake();
            levelLoader = GameObject.FindObjectOfType<LevelManager>();
        }

        public void OnNextLevelPressed()
        {
            base.OnBackPressed(); //return to GameMenu
            //levelLoader.LoadNextLevel();
        }

        //restarting the level means killing the PC and resetting all items/monsters/obstacles
        public void OnRestartPressed() 
        {
            base.OnBackPressed();   //returns to the GameMenu
            Player.Instance.CharacterDeath(DeathType.Damage);
        }

        public void OnMainMenuPressed()
        {
            LevelManager.LoadMainMenuLevel();
            //MainMenu.Open();
            MenuManager.Instance.OpenMenu(MenuTypes.MainMenu);
        }

        public override void OnBackPressed()
        {
            Application.Quit();
        }
    }
}