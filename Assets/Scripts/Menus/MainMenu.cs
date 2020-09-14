using LevelManagement.Data;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LevelManagement.Menus
{
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

        protected override void Awake()
        {
            base.Awake();
            
        }

        public void Start()
        {
            levelLoader = GameObject.FindObjectOfType<LevelLoader>();
            LoadData();

            if (DataManager.Instance != null)
            {
                currentGameGroup.SetActive(true);
                resumeButton.SetActive(true);
                treasureCount.text = "Treasures Collected: " + DataManager.Instance.PlayerTreasureCount;
                currentLevel.text = "Current Level: " + (DataManager.Instance.CurrentLevelSpecs.LevelName);
                playerHealth.text = "Max Health: " + DataManager.Instance.PlayerMaxHealth;
                deathCount.text = "Deaths: " + DataManager.Instance.PlayerTotalDeathCount;

            } else
            {
                currentGameGroup.SetActive(false);
                resumeButton.SetActive(false);
            }
        }

        private void LoadData()
        {
            DataManager.Instance.Load();
        }

        public void SaveData()
        {
            DataManager.Instance.Save();
        }

        public void ResetData()
        {
            //TODO should confirm first
            DataManager.Instance.Clear();
        }

        public void OnResumePressed()
        {
            StartCoroutine(OnResumePressedRoutine());
        }

        private IEnumerator OnResumePressedRoutine()
        {
            TransitionFader.PlayTransition(startTransitionPrefab);
            levelLoader.LoadLevel(DataManager.Instance.CurrentLevelSpecs.SceneName);
            yield return new WaitForSeconds(_playDelay);
            GameMenu.Open();
        }

        public void OnPlayPressed()
        {
            StartCoroutine(OnPlayPressedRoutine());
        }

        private IEnumerator OnPlayPressedRoutine()
        {
            TransitionFader.PlayTransition(startTransitionPrefab);
            //levelLoader.LoadNextLevel();
            yield return new WaitForSeconds(_playDelay);
            GameMenu.Open();
        }

        public void OnSettingsPressed()
        {
            SettingsMenu.Open();
        }

        public void OnCreditsPressed()
        {
            CreditsMenu.Open();
        }

        public override void OnBackPressed()
        {
            SaveData();
            Application.Quit();
        }

        public void OnResetSaveDataPressed()
        {
            ResetData();
        }
    }
}