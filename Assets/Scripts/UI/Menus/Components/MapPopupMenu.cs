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
        public Button StartButton;

        LocationSpecs _locationSpecs;

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
            LevelSpecs firstLevelSpecs = _levelManager.GetFirstLevelInLocation(_locationSpecs);
            _gameManager.EnterLocation();
            _levelManager.LoadLevel(firstLevelSpecs.Id);
        }

        public void SetLocationData(LocationSpecs locationSpecs)
        {
            _locationSpecs = locationSpecs;
            List<LevelSaveData> levels = _dataManager.LevelSaves.FindAll(x => x.LocationId == locationSpecs.LocationId && x.Completed == true);
            //count completed levels
            int completedLevels = levels.Count;
            int totalLevels = locationSpecs.LevelSpecs.Count();
            LevelsCompleteText.text = $"{completedLevels} / {totalLevels} levels";
            LocationNameText.text = locationSpecs.DisplayName;
            RelicImage.sprite = locationSpecs.RelicImage;
            RelicsCompleteText.text = $"{locationSpecs.TotalRelics} relics";
            LocationImage.sprite = locationSpecs.Thumbnail;
        }
    }
}
