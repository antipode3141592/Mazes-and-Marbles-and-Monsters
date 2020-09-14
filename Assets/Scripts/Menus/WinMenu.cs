using UnityEngine;
using MarblesAndMonsters;

namespace LevelManagement.Menus
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
            //LevelLoader.LoadNextLevel();  //static method relies on well ordered scene list
            levelLoader.LoadNextLevel();
        }

        //restarting the level means killing the PC and resetting all items/monsters/obstacles
        public void OnRestartPressed() 
        {
            base.OnBackPressed();   //returns to the GameMenu
            GameController.Instance.EndLevel(false);    //

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