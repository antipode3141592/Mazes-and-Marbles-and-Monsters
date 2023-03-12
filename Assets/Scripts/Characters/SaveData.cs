using MarblesAndMonsters.Items;
using System;
using System.Collections.Generic;

//code based on the course content at https://www.udemy.com/course/level-management-in-unity/ , which was super helpeful and highly recommended

namespace LevelManagement.DataPersistence
{
    [Serializable]
    public class SaveData
    {
        //general game settings
        public float totalGameTime;

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
        public int playerScrollCount;

        //spells
        public List<SpellData> UnlockedSpells;
        //keys
        public List<KeyItem> CollectedKeys;

        public string hashValue;    //for verifying save file integrity

        public SaveData()
        {
            totalGameTime = 0f;
            playerMaxHealth = 3; //default max health is 3 hearts
            playerDeathCount = 0;
            playerScrollCount = 0;
            hashValue = "";
            LevelSaves = new List<LevelSaveData>();
            LocationSaves = new List<LocationSaveData>();
            UnlockedSpells = new List<SpellData>();
            CollectedKeys = new List<KeyItem>();
        }
    }

    

}