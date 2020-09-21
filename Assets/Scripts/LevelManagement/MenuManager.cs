using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using LevelManagement.Menus;

//  Class Name:  MenuManager
//  Interacts with:  Menu
//  Purpose:  A UI controller with 
//  

namespace LevelManagement.Menus
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField]
        private MainMenu mainMenuPrefab;
        [SerializeField]
        private SettingsMenu settingsMenuPrefab;
        [SerializeField]
        private CreditsMenu creditsScreenPrefab;
        [SerializeField]
        private GameMenu gameMenuPrefab;    //the ingame UI
        [SerializeField]
        private PauseMenu pauseMenuPrefab;
        [SerializeField]
        private WinMenu winMenuPrefab;

        [SerializeField]
        private Transform _menuParent;

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
                InitializeMenus();
            }
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
            if (_menuParent == null)
            {
                GameObject menuParentObject = new GameObject("Menus");
                _menuParent = menuParentObject.transform;
            }
            Object.DontDestroyOnLoad(_menuParent.gameObject);   //protect the parent

            //use reflection to get menus
            System.Type myType = this.GetType();
            BindingFlags myFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
            FieldInfo[] fields = myType.GetFields(myFlags);

            foreach (FieldInfo field in fields)
            {
                Menu prefab = field.GetValue(this) as Menu;
                if (prefab != null)
                {

                    Menu menuInstance = Instantiate(prefab, _menuParent);
                    if (prefab != mainMenuPrefab)
                    {
                        menuInstance.gameObject.SetActive(false);
                    } else if (SceneManager.GetActiveScene().buildIndex <= 1)
                    {
                        Debug.Log(string.Format("Active Scene Index: {0}", SceneManager.GetActiveScene().buildIndex));
                        OpenMenu(menuInstance);
                    } else
                    {
                        menuInstance.gameObject.SetActive(false);
                        OpenMenu(gameMenuPrefab);
                    }
                }
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
                    menu.gameObject.SetActive(false);
                }
            }

            menuInstance.gameObject.SetActive(true);
            _menuStack.Push(menuInstance);
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