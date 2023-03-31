using LevelManagement;
using LevelManagement.DataPersistence; //for datamanager
using LevelManagement.Levels;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MarblesAndMonsters.Menus
{
    public class MapPopupMenu : MonoBehaviour
    {
        IDataManager _dataManager;
        ILevelManager _levelManager;
        IGameManager _gameManager;

        public Text LocationNameText;
        public Text LocationDescriptionText;
        public Image LocationImage;
        public Text LevelsCompleteText;
        public Text RelicsCompleteText;
        public Image RelicImage;
        public Button PlayButton;
        public Button ResetButton;

        string _locationId;

        [Inject]
        public void Init(IDataManager dataManager, ILevelManager levelManager, IGameManager gameManager)
        {
            _dataManager = dataManager;
            _levelManager = levelManager;
            _gameManager = gameManager;
        }

        void Start()
        {
            gameObject.SetActive(false);
        }

        public void OnClosePopupClicked()
        {
            gameObject.SetActive(false);
        }

        public void OnPlayClicked()
        {
            LoadFirstLevel();
        }

        void LoadFirstLevel()
        {
            LevelSpecs firstLevelSpecs = _levelManager.GetFirstLevelInLocation(_locationId);
            _gameManager.EnterLocation();
            _levelManager.LoadLevel(firstLevelSpecs.Id);
        }


        public void OnResetClicked()
        {

        }

        public void SetLocationData(string locationId)
        {
            _locationId = locationId;
            List<LevelSaveData> levels = _dataManager.LevelSaves.FindAll(x => x.LocationId == locationId && x.Completed == true);
            //count completed levels
            int completedLevels = levels.Count;
            int totalLevels = _levelManager.LevelSpecsById.Count(x => x.Value.LocationId == locationId);
            LevelsCompleteText.text = $"{completedLevels} / {totalLevels} levels";
            LocationSpecs locationSpecs = _levelManager.LocationSpecs.Find(x => x.LocationId == locationId);
            LocationNameText.text = locationSpecs.DisplayName;
            RelicImage.sprite = locationSpecs.RelicImage;
            RelicsCompleteText.text = $"{locationSpecs.TotalRelics} relics";
            LocationImage.sprite = locationSpecs.Thumbnail;
        }
    }
}
