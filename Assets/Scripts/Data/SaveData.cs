using System.Collections;
using System.Collections.Generic;
using System;
using LevelManagement.Levels;

//code based on the course content at https://www.udemy.com/course/level-management-in-unity/ , which was super helpeful and highly recommended

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

        //level data
        public string currentCampaign;
        public string currentLevel;

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