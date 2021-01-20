using System.Collections;
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
        private LevelSelector levelSelector;

        private void Awake()
        {
            levelSelector = GetComponent<LevelSelector>();
        }

        //load the next scene in the LevelList, as tracked by the LevelSelector
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
                LevelSpecs nextLevelSpecs = levelSelector.Next();
                if (DataManager.Instance != null)
                {
                    DataManager.Instance.SavedLevel = nextLevelSpecs.LevelName;    //unlock next index
                    DataManager.Instance.SavedCampaign = levelSelector.CurrentCampaign;
                    DataManager.Instance.CurrentLevelSpecs = nextLevelSpecs;
                    DataManager.Instance.Save();
                } else
                {
                    Debug.LogWarning("DataManager not available during LoadNextLevel()");
                }
                LoadLevel(nextLevelSpecs.SceneName);
            }
        }

        //load a specific scene
        public void LoadLevel(string sceneName)
        {
            Debug.Log("attempting to load " + sceneName);
            if (Application.CanStreamedLevelBeLoaded(sceneName))
            {
                StartCoroutine(LoadLevelAsync(sceneName));
            }
            else
            {
                Debug.LogError("scene stream is invalid");
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
    }
}