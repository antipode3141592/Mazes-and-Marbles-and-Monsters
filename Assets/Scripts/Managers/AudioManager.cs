using UnityEngine;
using UnityEngine.Audio;

namespace MarblesAndMonsters.Managers
{
    public class AudioManager: MonoBehaviour, IAudioManager
    {
        [SerializeField] AudioMixer audioMixer;
        [SerializeField] AudioMixerGroup masterGroup;
        [SerializeField] AudioMixerGroup sfxGroup;
        [SerializeField] AudioMixerGroup musicGroup;
        [SerializeField] AudioMixerGroup uiGroup;

        AudioSource _audioSource;

        void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void SetMasterLevel(float level)
        {
            masterGroup.audioMixer.SetFloat("MasterVolume", level);
        }

        public void SetSFXLevel(float level)
        {
            sfxGroup.audioMixer.SetFloat("SfxVolume", level);
        }

        public void SetMusicLevel(float level)
        {
            musicGroup.audioMixer.SetFloat("MusicVolume", level);
        }

        public void SetUILevel(float level)
        {
            uiGroup.audioMixer.SetFloat("UiVolume", level);
        }

        public void PlayMusic(AudioClip musicClip)
        {
            _audioSource.clip = musicClip;
            _audioSource.Play();
        }
    }
}