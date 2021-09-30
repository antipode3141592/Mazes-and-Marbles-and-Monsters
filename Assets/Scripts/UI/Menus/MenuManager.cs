using LevelManagement.Menus;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

//  Class Name:  MenuManager
//  Interacts with:  Menu
//  Purpose:  A UI controller with 
//  

namespace MarblesAndMonsters.Menus
{

    public class MenuManager : MonoBehaviour
    {
        [SerializeField] bool ShowSplashScreen;

        private SplashScreen splashScreen;
        private Dictionary<MenuTypes, Menu> menuCollection = new Dictionary<MenuTypes, Menu>();
        private Stack<Menu> _menuStack = new Stack<Menu>();

        GameManager _gameManager;

        [Inject]
        public void Init(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        private void Awake()
        {
            //the true flag is to include inactive objects
            splashScreen = FindObjectOfType<SplashScreen>(true);
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

        private void Start()
        {
            InitializeMenus();
        }

        /// <summary>
        /// If MenuManager is in the only active scene, load 
        /// </summary>
        private void InitializeMenus()
        {
            if (SceneManager.sceneCount == 1)
            {
                if (ShowSplashScreen)
                {
                    splashScreen.gameObject.SetActive(true);
                    ShowSplashScreen = false;
                }
                else
                {
                    OpenMenu(MenuTypes.MainMenu);
                }
            }
            else
            {
                OpenMenu(MenuTypes.GameMenu);
                _gameManager.ShouldBeginLevel = true;
            }
        }

        public void OpenMenu(MenuTypes menuType)
        {
            Debug.Log(string.Format("Opening Menu of Type: {0}", menuType.ToString()));
            //if menus exist in stack, deactivate everything in stack
            if (_menuStack.Count > 0)
            {
                foreach (Menu menu in _menuStack)
                {
                    Debug.Log(string.Format("Deactivating Menu in Stack: {0}", menu.name));
                    menu.gameObject.SetActive(false);
                }
            }
            var _menu = menuCollection[menuType];
            _menu.gameObject.SetActive(true);
            _menuStack.Push(_menu);
            Debug.Log(string.Format("After push to stack, there are {0} menus in _menuStack", _menuStack.Count));
        }

        //close the top of the menu stack
        public void CloseMenu()
        {
            if (_menuStack.Count == 0)
            {
                Debug.LogWarning("no menu to close, menu stack is empty");
                return;
            }
            Menu topMenu = _menuStack.Pop();
            topMenu.gameObject.SetActive(false);
            //if menustack is not empty
            if (_menuStack.Count > 0)
            {
                Menu nextMenu = _menuStack.Peek();
                nextMenu.gameObject.SetActive(true);
            }
            
        }
    }
}