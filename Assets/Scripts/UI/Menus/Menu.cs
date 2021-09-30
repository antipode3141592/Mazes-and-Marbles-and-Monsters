using UnityEngine;
using MarblesAndMonsters.Menus;
using MarblesAndMonsters;
using LevelManagement.DataPersistence;
using Zenject;

namespace LevelManagement.Menus
{
    public abstract class Menu<T>: Menu where T : Menu<T>
    {
        protected MenuManager _menuManager;
        protected GameManager _gameManager;
        protected DataManager _dataManager;
        protected LevelManager _levelManager;

        [Inject]
        public void Init(MenuManager menuManager, GameManager gameManager, DataManager dataManager, LevelManager levelManager)
        {
            _menuManager = menuManager;
            _gameManager = gameManager;
            _dataManager = dataManager;
            _levelManager = levelManager;
        }

        /// <summary>
        /// St
        /// </summary>
        public virtual void OnBackPressed()
        {
            _menuManager.CloseMenu();
        }
    }

    [RequireComponent(typeof(Canvas))]
    public abstract class Menu : MonoBehaviour
    {
        
    }
}
