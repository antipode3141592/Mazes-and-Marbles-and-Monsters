using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManagement.Levels;
using MarblesAndMonsters.Items;

//code based on the course content at https://www.udemy.com/course/level-management-in-unity/ , which was super helpeful and highly recommended

namespace LevelManagement.DataPersistence
{
    public class DataManager : MonoBehaviour
    {
        private SaveData saveData;
        private JSONSaver jsonSaver;

        public float MasterVolume { get { return saveData.masterVolume; } set { saveData.masterVolume = value; } }
        public float SFXVolume { get { return saveData.sfxVolume; } set { saveData.sfxVolume = value; } }
        public float MusicVolume { get { return saveData.musicVolume; } set { saveData.musicVolume = value; } }
        public string SavedLocation { get { return saveData.currentLocation; } set { saveData.currentLocation = value; } }
        public string CheckPointLevelId { get { return saveData.checkPointLevelId; } set { saveData.checkPointLevelId = value; } }

        public int PlayerCurrentHealth { get { return saveData.playerCurrentHealth; } set { saveData.playerCurrentHealth = value; } }
        public int PlayerMaxHealth { get { return saveData.playerMaxHealth; } set { saveData.playerMaxHealth = value; } }

        public int PlayerTotalDeathCount { get { return saveData.playerDeathCount; } set { saveData.playerDeathCount = value; } }

        public int PlayerScrollCount { get { return saveData.playerScrollCount; } set { saveData.playerScrollCount = value; } }

        public List<LevelSaveData> LevelSaves { get { return saveData.LevelSaves; } set { saveData.LevelSaves = value; } }

        public List<LocationSaveData> LocationSaves { get { return saveData.LocationSaves; } set { saveData.LocationSaves = value; } }

        public List<SpellData> UnlockedSpells { get { return saveData.UnlockedSpells; } set { saveData.UnlockedSpells = value; } }

        public List<KeyItem> CollectedKeys { get { return saveData.CollectedKeys; } set { saveData.CollectedKeys = value; } }

        private void Awake()
        {
            saveData = new SaveData();
            jsonSaver = new JSONSaver();
            Load();
        }

        public void Save()
        {
            jsonSaver.Save(saveData);
        }

        public void UpdateLevelSaves(LevelSaveData levelData)
        {
            //check for Location in LevelSaves
            int index = LevelSaves.FindIndex(x => x.LevelId == levelData.LevelId);
            //if so, update
            if (index >= 0)
            {
                LevelSaves[index] = levelData;
            }
            else
            {
                LevelSaves.Add(levelData);
            }
            
        }

        public void UpdateLocationSaves(LocationSaveData locationData)
        {
            //check for Location in LevelSaves
            int index = LocationSaves.FindIndex(x => x.LocationId == locationData.LocationId);
            //if so, update
            if (index >= 0)
            {
                LocationSaves[index] = locationData;
            }
            else
            {
                LocationSaves.Add(locationData);
            }
        }

        public void Load()
        {
            try
            {
                jsonSaver.Load(saveData);
            }
            catch
            {

            }
        }

        public void Clear()
        {
            saveData = new SaveData();
            jsonSaver.Delete(); //delete saved data
        }
    }
}
