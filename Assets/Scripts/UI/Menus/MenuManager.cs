using LevelManagement.Menus;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace MarblesAndMonsters.Menus
{
    public class MenuManager : MonoBehaviour, IMenuManager
    {
        IGameManager _gameManager;

        Dictionary<MenuTypes, Menu> menuCollection = new Dictionary<MenuTypes, Menu>();
        Stack<Menu> _menuStack = new Stack<Menu>();

        [Inject]
        public void Init(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        void Awake()
        {
            menuCollection.Add(MenuTypes.MainMenu, FindObjectOfType<MainMenu>(true));
            menuCollection.Add(MenuTypes.CreditsMenu, FindObjectOfType<CreditsMenu>(true));

            menuCollection.Add(MenuTypes.GameMenu, FindObjectOfType<GameMenu>(true));
            menuCollection.Add(MenuTypes.BackpackMenu, FindObjectOfType<BackpackMenu>(true));
            menuCollection.Add(MenuTypes.PauseMenu, FindObjectOfType<PauseMenu>(true));
            menuCollection.Add(MenuTypes.SettingsMenu, FindObjectOfType<SettingsMenu>(true));

            menuCollection.Add(MenuTypes.WinMenu, FindObjectOfType<WinMenu>(true));
            menuCollection.Add(MenuTypes.DefeatMenu, FindObjectOfType<DefeatMenu>(true));
            menuCollection.Add(MenuTypes.MapMenu, FindObjectOfType<MapMenu>(true));
            menuCollection.Add(MenuTypes.MapPopupMenu, FindObjectOfType<MapPopupMenu>(true));
        }

        void Start()
        {
            InitializeMenus();
        }

        void InitializeMenus()
        {
            if (SceneManager.sceneCount == 2)
            {
                OpenMenu(MenuTypes.MainMenu);
            }
            else
            {
                OpenMenu(MenuTypes.GameMenu);
                _gameManager.ShouldBeginLevel = true;
            }
        }

        public void OpenMenu(MenuTypes menuType)
        {
            if (Debug.isDebugBuild)
                Debug.Log($"Opening Menu of Type: {menuType}");
            if (_menuStack.Count > 0)
            {
                foreach (Menu menu in _menuStack)
                {
                    if (Debug.isDebugBuild)
                        Debug.Log($"Deactivating Menu in Stack: {menu.name}");
                    menu.gameObject.SetActive(false);
                }
            }
            var _menu = menuCollection[menuType];
            _menu.gameObject.SetActive(true);
            _menuStack.Push(_menu);
            if (Debug.isDebugBuild)
                Debug.Log($"After push to stack, there are {_menuStack.Count} menus in _menuStack");
        }

        public void CloseMenu()
        {
            if (_menuStack.Count == 0)
            {
                if (Debug.isDebugBuild)
                    Debug.LogWarning("no menu to close, menu stack is empty!");
                return;
            }
            Menu topMenu = _menuStack.Pop();
            topMenu.gameObject.SetActive(false);
            if (_menuStack.Count > 0)
            {
                Menu nextMenu = _menuStack.Peek();
                nextMenu.gameObject.SetActive(true);
            }

        }
    }
}