using LevelManagement.Menus;
using UnityEngine;
using LevelManagement.DataPersistence;
using LevelManagement;
using System.Collections;
using UnityEngine.UI;

namespace MarblesAndMonsters.Menus
{

    //Main Menu Controller
    //  Allow player to:
    //      -start a new game
    //      -continue an old game
    //      -continue the current game
    //      -change game settings
    //      -view credits
    public class MainMenu : Menu<MainMenu>
    {
        [SerializeField]
        private float _playDelay = 0.5f;
        [SerializeField]
        private TransitionFader startTransitionPrefab;
        [SerializeField]
        private Text currentLevel;
        [SerializeField]
        private Text playerHealth;
        [SerializeField]
        private Text scrollCount;
        [SerializeField]
        private Text deathCount;
        [SerializeField]
        private GameObject currentGameGroup;
        [SerializeField]
        private GameObject resumeButton;

        protected void Start()
        {
            UpdateCurrentGameStats();
        }

        public void UpdateCurrentGameStats()
        {
            if (_dataManager != null && JSONSaver.CheckSaveFile())
            {
                currentGameGroup.SetActive(true);
                resumeButton.SetActive(true);
               
                scrollCount.text = "Scrolls Collected: " + _dataManager.PlayerScrollCount;
                currentLevel.text = string.Format("Current Level: {0} - {1}",
                    _dataManager.SavedLocation, 
                    _dataManager.CheckPointLevelId);
                playerHealth.text = "Health: " + _dataManager.PlayerMaxHealth;
                deathCount.text = "Deaths: " + _dataManager.PlayerTotalDeathCount;
            }
            else
            {
                currentGameGroup.SetActive(false);
                resumeButton.SetActive(false);
            }
        }

        public void SaveData()
        {
            if (_dataManager != null)
            {
                _dataManager.Save();
            }
        }

        public void ResetData()
        {
            //TODO should confirm first
            if (_dataManager != null)
            {
                _dataManager.Clear();
            }
            //update UI
            UpdateCurrentGameStats();
        }

        public void OnResumePressed()
        {
            StartCoroutine(OnResumePressedRoutine());
        }

        private IEnumerator OnResumePressedRoutine()
        {
            //TransitionFader.PlayTransition(startTransitionPrefab);
            if (_dataManager != null)
            {
                _levelManager.LoadLevel(_dataManager.CheckPointLevelId);
                _menuManager.OpenMenu(MenuTypes.GameMenu);
                yield return new WaitForSeconds(_playDelay);
            }
        }

        public void OnPlayPressed()
        {
            ResetData();
            StartCoroutine(OnPlayPressedRoutine());
        }

        private IEnumerator OnPlayPressedRoutine()
        {
            //TransitionFader.PlayTransition(startTransitionPrefab);
            //load first level
            //levelLoader.LoadLevel(0);  //load first level in the level list
            //levelLoader.LoadNextLevel();
            _levelManager.LoadLevel(_levelManager.GetFirstLevel().Id);
            _menuManager.OpenMenu(MenuTypes.GameMenu);
            yield return new WaitForSeconds(_playDelay);
            //base.OnBackPressed();
        }

        public void OnSettingsPressed()
        {
            //SettingsMenu.Open();
            _menuManager.OpenMenu(MenuTypes.SettingsMenu);
        }

        public void OnCreditsPressed()
        {
            //CreditsMenu.Open();
            _menuManager.OpenMenu(MenuTypes.CreditsMenu);
        }

        public override void OnBackPressed()
        {
            SaveData();
            Application.Quit();
        }

        public void OnResetSaveDataPressed()
        {
            //clear data
            ResetData();
        }
    }
}