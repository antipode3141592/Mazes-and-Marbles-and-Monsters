using LevelManagement.Menus;
using UnityEngine;
using UnityEngine.UI;

namespace MarblesAndMonsters.Menus
{
    public class SettingsMenu : Menu<SettingsMenu>
    {
        [SerializeField] Slider _masterVolumeSlider;
        [SerializeField] Slider _SFXVolumeSlider;
        [SerializeField] Slider _MusicVolumeSlider;
        [SerializeField] Slider _accelerometerSensitivitySlider;

        private void Start()
        {
            LoadData();
        }

        public void OnMasterVolumeChanged(float volume)
        {
            if (_dataManager is null)
                return;
            _dataManager.MasterVolume = volume;
        }

        public void OnSFXVolumeChanged(float volume)
        {
            if (_dataManager is null)
                return;
            _dataManager.SFXVolume = volume;
        }

        public void OnMusicVolumeChanged(float volume)
        {
            if (_dataManager is null)
                return;
            _dataManager.MusicVolume = volume;
        }

        public void OnAccelerometerSensitivityChanged(float sensitivity)
        {
            if (_dataManager is null)
                return;
            _dataManager.AccelerometerSensitivity = sensitivity;
        }

        
        public override void OnBackPressed()
        {
            _dataManager.Save();
            base.OnBackPressed();
        }

        //load volume states from stored SaveData object
        public void LoadData()
        {
            _dataManager.Load();
            _masterVolumeSlider.value = _dataManager.MasterVolume;
            _SFXVolumeSlider.value = _dataManager.SFXVolume;
            _MusicVolumeSlider.value = _dataManager.MusicVolume;
            _accelerometerSensitivitySlider.value = _dataManager.AccelerometerSensitivity;
        }
    }
}
