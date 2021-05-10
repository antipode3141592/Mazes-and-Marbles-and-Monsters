using LevelManagement.Menus;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//code based on the course content at https://www.udemy.com/course/level-management-in-unity/ , which was super helpeful and highly recommended
//  Class Name:  MenuManager
//  Interacts with:  Menu
//  Purpose:  A UI controller with 
//  

namespace MarblesAndMonsters.Menus
{
    public enum MenuTypes {MainMenu, GameMenu, SettingsMenu, CreditsMenu, PauseMenu, WinMenu, DefeatMenu, MapMenu, MapPopupMenu, BackpackMenu}

    public class MenuManager : MonoBehaviour
    {
        private Dictionary<MenuTypes, Menu> menuCollection = new Dictionary<MenuTypes, Menu>();
        private Stack<Menu> _menuStack = new Stack<Menu>();

        private static MenuManager _instance;

        public static MenuManager Instance
        {
            get { return _instance; }
        }

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                //_menusParent = GameObject.Instantiate(MenusParentPrefab);   //instantiate menu stack
                //Object.DontDestroyOnLoad(_menusParent);
                //_menuList = new List<Menu>(FindObjectsOfType<Menu>(true));    //true to include inactive menu items
                //foreach (Menu menu in _menuList)
                //{
                //the true flag is to include inactive objects
                menuCollection.Add(MenuTypes.MainMenu, FindObjectOfType<MainMenu>(true));
                menuCollection.Add(MenuTypes.GameMenu, FindObjectOfType<GameMenu>(true));
                menuCollection.Add(MenuTypes.SettingsMenu, FindObjectOfType<SettingsMenu>(true));
                menuCollection.Add(MenuTypes.CreditsMenu, FindObjectOfType<CreditsMenu>(true));
                menuCollection.Add(MenuTypes.PauseMenu, FindObjectOfType<PauseMenu>(true));
                menuCollection.Add(MenuTypes.WinMenu, FindObjectOfType<WinMenu>(true));
                menuCollection.Add(MenuTypes.DefeatMenu, FindObjectOfType<DefeatMenu>(true));
                menuCollection.Add(MenuTypes.MapMenu, FindObjectOfType<MapMenu>(true));
                menuCollection.Add(MenuTypes.MapPopupMenu, FindObjectOfType<MapPopupMenu>(true));
                menuCollection.Add(MenuTypes.BackpackMenu, FindObjectOfType<BackpackMenu>(true));
                //}
                //Debug.Log(string.Format("there are {0} items in the _menuList: {1}", _menuList.Count, string.Join(", ", _menuList)));
            }
        }

        private void Start()
        {
            InitializeMenus();
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }

        private void InitializeMenus()
        {
            if (SceneManager.GetActiveScene().buildIndex <= 1)
            {
                Debug.Log(string.Format("Active Scene Index: {0}", SceneManager.GetActiveScene().buildIndex));
                OpenMenu(MenuTypes.MainMenu);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                OpenMenu(MenuTypes.MapMenu);
            }
            else
            {
                OpenMenu(MenuTypes.GameMenu);
            }
        }
        
        public void OpenMenu(Menu menuInstance)
        {
            if (menuInstance == null)
            {
                Debug.LogWarning("menuInstance is null in MenuManager");
                return;
            }
            Debug.Log(string.Format("Opening Menu: {0}", menuInstance.name));
            //if menus exist in stack, deactivate everything
            if (_menuStack.Count > 0)
            {
                foreach(Menu menu in _menuStack)
                {
                    Debug.Log(string.Format("Deactivating Menu in Stack: {0}", menu.name));
                    menu.gameObject.SetActive(false);
                }
            }
            menuInstance.gameObject.SetActive(true);
            _menuStack.Push(menuInstance);
            Debug.Log(string.Format("After push to stack, there are {0} menus in _menuStack", _menuStack.Count));
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