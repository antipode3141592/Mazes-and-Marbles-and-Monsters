using System.Collections;
using System.Collections.Generic;
using System;
using LevelManagement.Levels;

namespace LevelManagement.Data
{
    [Serializable]
    public class SaveData
    {
        public float masterVolume;
        public float sfxVolume;
        public float musicVolume;
        public int highestLevelUnlocked;
        public int playerMaxHealth;
        public int playerDeathCount;
        public LevelSpecs playerCurrentLevel;


        public string hashValue;

        public SaveData()
        {
            masterVolume = 0f;
            sfxVolume = 0f;
            musicVolume = 0f;
            highestLevelUnlocked = 0;
            playerMaxHealth = 2; //default max health is 2 hearts
            playerDeathCount = 0;
            playerCurrentLevel = new LevelSpecs();

            hashValue = "";
        }

    }
}