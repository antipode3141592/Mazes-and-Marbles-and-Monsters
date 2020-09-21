using System.Collections;
using System.Collections.Generic;
using System;
using LevelManagement.Levels;

namespace LevelManagement.Data
{
    [Serializable]
    public class SaveData
    {
        //general game settings
        public float masterVolume;
        public float sfxVolume;
        public float musicVolume;
        public LevelSpecs playerCurrentLevelSpecs;  //current level info

        public int playerMaxHealth;
        public int playerDeathCount;
        public int playerTreasureCounter;

        public string hashValue;

        public SaveData()
        {
            masterVolume = 0f;
            sfxVolume = 0f;
            musicVolume = 0f;
            //CurrentLevelIndex = 0;
            playerMaxHealth = 3; //default max health is 2 hearts
            playerDeathCount = 0;
            playerTreasureCounter = 0;
            playerCurrentLevelSpecs = new LevelSpecs();
            hashValue = "";
        }

    }
}