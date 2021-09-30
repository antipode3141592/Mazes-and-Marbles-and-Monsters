using LevelManagement;
using LevelManagement.DataPersistence; //for DataManager singleton
using LevelManagement.Levels;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.Menus
{
    /// <summary>
    /// Clickable Gameobject that represents a location on the map
    /// </summary>
    public class Location : MonoBehaviour
    {
        
        [SerializeField] private string locationId;
        private Sprite sprite;
        [SerializeField] private Sprite occupiedSprite;
        [SerializeField] private Sprite availableSprite;
        [SerializeField] private Sprite completeSprite;

        protected LevelManager _levelManager;
        protected DataManager _dataManager;
        protected MenuManager _menuManager;

        protected MapPopupMenu _popupMenu;

        //upon awake, grab the levelloader reference
        //  preload location summary data
        //  update sprite
        private void Awake()
        {
            _menuManager = FindObjectOfType<MenuManager>();
            _levelManager = FindObjectOfType<LevelManager>();
            _dataManager = FindObjectOfType<DataManager>();
            _popupMenu = FindObjectOfType<MapPopupMenu>(true);
            sprite = GetComponent<Sprite>();
            
        }

        /// <summary>
        /// Event from clicking on the collider
        /// </summary>
        private void OnMouseDown()
        {
            OpenLocationPopup();
        }

        private void OpenLocationPopup()
        {
            //open map menu
            if (_menuManager != null)
            {
                _menuManager.OpenMenu(MenuTypes.MapPopupMenu);
                _popupMenu.locationId.text = locationId;
                _popupMenu.PlayButton.onClick.AddListener(LoadFirstLevel);
                _popupMenu.ResetButton.onClick.AddListener(ResetLocation);
            }
            
            //levelLoader.LoadLevel(levelLoader.GetFirstLevelInLocation(locationName).Id);
        }

        private void LoadFirstLevel()
        {
            LevelSpecs firstLevelSpecs = _levelManager.GetFirstLevelInLocation(locationId);
            ////if 
            _levelManager.LoadLevel(firstLevelSpecs.Id);
        }

        private void ResetLocation()
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