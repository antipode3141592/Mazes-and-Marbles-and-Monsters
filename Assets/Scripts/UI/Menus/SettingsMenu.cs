using LevelManagement.DataPersistence; //include data classes
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
            _dataManager.MasterVolume = volume;
        }

        public void OnSFXVolumeChanged(float volume)
        {
            //if (_dataManager != null)
            //{
            //    _dataManager.SFXVolume = volume;
            //}
            _dataManager.SFXVolume = volume;
        }

        public void OnMusicVolumeChanged(float volume)
        {
            //if (_dataManager != null)
            //{
            //    _dataManager.MusicVolume = volume;
            //}
            _dataManager.MusicVolume = volume;
        }

        
        public override void OnBackPressed()
        {
            //_dataManager.Save();
            _dataManager.Save();
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
                _dataManager.Load();
                _masterVolumeSlider.value = _dataManager.MasterVolume;
                _SFXVolumeSlider.value = _dataManager.SFXVolume;
                _MusicVolumeSlider.value = _dataManager.MusicVolume;
            }
        }
    }
}
