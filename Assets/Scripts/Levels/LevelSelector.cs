using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//code based on the course content at https://www.udemy.com/course/level-management-in-unity/ , which was super helpeful and highly recommended

namespace LevelManagement.Levels

{

    //for accessing the levelList scriptable object (which contains data about level scenes)
    //  -trac
    //  TODO - make a ScriptableObject that holds a LevelList, with descriptors
    //          - 
    public class LevelSelector : MonoBehaviour
    {
        [SerializeField] protected LevelList[] levelLists;
        protected Dictionary<string, LevelList> campaignDictionary;

        private int _currentIndex;
        private string _currentCampaign;    //campaign is a group of Levels/scenes, key for campaignDictionary

        public int CurrentIndex => _currentIndex;
        public string CurrentCampaign => CurrentCampaign;

        #region Unity Overrides
        private void Awake()
        {
            campaignDictionary = new Dictionary<string, LevelList>();
            _currentIndex = 0;
            _currentCampaign = levelLists[_currentIndex].CampaignName;
            foreach(LevelList levelList in levelLists)
            {
                campaignDictionary.Add(levelList.CampaignName, levelList);
            }
        }
        #endregion


        public void SetIndex(int index)
        {
            _currentIndex = Mathf.Clamp(index, 0, campaignDictionary[_currentCampaign].TotalLevels);
        }

        public void SetCampaign(string Campaign)
        {
            if (campaignDictionary.ContainsKey(Campaign))
            {
                _currentCampaign = Campaign;
            }
        }

        public int TotalLevelCount()
        {
            return campaignDictionary[_currentCampaign].TotalLevels;
        }

        public LevelSpecs Next()
        {
            SetIndex(_currentIndex + 1);
            return campaignDictionary[_currentCampaign].GetLevelSpecs(_currentIndex);
        }

        public LevelSpecs GetLevelSpecsAtIndex(int index)
        {
            return campaignDictionary[_currentCampaign].GetLevelSpecs(index);
        }

        public LevelSpecs GetCurrentLevelSpecs()
        {
            return campaignDictionary[_currentCampaign].GetLevelSpecs(_currentIndex);
        }

        public List<string> GetCampaigns()
        {
            return campaignDictionary.Keys.ToList();
        }
    }
}
