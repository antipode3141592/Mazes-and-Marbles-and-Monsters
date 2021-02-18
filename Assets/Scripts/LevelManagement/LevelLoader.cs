using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LevelManagement.Levels;
using LevelManagement.Data;

//code based on the course content at https://www.udemy.com/course/level-management-in-unity/ , which was super helpeful and highly recommended

namespace LevelManagement
{

    //
    public class LevelLoader : MonoBehaviour
    {
        private static readonly int mainMenuIndex = 1;   //splash screen is build index 0
        [SerializeField] private LevelList levelList;    //scriptable object with all accessible levels

        private Dictionary<string, LevelSpecs> levelSpecsById = new Dictionary<string, LevelSpecs>(); // key is string Id

        private static LevelLoader _instance;
        public static LevelLoader Instance
        {
            get { return _instance; }
        }

        protected void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                //build dictionary with key equal to Id field in LevelSpecs
                //levelSpecsById = new Dictionary<string, LevelSpecs>();
                foreach (LevelSpecs level in levelList.Levels)
                {
                    levelSpecsById.Add(level.Id, level);
                }
                Debug.Log(string.Format("Level Dictionary has {0} key-value pairs", levelSpecsById.Count));
            }
        }

        /// <summary>
        /// Load a specific scene by levelId
        /// </summary>
        public void LoadLevel(string levelId)
        
        {
            if (levelId == string.Empty)
            {
                //load next level in level list
                levelId = GetNextLevelSpecs(DataManager.Instance.CheckPointLevelId).Id;
            }
            Debug.Log("attempting to load " + levelId);
            if (Application.CanStreamedLevelBeLoaded(GetLevelSpecsById(levelId).SceneName))
            {
                if (DataManager.Instance != null)
                {
                    //update saved level id
                    DataManager.Instance.CheckPointLevelId = GetLevelSpecsById(levelId).Id;    
                    DataManager.Instance.SavedLocation = GetLevelSpecsById(levelId).Location;
                    DataManager.Instance.Save();
                }
                
                StartCoroutine(LoadLevelAsync(GetLevelSpecsById(levelId).SceneName));
            }
            else
            {
                Debug.LogError("scene stream is invalid");
                //should implement an error popup for stuff like this
            }
        }

        private IEnumerator LoadLevelAsync(string sceneName)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncOperation.isDone)
            {
                yield return null; //yield this frame
            }
        }

        public static void LoadMainMenuLevel()
        {
            //LoadLevel(mainMenuIndex);
            SceneManager.LoadScene(mainMenuIndex);
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