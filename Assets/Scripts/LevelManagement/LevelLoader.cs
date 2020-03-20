using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LevelManagement.Levels;
using LevelManagement.Data;


namespace LevelManagement
{
    public class LevelLoader : MonoBehaviour
    {
        private static int mainMenuIndex = 1;   //splash screen is build index 0
        private LevelSelector levelSelector;


        private void Awake()
        {
            levelSelector = gameObject.GetComponent<LevelSelector>();
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
                LevelSpecs levelSpecs = levelSelector.GetLevelSpecsAtIndex(nextSceneIndex);
                levelSelector.SetIndex(nextSceneIndex);
                DataManager.Instance.HigestLevelUnlocked = nextSceneIndex;    //unlock next index
                DataManager.Instance.Save();
                LoadLevel(levelSpecs.SceneName);
            }
        }

        public void LoadLevel(int levelIndex)
        {
            LoadLevel(levelSelector.GetLevelSpecsAtIndex(levelIndex).SceneName);
            //if (levelIndex >= 0 && levelIndex < SceneManager.sceneCountInBuildSettings)
            //{
            //    if (levelIndex == mainMenuIndex)
            //    {
            //        MainMenu.Open();
            //    }
            //}
            //SceneManager.LoadScene(levelIndex);
        }

        private static void LoadLevel(string levelName)
        {
            Debug.Log("attempting to load " + levelName);
            if (Application.CanStreamedLevelBeLoaded(levelName))
            {
                Debug.Log("level is valid, now loading");
                SceneManager.LoadScene(levelName);
            }
            else
            {
                Debug.LogWarning("levelName not found in LoadLevel() script!");
            }
        }

        public static void LoadMainMenuLevel()
        {
            //LoadLevel(mainMenuIndex);
            SceneManager.LoadScene(mainMenuIndex);
        }
    }
}