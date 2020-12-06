using LevelManagement;
using LevelManagement.Menus;
using MarblesAndMonsters.Characters;
using UnityEngine;

namespace MarblesAndMonsters.Menus
{
    public class WinMenu : Menu<WinMenu>
    {
        private LevelLoader levelLoader;

        protected override void Awake()
        {
            base.Awake();
            levelLoader = GameObject.FindObjectOfType<LevelLoader>();
        }

        public void OnNextLevelPressed()
        {
            base.OnBackPressed(); //return to GameMenu
            levelLoader.LoadNextLevel();
            GameController.Instance.LoadNextLevel();
        }

        //restarting the level means killing the PC and resetting all items/monsters/obstacles
        public void OnRestartPressed() 
        {
            base.OnBackPressed();   //returns to the GameMenu
            Player.Instance.CharacterDeath();
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