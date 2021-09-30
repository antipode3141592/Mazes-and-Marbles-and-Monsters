﻿using LevelManagement.DataPersistence;
using LevelManagement.Levels;
using MarblesAndMonsters;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace LevelManagement
{

    //
    public class LevelManager : MonoBehaviour
    {
        private static readonly int mainMenuIndex = 1;   //splash screen is build index 0
        [SerializeField] private LevelList levelList;    //scriptable object with all accessible levels
        [SerializeField] private TransitionFader levelLoadTransition;

        private Dictionary<string, LevelSpecs> levelSpecsById = new Dictionary<string, LevelSpecs>(); // key is string Id

        protected DataManager _dataManager;
        protected GameManager _gameManager;

        protected string _currentLevelName = string.Empty;   //default to empty

        [Inject]
        public void Init(DataManager dataManager, GameManager gameManager)
        {
            _dataManager = dataManager;
            _gameManager = gameManager;
        }

        protected void Awake()
        {
            //build dictionary with key equal to Id field in LevelSpecs
            //levelSpecsById = new Dictionary<string, LevelSpecs>();
            foreach (LevelSpecs level in levelList.Levels)
                {
                    levelSpecsById.Add(level.Id, level);
                }
            Debug.Log(string.Format("Level Dictionary has {0} key-value pairs", levelSpecsById.Count));
        }

        private void Start()
        {
            //if there are two scenes open OnStart, it's a debug/playtest situation where the 
            // scene level is all ready loaded, so we need to trigger start of level manually here
            if (SceneManager.sceneCount == 2)
            {
                _gameManager.ShouldBeginLevel = true;
            }
        }

        /// <summary>
        /// Load a specific scene by levelId
        /// </summary>
        public void LoadLevel(string levelId)
        
        {
            Debug.Log(string.Format("LoadLevel(string {0})", levelId));
            if (levelId == string.Empty)
            {
                //load next level in level list
                if (_dataManager.CheckPointLevelId != string.Empty)
                {
                    Debug.Log(string.Format("CheckPointLevelId: {0}", _dataManager.CheckPointLevelId));
                    levelId = GetNextLevelSpecs(_dataManager.CheckPointLevelId).Id;
                } else
                {
                    Debug.Log("checkpointlevelid is empty, using GetCurrentLevelId()");
                    levelId = GetNextLevelSpecs(GetCurrentLevelId()).Id;
                }
            }
            Debug.Log("attempting to load " + levelId);
            if (Application.CanStreamedLevelBeLoaded(GetLevelSpecsById(levelId).SceneName))
            {
                if (_dataManager != null)
                {
                    //update saved level id
                    _dataManager.CheckPointLevelId = GetLevelSpecsById(levelId).Id;    
                    _dataManager.SavedLocation = GetLevelSpecsById(levelId).Location;
                    _dataManager.Save();
                }
                
                StartCoroutine(LoadLevelAsync(GetLevelSpecsById(levelId).SceneName));
            }
            else
            {
                Debug.LogError("scene stream is invalid");
                //should implement an error popup for stuff like this
            }
        }

        /// <summary>
        /// Plays the transition, loads sceneName asyncronously, then fade out the transition
        /// </summary>
        /// <param name="sceneName"></param>
        /// <returns></returns>
        private IEnumerator LoadLevelAsync(string sceneName)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            while (!asyncOperation.isDone)
            {
                yield return null; //yield this frame
            }
            if (_currentLevelName != string.Empty)
            {
                AsyncOperation asyncOperation2 = SceneManager.UnloadSceneAsync(_currentLevelName);
                while (!asyncOperation2.isDone)
                {
                    yield return null; //yield this frame
                }
            }
            _gameManager.ShouldBeginLevel = true;
            _currentLevelName = sceneName;
        }

        public void LoadMainMenuLevel()
        {
            //LoadLevel(mainMenuIndex);
            StartCoroutine(LoadManagers());
            
        }

        IEnumerator LoadManagers()
        {
            //AsyncOperation async = SceneManager.LoadSceneAsync(1);
            //while (!async.isDone)
            //{
            //    yield return null;
            //}
            AsyncOperation async2 = SceneManager.LoadSceneAsync(mainMenuIndex, LoadSceneMode.Additive);
            while (!async2.isDone)
            {
                yield return null;
            }
        }

        /// <summary>
        /// Return LevelSpecs for a given ID given by string id
        /// </summary>
        /// <param name="id"></param>
        public LevelSpecs GetLevelSpecsById(string id)
        {
            if (levelSpecsById.ContainsKey(id))
            {
                return levelSpecsById[id];
            }
            else
            {
                return GetMap();    //default to return to main map if id not found
            }
        }

        public string GetCurrentLevelId() {
            var currentScene = SceneManager.GetActiveScene();
            var retval = levelSpecsById.Where(x => x.Value.SceneName == currentScene.path).FirstOrDefault();
            return retval.Key;
        }

        /// <summary>
        /// Returns next level in sort order from current location
        /// </summary>
        /// <param name="currentId"></param>
        /// <returns> LevelSpecs </returns>
        public LevelSpecs GetNextLevelSpecs(string currentId)
        {
            if (levelSpecsById.ContainsKey(currentId))
            {
                //find the next level in the current location
                LevelSpecs nextLevel = levelList.Levels.Find(x => x.Location == GetLevelSpecsById(currentId).Location
                    && x.SortOrder == GetLevelSpecsById(currentId).SortOrder + 1);
                if (nextLevel != null)
                {
                    return nextLevel;
                }
                else
                {
                    Debug.LogError(string.Format("Could not find {0} in level list", currentId));
                    //return main map when no next level is found (i.e. the end of the levels in the location)
                }
            }
            {
                Debug.LogError(string.Format("Could not find {0} in keys", currentId));
            }
            return GetMap();    //default to return to main map if id not found
        }

        /// <summary>
        /// Get the LevelSpecs of the map given by mapId
        /// </summary>
        /// <returns>LevelSpecs of the desired map (Main Map, by default) </returns>
        public LevelSpecs GetMap(string mapId = "Main_Map")
        {
            return GetLevelSpecsById(levelList.MapId);
        }

        public LevelSpecs GetFirstLevel()
        {
            return GetLevelSpecsById(levelList.FirstLevelId);
        }

        public LevelSpecs GetFirstLevelInLocation(string location)
        {
            LevelSpecs level = levelList.Levels.Find(x => x.Location == location && x.SortOrder == 0);
            if (level != null)
            {
                return level;
            }
            else
            {
                return GetMap();
            }
        }

        public LevelSpecs CurrentLevel()
        {
            return levelList.Levels.Find(x => x.SceneName == SceneManager.GetActiveScene().path);
        }
    }
}