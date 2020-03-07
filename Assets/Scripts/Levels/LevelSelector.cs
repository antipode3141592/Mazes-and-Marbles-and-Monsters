using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            return levelList.GetLevel(index);
        }

        public LevelSpecs GetCurrentLevelSpecs()
        {
            return levelList.GetLevel(_currentIndex);
        }
    }
}
