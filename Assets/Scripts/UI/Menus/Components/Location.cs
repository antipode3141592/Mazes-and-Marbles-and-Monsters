using LevelManagement;
using LevelManagement.DataPersistence; //for DataManager singleton
using LevelManagement.Levels;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.Menus
{
    public class Location : MonoBehaviour
    {
        
        [SerializeField] string locationId;
        Sprite sprite;
        [SerializeField] Sprite occupiedSprite;
        [SerializeField] Sprite availableSprite;
        [SerializeField] Sprite completeSprite;

        protected ILevelManager _levelManager;
        protected IDataManager _dataManager;
        protected IMenuManager _menuManager;

        protected MapPopupMenu _popupMenu;

        //upon awake, grab the levelloader reference
        //  preload location summary data
        //  update sprite
        void Awake()
        {
            _menuManager = FindObjectOfType<MenuManager>();
            _levelManager = FindObjectOfType<LevelManager>();
            _dataManager = FindObjectOfType<DataManager>();
            _popupMenu = FindObjectOfType<MapPopupMenu>(true);
            sprite = GetComponent<Sprite>();
            
        }

        void OnMouseDown()
        {
            OpenLocationPopup();
        }

        void OpenLocationPopup()
        {
            if (_menuManager != null)
            {
                _menuManager.OpenMenu(MenuTypes.MapPopupMenu);
                _popupMenu.locationId.text = locationId;
                _popupMenu.PlayButton.onClick.AddListener(LoadFirstLevel);
                _popupMenu.ResetButton.onClick.AddListener(ResetLocation);
            }
            
            //levelLoader.LoadLevel(levelLoader.GetFirstLevelInLocation(locationName).Id);
        }

        void LoadFirstLevel()
        {
            LevelSpecs firstLevelSpecs = _levelManager.GetFirstLevelInLocation(locationId);
            _levelManager.LoadLevel(firstLevelSpecs.Id);
        }

        void ResetLocation()
        {

        }

        public bool GetLocationSummary()
        {
            if (_dataManager != null)
            {
                //get a list of all completed locations from this location
                List<LevelSaveData> levels = _dataManager.LevelSaves.FindAll(x => x.LocationId == locationId && x.Completed == true);
                //count completed levels
                int completedLevels = levels.Count;
                //count total non-hidden levels (sortorder >= 0)

                //get latest level
                //string currentLevel = levels.Find(x => x.sor)
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}