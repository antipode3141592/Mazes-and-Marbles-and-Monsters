using LevelManagement.Data; //for DataManager singleton
using MarblesAndMonsters.Menus; //for MenuManager singelton
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement.Levels
{
    /// <summary>
    /// Clickable Gameobject that represents a location on the map
    /// </summary>
    public class Location : MonoBehaviour
    {
        private LevelManager levelLoader;
        [SerializeField] private string locationId;
        private Sprite sprite;
        [SerializeField] private Sprite occupiedSprite;
        [SerializeField] private Sprite availableSprite;
        [SerializeField] private Sprite completeSprite;

        //upon awake, grab the levelloader reference
        //  preload location summary data
        //  update sprite
        private void Awake()
        {
            levelLoader = FindObjectOfType<LevelManager>();
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
            if (MenuManager.Instance != null)
            {
                MenuManager.Instance.OpenMenu(MenuTypes.MapPopupMenu);
                MapPopupMenu.Instance.locationId.text = locationId;
                MapPopupMenu.Instance.PlayButton.onClick.AddListener(LoadFirstLevel);
                MapPopupMenu.Instance.ResetButton.onClick.AddListener(ResetLocation);
            }
            
            //levelLoader.LoadLevel(levelLoader.GetFirstLevelInLocation(locationName).Id);
        }

        private void LoadFirstLevel()
        {
            LevelSpecs firstLevelSpecs = levelLoader.GetFirstLevelInLocation(locationId);
            ////if 
            levelLoader.LoadLevel(firstLevelSpecs.Id);
        }

        private void ResetLocation()
        {

        }

        public bool GetLocationSummary()
        {
            if (DataManager.Instance != null)
            {
                //get a list of all completed locations from this location
                List<LevelSaveData> levels = DataManager.Instance.LevelSaves.FindAll(x => x.LocationId == locationId && x.Completed == true);
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