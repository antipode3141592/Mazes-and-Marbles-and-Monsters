using LevelManagement.DataPersistence;
using LevelManagement.Levels;
using MarblesAndMonsters;
using MarblesAndMonsters.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace LevelManagement
{
    public class LevelManager : MonoBehaviour, ILevelManager
    {
        [SerializeField] int managerSceneIndex = 0;
        [SerializeField] int lightingSceneIndex = 1;
        [SerializeField] LevelSpecs mainMap;
        [SerializeField] LevelSpecs startingLevel;
        [SerializeField] LocationSpecs startingLocation;
        [SerializeField] TransitionFader levelLoadTransition;
        [SerializeField] List<LocationSpecs> locationSpecs = new();

        public List<LocationSpecs> LocationSpecs => locationSpecs;

        protected IDataManager _dataManager;
        protected IGameManager _gameManager;
        protected IAudioManager _audioManager;

        protected string _currentLevelName = string.Empty;   //default to empty

        public LocationSpecs StartingLocation => startingLocation;

        [Inject]
        public void Init(IDataManager dataManager, IGameManager gameManager, IAudioManager audioManager)
        {
            _dataManager = dataManager;
            _gameManager = gameManager;
            _audioManager = audioManager;
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

        public LevelSpecs LoadLevel(string levelId = "")
        {
            LevelSpecs _levelSpecs;
            if (Debug.isDebugBuild)
                Debug.Log($"LoadLevel({levelId})", this);
            if (levelId == string.Empty)
            {
                //load next level in level list
                if (_dataManager.CurrentLevelId != string.Empty)
                {
                    if (Debug.isDebugBuild)
                        Debug.Log($"CurrentLevelId: {_dataManager.CurrentLevelId}", this);
                    _levelSpecs = GetNextLevelSpecs(GetLevelSpecsById(_dataManager.CurrentLevelId));
                }
                else
                {
                    if (Debug.isDebugBuild)
                        Debug.Log("checkpointlevelid is empty, using GetCurrentLevelId()");
                    _levelSpecs = GetNextLevelSpecs(CurrentLevel());
                }
                levelId = _levelSpecs.Id;
            } else
            {
                _levelSpecs = GetLevelSpecsById(levelId);
            }
            if (Debug.isDebugBuild)
                Debug.Log("attempting to load " + levelId, this);
            if (Application.CanStreamedLevelBeLoaded(_levelSpecs.ScenePath))
            {
                if (_levelSpecs != GetMap() && _dataManager != null)
                {
                    //update saved level id
                    if (Debug.isDebugBuild)
                        Debug.Log($"storing current location and level to data manager: {_levelSpecs.Id}, {_levelSpecs.LocationId}", this);
                    _dataManager.CurrentLevelId = _levelSpecs.Id;
                    _dataManager.CurrentLocationId = _levelSpecs.LocationId;
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
            if (SceneManager.sceneCount == 1 && SceneManager.GetActiveScene().buildIndex == managerSceneIndex)
            {
                AsyncOperation async = SceneManager.LoadSceneAsync(lightingSceneIndex, LoadSceneMode.Additive);
                while (!async.isDone)
                    yield return null;
            }
            else
            {
                AsyncOperation async2 = SceneManager.LoadSceneAsync(managerSceneIndex, LoadSceneMode.Single);
                AsyncOperation async3 = SceneManager.LoadSceneAsync(lightingSceneIndex, LoadSceneMode.Additive);
                while (!async2.isDone && !async3.isDone)
                    yield return null;
            }
        }

        public LevelSpecs GetLevelSpecsById(string id)
        {
            foreach(var location in locationSpecs)
            {
                var levelSpecs = location.LevelSpecs.Find(x => x.Id == id);
                if (levelSpecs is null)
                    continue;
                return levelSpecs;
            }
            return GetMap();    //default to return to main map if id not found
        }

        LevelSpecs GetNextLevelSpecs(LevelSpecs currentLevelSpecs)
        {
            LevelSpecs nextLevel = currentLevelSpecs.LocationSpecs.LevelSpecs.Find(x => x.SortOrder == currentLevelSpecs.SortOrder + 1);
            if (nextLevel is null)
                return GetMap();    //default to return to main map if id not found
            return nextLevel;
        }

        public LevelSpecs GetMap(string mapId = "Main_Map")
        {
            return mainMap;
        }

        public LevelSpecs GetFirstLevel()
        {
            return startingLevel;
        }

        public LevelSpecs GetFirstLevelInLocation(LocationSpecs locationSpecs)
        {
            LevelSpecs level = locationSpecs.LevelSpecs.Find(x => x.SortOrder == 0);
            if (level is null)
                return GetMap();
            return level;
        }

        public LevelSpecs CurrentLevel()
        {
            foreach (var location in LocationSpecs)
            {
                var levelSpecs = location.LevelSpecs.Find(x => x.ScenePath == SceneManager.GetActiveScene().path);
                if (levelSpecs is null)
                    continue;
                return levelSpecs;
            }
            return GetMap();
        }
    }
}