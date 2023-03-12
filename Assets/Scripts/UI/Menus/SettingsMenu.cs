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
        [SerializeField] Slider _UIVolumeSlider;
        [SerializeField] Slider _accelerometerSensitivitySlider;

        private void Start()
        {
            LoadData();
        }

        public void OnMasterVolumeChanged(float volume)
        {
            PlayerPrefs.SetFloat("MasterVolume", volume);
        }

        public void OnSFXVolumeChanged(float volume)
        {
            PlayerPrefs.SetFloat("SFXVolume", volume);
        }

        public void OnMusicVolumeChanged(float volume)
        {
            PlayerPrefs.SetFloat("MusicVolume", volume);
        }

        public void OnUIVolumeChanged(float volume)
        {
            PlayerPrefs.SetFloat("UIVolume", volume);
        }

        public void OnAccelerometerSensitivityChanged(float sensitivity)
        {
            PlayerPrefs.SetFloat("Sensitivity", sensitivity);
        }
        
        public override void OnBackPressed()
        {
            PlayerPrefs.Save();
            base.OnBackPressed();
        }

        //load volume states from stored SaveData object
        public void LoadData()
        {
            _masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
            _SFXVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
            _MusicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
            _UIVolumeSlider.value = PlayerPrefs.GetFloat("UIVolume", 1f);
            _accelerometerSensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity", 1f);
        }
    }
}
