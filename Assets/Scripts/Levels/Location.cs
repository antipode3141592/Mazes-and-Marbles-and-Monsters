using UnityEngine;
using LevelManagement.Data;

namespace LevelManagement.Levels
{
    //
    public class Location : MonoBehaviour
    {
        private ScriptableObject levelList;
        private int currentIndex = 0;
        private LevelSpecs currentLevelSpecs;
        private LevelLoader levelLoader;
        [SerializeField] private string locationName;

        public int CurrentIndex => currentIndex;
        public LevelSpecs CurrentLevelSpecs => currentLevelSpecs;

        //upon awake, 
        private void Awake()
        {
            levelLoader = FindObjectOfType<LevelLoader>();
            if (DataManager.Instance != null)
            {

            }
            else
            {
                currentIndex = 0;
            }
        }





        //when player enters trigger area, open map popup
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //pause time

            //open map menu

            //await 
            if (collision.CompareTag("Player"))
            {
                levelLoader.LoadLevel(levelLoader.GetFirstLevelInLocation(locationName).Id);
            }
        }


    }
}