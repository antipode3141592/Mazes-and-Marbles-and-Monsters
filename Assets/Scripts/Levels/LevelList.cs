using System.Collections.Generic;
using UnityEngine;

//code based on the course content at https://www.udemy.com/course/level-management-in-unity/ , which was super helpeful and highly recommended

namespace LevelManagement.Levels
{
    [CreateAssetMenu(fileName = "LevelList", menuName = "Levels/Create LevelList", order =1)]
    public class LevelList : ScriptableObject
    {
        #region INSPECTOR
        public string CampaignName;
        [SerializeField] private List<LevelSpecs> _levels;

        #endregion

        #region PROPERTIES
        public int TotalLevels => _levels.Count;

        
        #endregion

        public LevelSpecs GetLevelSpecs(int index)
        {
            return _levels[index];
        }

        public LevelSpecs FindLevel(string sceneName)
        {
            return _levels.Find(x => x.SceneName == sceneName);
        }
    }
}