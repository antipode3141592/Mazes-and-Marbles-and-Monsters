﻿using MarblesAndMonsters.Items;
using System.Collections.Generic;

namespace LevelManagement.DataPersistence
{
    public interface IDataManager
    {
        public string CheckPointLevelId { get; set; }
        public List<KeyItem> CollectedKeys { get; set; }
        public List<LevelSaveData> LevelSaves { get; set; }
        public List<LocationSaveData> LocationSaves { get; set; }
        public float MasterVolume { get; set; }
        public float MusicVolume { get; set; }
        public int PlayerCurrentHealth { get; set; }
        public int PlayerMaxHealth { get; set; }
        public int PlayerScrollCount { get; set; }
        public int PlayerTotalDeathCount { get; set; }
        public string SavedLocation { get; set; }
        public float SFXVolume { get; set; }
        public List<SpellData> UnlockedSpells { get; set; }

        public void Clear();
        public void Load();
        public void Save();
        public void UpdateLevelSaves(LevelSaveData levelData);
        public void UpdateLocationSaves(LocationSaveData locationData);
    }
}