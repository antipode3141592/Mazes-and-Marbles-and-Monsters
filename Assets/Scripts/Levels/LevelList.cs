using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement.Levels
{
    [CreateAssetMenu(fileName = "LevelList", menuName = "Levels/Create LevelList", order =1)]
    public class LevelList : ScriptableObject
    {
        #region INSPECTOR
        [SerializeField] private List<LevelSpecs> _levels;

        #endregion

        #region PROPERTIES
        public int TotalLevels => _levels.Count;
        #endregion

        public LevelSpecs GetLevelSpecs(int index)
        {
            return _levels[index];
        }
    }
}