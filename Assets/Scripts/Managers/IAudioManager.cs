using UnityEngine;

namespace MarblesAndMonsters.Managers
{
    public interface IAudioManager
    {
        public void PlayMusic(AudioClip musicClip);
        public void SetMasterLevel(float level);
        public void SetMusicLevel(float level);
        public void SetSFXLevel(float level);
        void SetUILevel(float level);
    }
}