using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManagement.Levels;

//code based on the course content at https://www.udemy.com/course/level-management-in-unity/ , which was super helpeful and highly recommended

namespace LevelManagement.Data
{
    public class DataManager : MonoBehaviour
    {
        private SaveData saveData;
        private JSONSaver jsonSaver;

        public float MasterVolume { get { return saveData.masterVolume; } set { saveData.masterVolume = value; } }
        public float SFXVolume { get { return saveData.sfxVolume; } set { saveData.sfxVolume = value; } }
        public float MusicVolume { get { return saveData.musicVolume; } set { saveData.musicVolume = value; } }
        //public int HigestLevelUnlocked { get { return saveData.CurrentLevelIndex; } set { saveData.CurrentLevelIndex = value; } }

        public LevelSpecs CurrentLevelSpecs { get { return saveData.playerCurrentLevelSpecs; } set { saveData.playerCurrentLevelSpecs = value; } }

        public int PlayerMaxHealth { get { return saveData.playerMaxHealth; } set { saveData.playerMaxHealth = value; } }

        public int PlayerTotalDeathCount { get { return saveData.playerDeathCount; }  set { saveData.playerDeathCount = value; } }

        public int PlayerTreasureCount { get { return saveData.playerTreasureCounter; } set { saveData.playerTreasureCounter = value; } }

        private static DataManager _instance;
        public static DataManager Instance
        {
            get { return _instance;  }
        }

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                saveData = new SaveData();
                jsonSaver = new JSONSaver();
            }
        }
        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }

        public void Save()
        {
            jsonSaver.Save(saveData);
        }

        public void Load()
        {
            jsonSaver.Load(saveData);
        }

        public void Clear()
        {
            jsonSaver.Delete(); //delete saved data
        }
    }
}
