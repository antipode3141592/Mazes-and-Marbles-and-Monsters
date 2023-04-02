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
        public float totalGameTime = 0f;

        //level data
        public string currentLocationId = string.Empty;
        public string checkPointLevelId = string.Empty; //game restores to this level (after load, for example)

        //completed levels data
        public List<LevelSaveData> LevelSaves = new();
        public List<LocationSaveData> LocationSaves = new();

        //player stats
        public int playerCurrentHealth = 3;
        public int playerMaxHealth = 3;
        public int playerDeathCount = 0;
        public int playerScrollCount = 0;

        //spells
        public List<SpellData> UnlockedSpells = new();
        //keys
        public List<KeyItem> CollectedKeys = new();

        public string hashValue = string.Empty;    //for verifying save file integrity

        public SaveData()
        {
        }
    }

    

}