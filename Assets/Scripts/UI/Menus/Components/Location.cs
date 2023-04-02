using LevelManagement;
using LevelManagement.DataPersistence; //for DataManager singleton
using LevelManagement.Levels;
using UnityEngine;
using Zenject;

namespace MarblesAndMonsters.Menus
{
    public class Location : MonoBehaviour
    {
        
        [SerializeField] LocationSpecs locationSpecs;
        [SerializeField] Sprite occupiedSprite;
        [SerializeField] Sprite availableSprite;
        [SerializeField] Sprite completeSprite;

        [SerializeField] SpriteRenderer foregroundRenderer;
        [SerializeField] SpriteRenderer highlightRenderer;

        protected ILevelManager _levelManager;
        protected IDataManager _dataManager;
        protected IMenuManager _menuManager;

        protected MapPopupMenu _popupMenu;

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
        }

        void Start()
        {
            var locationSave = _dataManager.LocationSaves.Find(x => x.LocationId == locationSpecs.LocationId);
            if (locationSave is null)
                SetState(isOccupied: false, isAvailable: locationSpecs.IsAvailable, isComplete: false);
            else
                SetState(isOccupied: _dataManager.CurrentLocationId == locationSpecs.LocationId,
                    isAvailable: locationSave.IsAvailable || locationSpecs.IsAvailable,
                    isComplete: locationSave.Completed);
        }

        void OnMouseUp()
        {
            _popupMenu.gameObject.SetActive(true);
            _popupMenu.SetLocationData(locationSpecs);
        }

        void SetState(bool isOccupied, bool isAvailable, bool isComplete)
        {
            if (Debug.isDebugBuild)
                Debug.Log($"SetState({isOccupied}, {isAvailable}, {isComplete})", this);
            highlightRenderer.color = isOccupied ? Color.white : Color.clear;
            foregroundRenderer.color = isAvailable ? Color.white : Color.clear;
            foregroundRenderer.sprite = isComplete ? completeSprite : availableSprite;
        }
    }
}