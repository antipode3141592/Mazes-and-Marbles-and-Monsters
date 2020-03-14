using SampleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LevelManagement.Data;

namespace LevelManagement
{
    public class MainMenu : Menu<MainMenu>
    {
        [SerializeField]
        private float _playDelay = 0.5f;
        [SerializeField]
        private TransitionFader startTransitionPrefab;

        private LevelLoader levelLoader;

        protected override void Awake()
        {
            base.Awake();
            
        }

        public void Start()
        {
            levelLoader = GameObject.FindObjectOfType<LevelLoader>();
            //LoadData();
        }

        private void LoadData()
        {
            //if (playerNameInputField == null)
            //{
            //    return;
            //} else
            //{
            //    DataManager.Instance.Load();
            //    playerNameInputField.SetTextWithoutNotify(DataManager.Instance.PlayerName);
            //}

        }

        public void SaveData()
        {
            DataManager.Instance.Save();
        }

        public void OnPlayPressed()
        {
            StartCoroutine(OnPlayPressedRoutine());
        }

        private IEnumerator OnPlayPressedRoutine()
        {
            TransitionFader.PlayTransition(startTransitionPrefab);
            //LevelLoader.LoadNextLevel();
            levelLoader.LoadNextLevel();
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
    }
}