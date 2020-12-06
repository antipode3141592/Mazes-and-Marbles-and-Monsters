using UnityEngine;

//code based on the course content at https://www.udemy.com/course/level-management-in-unity/ , which was super helpeful and highly recommended

namespace LevelManagement.Levels

{
    public class LevelSelector : MonoBehaviour
    {
        [SerializeField] protected LevelList levelList;

        private int _currentIndex;

        public int CurrentIndex => _currentIndex;

        public void SetIndex(int index)
        {
            _currentIndex = index;
            ClampIndex();
        }

        public int TotalLevelCount()
        {
            return levelList.TotalLevels;
        }

        private void ClampIndex()
        {
            if (levelList.TotalLevels == 0)
            {
                Debug.LogWarning("Clamp index failed, no levels in levelList!");
                return;
            }
            else if (_currentIndex >= levelList.TotalLevels)
            {
                //if index is at or above the total levels, wrap to 0
                _currentIndex = 0;
            }
            else if (_currentIndex < 0)
            {
                //if index is less than 0, wrap to last level
                _currentIndex = levelList.TotalLevels - 1; 
            }
        }

        public LevelSpecs GetLevelSpecsAtIndex(int index)
        {
            return levelList.GetLevelSpecs(index);
        }

        public LevelSpecs GetCurrentLevelSpecs()
        {
            return levelList.GetLevelSpecs(_currentIndex);
        }
    }
}
