using LevelManagement;
using LevelManagement.Menus;
using MarblesAndMonsters.Characters;
using UnityEngine;

namespace MarblesAndMonsters.Menus
{
    public class DefeatMenu : Menu<DefeatMenu>
    {
        //private LevelLoader levelLoader;

        protected override void Awake()
        {
            base.Awake();
            //levelLoader = GameObject.FindObjectOfType<LevelLoader>();
        }

        //restarting the level means killing the PC and resetting all items/monsters/obstacles
        public void OnRestartPressed()
        {
            base.OnBackPressed();   //returns to the GameMenu
            Player.Instance.CharacterDeath(DeathType.Damage);
        }

        public void OnMainMenuPressed()
        {
            LevelLoader.LoadMainMenuLevel();
            //MainMenu.Open();
            MenuManager.Instance.OpenMenu(MenuTypes.MainMenu);
        }

        public override void OnBackPressed()
        {
            Application.Quit();
        }
    }
}