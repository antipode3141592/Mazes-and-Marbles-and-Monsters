using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LevelManagement.Levels;
using LevelManagement.Data;

//code based on the course content at https://www.udemy.com/course/level-management-in-unity/ , which was super helpeful and highly recommended

namespace LevelManagement
{
    public class LevelLoader : MonoBehaviour
    {
        private static int mainMenuIndex = 1;   //splash screen is build index 0
        private LevelSelector levelSelector;


        private void Awake()
        {
            levelSelector = GetComponent<LevelSelector>();
        }

        //reload current level, no need to specificy scene name or index
        public static void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //public static void LoadNextLevel()
        //{

        //    int nextSceneIndex = (SceneManager.GetActiveScene().buildIndex + 1)
        //        % SceneManager.sceneCountInBuildSettings;
        //    nextSceneIndex = Mathf.Clamp(nextSceneIndex, mainMenuIndex, SceneManager.sceneCountInBuildSettings);
        //    LoadLevel(nextSceneIndex);
        //}

        public void LoadNextLevel()
        {
            if (SceneManager.GetActiveScene().buildIndex == mainMenuIndex) 
            {
                Debug.Log("currently in main menu, loading first level from level list");
                levelSelector.SetIndex(0);
                LoadLevel(levelSelector.GetLevelSpecsAtIndex(0).SceneName);
            }
            else
            {
                int nextSceneIndex = (levelSelector.CurrentIndex + 1)
                    % levelSelector.TotalLevelCount();
                //nextSceneIndex = Mathf.Clamp(nextSceneIndex, mainMenuIndex, levelSelector.TotalLevelCount());
                LevelSpecs nextLevelSpecs = levelSelector.GetLevelSpecsAtIndex(nextSceneIndex);
                levelSelector.SetIndex(nextSceneIndex);
                DataManager.Instance.CurrentLevelSpecs = nextLevelSpecs;    //unlock next index
                DataManager.Instance.Save();
                LoadLevel(nextLevelSpecs.SceneName);
            }
        }

        public void LoadLevel(int levelIndex)
        {
            LoadLevel(levelSelector.GetLevelSpecsAtIndex(levelIndex).SceneName);
        }

        public static void LoadLevel(string levelName)
        {
            Debug.Log("attempting to load " + levelName);
            if (Application.CanStreamedLevelBeLoaded(levelName))
            {
                Debug.Log("level is valid, now loading");
                SceneManager.LoadScene(levelName);
            }
            else
            {
                Debug.LogWarning(string.Format("levelName {0} not found in LoadLevel() script!", levelName));
            }
        }

        public static void LoadMainMenuLevel()
        {
            //LoadLevel(mainMenuIndex);
            SceneManager.LoadScene(mainMenuIndex);
        }
    }
}