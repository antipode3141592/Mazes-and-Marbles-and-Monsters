using UnityEngine;
using MarblesAndMonsters.Menus;
using MarblesAndMonsters;
using LevelManagement.DataPersistence;
using Zenject;

namespace LevelManagement.Menus
{
    public abstract class Menu<T>: Menu where T : Menu<T>
    {
        protected IMenuManager _menuManager;
        protected IGameManager _gameManager;
        protected IDataManager _dataManager;
        protected ILevelManager _levelManager;

        [Inject]
        public void Init(IMenuManager menuManager, IGameManager gameManager, IDataManager dataManager, ILevelManager levelManager)
        {
            _menuManager = menuManager;
            _gameManager = gameManager;
            _dataManager = dataManager;
            _levelManager = levelManager;
        }

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
