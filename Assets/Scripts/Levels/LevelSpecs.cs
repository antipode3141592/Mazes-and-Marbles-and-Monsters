using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//code based on the course content at https://www.udemy.com/course/level-management-in-unity/ , which was super helpeful and highly recommended

namespace LevelManagement.Levels
{
    [Serializable]
    public class LevelSpecs
    {

        #region INSPECTOR
        [SerializeField] protected string _name;
        [SerializeField] protected string _description;
        //[SerializeField] protected string _campaignName;
        [SerializeField] protected string _levelName; //the display name
        [SerializeField] protected string _sceneName; //exact scene name in Unity
        [SerializeField] protected string _id; //sorter unique id for level
        [SerializeField] protected Sprite _image;    //thumbnail of level
        [SerializeField] protected bool _illuminated = true;   //default true for global lighting, false for dark global and player torchlight
        #endregion

        #region PROPERTIES
        public string Name => _name; //shorthand for readonly get 
        public string Description => _description;
        //public string CampaignName => _campaignName;
        public string LevelName => _levelName;
        public string SceneName => _sceneName;
        public string Id => _id;
        public Sprite Image => _image;
        public bool Illuminated => _illuminated;
        #endregion

        public LevelSpecs()
        {

        }
    }
}
