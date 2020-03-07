using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LevelManagement.Levels
{
    [Serializable]
    public class LevelSpecs
    {

        #region INSPECTOR
        [SerializeField] protected string _name;
        [SerializeField] protected string _description;
        [SerializeField] protected string _campaignName;
        [SerializeField] protected string _sceneName; //exast scene name in Unity
        [SerializeField] protected string _id; //sorter unique id for level
        [SerializeField] protected Sprite _image;    //thumbnail of level
        #endregion

        #region PROPERTIES
        public string Name => _name; //shorthand for readonly get 
        public string Description => _description;
        public string CampaignName => _campaignName;
        public string SceneName => _sceneName;
        public string Id => _id;
        public Sprite Image => _image;
        #endregion

        public LevelSpecs()
        {

        }
    }
}
