using LevelManagement.Menus;
using UnityEngine;
using LevelManagement.Data;
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
        private Text treasureCount;
        [SerializeField]
        private Text deathCount;
        [SerializeField]
        private GameObject currentGameGroup;
        [SerializeField]
        private GameObject resumeButton;

        private LevelLoader levelLoader;
        
        protected void OnEnable()
        {
            LoadData();
        }

        protected override void Awake()
        {
            base.Awake();
            levelLoader = GameObject.FindObjectOfType<LevelLoader>();
            //LoadData();
            UpdateCurrentGameStats();
        }

        public void UpdateCurrentGameStats()
        {
            if (DataManager.Instance != null && JSONSaver.CheckSaveFile())
            {
                currentGameGroup.SetActive(true);
                resumeButton.SetActive(true);
                treasureCount.text = "Scrolls Collected: " + DataManager.Instance.PlayerTreasureCount;
                currentLevel.text = string.Format("Current Level: {0} - {1}",
                    DataManager.Instance.SavedLocation, 
                    DataManager.Instance.SavedLevelId);
                playerHealth.text = "Max Health: " + DataManager.Instance.PlayerMaxHealth;
                deathCount.text = "Deaths: " + DataManager.Instance.PlayerTotalDeathCount;

            }
            else
            {
                currentGameGroup.SetActive(false);
                resumeButton.SetActive(false);
            }
        }

        private void LoadData()
        {
            if (DataManager.Instance != null)
            {
                DataManager.Instance.Load();
            }
        }

        public void SaveData()
        {
            DataManager.Instance.Save();
        }

        public void ResetData()
        {
            //TODO should confirm first
            if (DataManager.Instance != null)
            {
                DataManager.Instance.Clear();
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
            if (DataManager.Instance != null)
            {
                levelLoader.LoadLevel(DataManager.Instance.SavedLevelId);
                yield return new WaitForSeconds(_playDelay);
            }
            //GameMenu.Open();
        }

        public void OnPlayPressed()
        {
            StartCoroutine(OnPlayPressedRoutine());
        }

        private IEnumerator OnPlayPressedRoutine()
        {
            //TransitionFader.PlayTransition(startTransitionPrefab);
            //load first level
            //levelLoader.LoadLevel(0);  //load first level in the level list
            levelLoader.LoadNextLevel();
            yield return new WaitForSeconds(_playDelay);
            //base.OnBackPressed();
        }

        public void OnSettingsPressed()
        {
            //SettingsMenu.Open();
            MenuManager.Instance.OpenMenu(MenuTypes.SettingsMenu);
        }

        public void OnCreditsPressed()
        {
            //CreditsMenu.Open();
            MenuManager.Instance.OpenMenu(MenuTypes.CreditsMenu);
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