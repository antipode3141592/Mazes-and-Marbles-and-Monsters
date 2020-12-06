using UnityEngine;
using MarblesAndMonsters.Menus;

//code based on the course content at https://www.udemy.com/course/level-management-in-unity/ , which was super helpeful and highly recommended
//  Class Name:  Menu
//  Interacts with:  MenuManager
//  Purpose:  Menu type for all Menu objects
//      a)  Menus are singletons
//      b)  Menu is an abstract class

namespace LevelManagement.Menus
{
    public abstract class Menu<T>: Menu where T : Menu<T>
    {
        private static T _instance;

        public static T Instance
        {
            get { return _instance; }
        }


        // if instance doesn't exist, create it and set to Don't Destroy On Load 
        //  (else destroy the attached gameObject)
        protected virtual void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = (T)this;
                DontDestroyOnLoad(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }

        public static void Open()
        {
            if (MenuManager.Instance != null && Instance != null)
            {
                MenuManager.Instance.OpenMenu(Instance);
            }
        }
    }

    [RequireComponent(typeof(Canvas))]
    public abstract class Menu : MonoBehaviour
    {
        public virtual void OnBackPressed()
        {
            MenuManager.Instance.CloseMenu();
        }
    }


}
