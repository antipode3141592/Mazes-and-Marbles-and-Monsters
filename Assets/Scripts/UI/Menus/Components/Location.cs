using LevelManagement;
using LevelManagement.DataPersistence; //for DataManager singleton
using LevelManagement.Levels;
using UnityEngine;
using Zenject;

namespace MarblesAndMonsters.Menus
{
    public class Location : MonoBehaviour
    {
        
        [SerializeField] string locationId;
        [SerializeField] Sprite occupiedSprite;
        [SerializeField] Sprite availableSprite;
        [SerializeField] Sprite completeSprite;

        protected ILevelManager _levelManager;
        protected IDataManager _dataManager;
        protected IMenuManager _menuManager;

        protected MapPopupMenu _popupMenu;

        MainMap _mainMap;

        [Inject]
        public void Init(ILevelManager levelManager, IDataManager dataManager, IMenuManager menuManager)
        {
            _levelManager = levelManager;
            _dataManager = dataManager;
            _menuManager = menuManager;
        }

        //upon awake, grab the levelloader reference
        //  preload location summary data
        //  update sprite
        void Awake()
        {
            _popupMenu = FindObjectOfType<MapPopupMenu>(true);
            _mainMap = FindObjectOfType<MainMap>(true);
        }

        void OnMouseUp()
        {
            _popupMenu.gameObject.SetActive(true);
            _popupMenu.SetLocationData(locationId);
        }
    }
}