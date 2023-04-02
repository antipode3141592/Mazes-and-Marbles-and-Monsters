using MarblesAndMonsters.Items;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

//code based on the course content at https://www.udemy.com/course/level-management-in-unity/ , which was super helpeful and highly recommended

namespace LevelManagement.DataPersistence
{
    public class DataManager : MonoBehaviour, IDataManager
    {
        ILevelManager _levelManager;

        SaveData saveData;
        JSONSaver jsonSaver;

        public float TotalGameTime { get => saveData.totalGameTime; set => saveData.totalGameTime = value; }
        public string CurrentLocationId { get { return saveData.currentLocationId; } set { saveData.currentLocationId = value; } }
        public string CurrentLevelId { get { return saveData.checkPointLevelId; } set { saveData.checkPointLevelId = value; } }
        public int PlayerCurrentHealth { get { return saveData.playerCurrentHealth; } set { saveData.playerCurrentHealth = value; } }
        public int PlayerMaxHealth { get { return saveData.playerMaxHealth; } set { saveData.playerMaxHealth = value; } }
        public int PlayerTotalDeathCount { get { return saveData.playerDeathCount; } set { saveData.playerDeathCount = value; } }
        public int PlayerScrollCount { get { return saveData.playerScrollCount; } set { saveData.playerScrollCount = value; } }
        public List<LevelSaveData> LevelSaves { get { return saveData.LevelSaves; } set { saveData.LevelSaves = value; } }
        public List<LocationSaveData> LocationSaves { get { return saveData.LocationSaves; } set { saveData.LocationSaves = value; } }
        public List<SpellData> UnlockedSpells { get { return saveData.UnlockedSpells; } set { saveData.UnlockedSpells = value; } }
        public List<KeyItem> CollectedKeys { get { return saveData.CollectedKeys; } set { saveData.CollectedKeys = value; } }

        [Inject]
        public void Init(ILevelManager levelManager)
        {
            _levelManager = levelManager;
        }

        void Awake()
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
            TotalGameTime = LevelSaves.Sum(x => x.ElapsedGameTimeInSeconds);
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

        public bool Load()
        {
            return jsonSaver.Load(saveData);
        }

        public void Clear()
        {
            saveData = new SaveData();
            jsonSaver.Delete(); //delete saved data
        }
    }
}
