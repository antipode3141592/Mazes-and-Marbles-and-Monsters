using System;
using System.Collections.Generic;
using MarblesAndMonsters;
using MarblesAndMonsters.Items;

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
        public int playerScrollCount;

        //spells
        public List<SpellData> UnlockedSpells;
        //keys
        public List<KeyItem> CollectedKeys;

        public string hashValue;    //for verifying save file integrity

        public SaveData()
        {
            masterVolume = 0f;
            sfxVolume = 0f;
            musicVolume = 0f;
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

    [Serializable]
    public class SpellData
    {
        public SpellName SpellName;
        public SpellStats SpellStats;
        public bool IsAssigned;
        public int QuickSlot;

        public SpellData(SpellName spellName, SpellStats spellStats, bool isAssigned = false, int quickSlot = -1)
        {
            SpellName = spellName;
            SpellStats = spellStats;
            IsAssigned = isAssigned;
            QuickSlot = quickSlot;
        }
    }

}