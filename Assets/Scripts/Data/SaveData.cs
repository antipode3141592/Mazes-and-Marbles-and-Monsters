using System;
using System.Collections.Generic;

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

        //level data
        public string currentLocation;
        public string checkPointLevelId; //game restores to this level (after load, for example)

        //completed levels data
        public List<LevelSaveData> LevelSaves;
        public List<LocationSaveData> LocationSaves;

        //player stats
        public int playerCurrentHealth;
        public int playerMaxHealth;
        public int playerDeathCount;
        public int playerTreasureCounter;

        public string hashValue;    //for verifying save file integrity

        public SaveData()
        {
            masterVolume = 0f;
            sfxVolume = 0f;
            musicVolume = 0f;
            playerMaxHealth = 3; //default max health is 3 hearts
            playerDeathCount = 0;
            playerTreasureCounter = 0;
            hashValue = "";
            LevelSaves = new List<LevelSaveData>();
            LocationSaves = new List<LocationSaveData>();
        }

    }
}