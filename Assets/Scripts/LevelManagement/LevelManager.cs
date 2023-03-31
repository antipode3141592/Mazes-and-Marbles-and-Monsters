using LevelManagement.DataPersistence;
using LevelManagement.Levels;
using MarblesAndMonsters;
using MarblesAndMonsters.Managers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace LevelManagement
{
    public class LevelManager : MonoBehaviour, ILevelManager
    {
        [SerializeField] LevelList levelList;    //scriptable object with all accessible levels
        [SerializeField] TransitionFader levelLoadTransition;

        Dictionary<string, LevelSpecs> levelSpecsById = new Dictionary<string, LevelSpecs>(); // key is string Id
        [SerializeField] List<LocationSpecs> locationSpecs = new();

        public Dictionary<string, LevelSpecs> LevelSpecsById => levelSpecsById;
        public List<LocationSpecs> LocationSpecs => locationSpecs;

        protected IDataManager _dataManager;
        protected IGameManager _gameManager;
        protected IAudioManager _audioManager;

        protected string _currentLevelName = string.Empty;   //default to empty

        [Inject]
        public void Init(IDataManager dataManager, IGameManager gameManager, IAudioManager audioManager)
        {
            _dataManager = dataManager;
            _gameManager = gameManager;
            _audioManager = audioManager;
        }

        protected void Awake()
        {
            //build dictionary with key equal to Id field in LevelSpecs
            foreach (LevelSpecs level in levelList.Levels)
                levelSpecsById.Add(level.Id, level);
            if (Debug.isDebugBuild)
                Debug.Log(string.Format("Level Dictionary has {0} key-value pairs", levelSpecsById.Count));
        }

        void Start()
        {
            if (Debug.isDebugBuild)
                Debug.Log($"LevelManager Start() => Scene Count = {SceneManager.sceneCount}", gameObject);
            //if there are three scenes open OnStart, it's a debug/playtest situation where the 
            // scene level is all ready loaded, so we need to trigger start of level manually here
            if (SceneManager.sceneCount == 3)
                _gameManager.ShouldBeginLevel = true;
            else if (SceneManager.sceneCount == 2)
            {
                if (Debug.isDebugBuild)
                    Debug.Log($"SceneCount 2", this);
            }
            else
                LoadMainMenuLevel();
        }

        /// <summary>
        /// Load a specific scene by levelId
        /// </summary>
        public LevelSpecs LoadLevel(string levelId = "")
        {
            LevelSpecs _levelSpecs;
            if (Debug.isDebugBuild)
                Debug.Log($"LoadLevel({levelId})", this);
            if (levelId == string.Empty)
            {
                //load next level in level list
                if (_dataManager.CheckPointLevelId != string.Empty)
                {
                    if (Debug.isDebugBuild)
                        Debug.Log($"CheckPointLevelId: {_dataManager.CheckPointLevelId}", this);
                    _levelSpecs = GetNextLevelSpecs(_dataManager.CheckPointLevelId);
                }
                else
                {
                    if (Debug.isDebugBuild)
                        Debug.Log("checkpointlevelid is empty, using GetCurrentLevelId()");
                    _levelSpecs = GetNextLevelSpecs(GetCurrentLevelId());
                }
                levelId = _levelSpecs.Id;
            } else
            {
                _levelSpecs = GetLevelSpecsById(levelId);
            }
            Debug.Log("attempting to load " + levelId);
            if (Application.CanStreamedLevelBeLoaded(_levelSpecs.ScenePath))
            {
                if (_dataManager != null)
                {
                    //update saved level id
                    _dataManager.CheckPointLevelId = _levelSpecs.Id;
                    _dataManager.SavedLocation = _levelSpecs.LocationId;
                    _dataManager.Save();
                }
                _audioManager.PlayMusic(_levelSpecs.LevelMusic);
                StartCoroutine(LoadLevelAsync(_levelSpecs.ScenePath));
                return _levelSpecs;
            }
            else
            {
                Debug.LogError("scene stream is invalid");
                return null;
            }
        }

        /// <summary>
        /// Plays the transition, loads sceneName asyncronously, then fade out the transition
        /// </summary>
        /// <param name="sceneName"></param>
        /// <returns></returns>
        IEnumerator LoadLevelAsync(string sceneName)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            while (!asyncOperation.isDone)
                yield return null;
            if (_currentLevelName != string.Empty)
            {
                AsyncOperation asyncOperation2 = SceneManager.UnloadSceneAsync(_currentLevelName);
                while (!asyncOperation2.isDone)
                    yield return null;
            }
            _gameManager.ShouldBeginLevel = true;
            _currentLevelName = sceneName;
        }

        public void LoadMainMenuLevel()
        {
            StartCoroutine(LoadManagers());
        }

        IEnumerator LoadManagers()
        {
            if (SceneManager.sceneCount == 1 && SceneManager.GetActiveScene().buildIndex == levelList.ManagerSceneIndex)
            {
                AsyncOperation async = SceneManager.LoadSceneAsync(levelList.LightingSceneIndex, LoadSceneMode.Additive);
                while (!async.isDone)
                    yield return null;
            }
            else
            {
                AsyncOperation async2 = SceneManager.LoadSceneAsync(levelList.ManagerSceneIndex, LoadSceneMode.Single);
                AsyncOperation async3 = SceneManager.LoadSceneAsync(levelList.LightingSceneIndex, LoadSceneMode.Additive);
                while (!async2.isDone && !async3.isDone)
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
                return levelSpecsById[id];
            else
                return GetMap();    //default to return to main map if id not found
        }

        public string GetCurrentLevelId()
        {
            var currentScene = SceneManager.GetActiveScene();
            var retval = levelSpecsById.Where(x => x.Value.ScenePath == currentScene.path).FirstOrDefault();
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
                LevelSpecs nextLevel = levelList.Levels.Find(x => x.LocationId == GetLevelSpecsById(currentId).LocationId
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
            return levelList.MainMapSpecs;
        }

        public LevelSpecs GetFirstLevel()
        {
            return levelList.StartingLevel;
        }

        public LevelSpecs GetFirstLevelInLocation(string location)
        {
            LevelSpecs level = levelList.Levels.Find(x => x.LocationId == location && x.SortOrder == 0);
            if (level != null)
                return level;
            else
                return GetMap();
        }

        public LevelSpecs CurrentLevel()
        {
            return levelList.Levels.Find(x => x.ScenePath == SceneManager.GetActiveScene().path);
        }
    }
}