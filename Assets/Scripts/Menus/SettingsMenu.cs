using LevelManagement.Data; //include data classes
using LevelManagement.Menus;
using UnityEngine;
using UnityEngine.UI;

namespace MarblesAndMonsters.Menus
{
    public class SettingsMenu : Menu<SettingsMenu>
    {
        [SerializeField]
        private Slider _masterVolumeSlider;

        [SerializeField]
        private Slider _SFXVolumeSlider;

        [SerializeField]
        private Slider _MusicVolumeSlider;

        //private DataManager _dataManager;

        protected override void Awake()
        {
            base.Awake();
            //_dataManager = Object.FindObjectOfType<DataManager>();
        }

        private void Start()
        {
            LoadData();
        }

        public void OnMasterVolumeChanged(float volume)
        {
            //if (_dataManager != null)
            //{
            //    _dataManager.MasterVolume = volume;
            //}
            DataManager.Instance.MasterVolume = volume;
        }

        public void OnSFXVolumeChanged(float volume)
        {
            //if (_dataManager != null)
            //{
            //    _dataManager.SFXVolume = volume;
            //}
            DataManager.Instance.SFXVolume = volume;
        }

        public void OnMusicVolumeChanged(float volume)
        {
            //if (_dataManager != null)
            //{
            //    _dataManager.MusicVolume = volume;
            //}
            DataManager.Instance.MusicVolume = volume;
        }

        
        public override void OnBackPressed()
        {
            //_dataManager.Save();
            DataManager.Instance.Save();
            base.OnBackPressed();
        }

        //load volume states from stored SaveData object
        public void LoadData()
        {
            if (_masterVolumeSlider == null || _SFXVolumeSlider == null || _MusicVolumeSlider == null)
            {
                return;
            }
            else
            {
                DataManager.Instance.Load();
                _masterVolumeSlider.value = DataManager.Instance.MasterVolume;
                _SFXVolumeSlider.value = DataManager.Instance.SFXVolume;
                _MusicVolumeSlider.value = DataManager.Instance.MusicVolume;
            }
        }
    }
}
