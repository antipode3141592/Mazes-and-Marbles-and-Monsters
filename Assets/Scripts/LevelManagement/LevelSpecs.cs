﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//code based on the course content at https://www.udemy.com/course/level-management-in-unity/ , which was super helpeful and highly recommended

namespace LevelManagement.Levels
{

    //  Class with header-type information for individual Unity scenes, to be managed by LevelSelector
    //      Scenes have 
    //      Each Level will have a primary string key of form:
    //          [Map Name] _[Location Name] _[Level: 000 - 999]{_SubLevel: A}

[Serializable]
    public class LevelSpecs
    {

        #region INSPECTOR
        //for 
        [SerializeField] protected string _id; //unique id for level
        [SerializeField] protected string _map;         //belongs to this map collection
        [SerializeField] protected string _location;    //belongs to this location
        [SerializeField] protected int _sortOrder;      // when negative, level is "hidden" 
        
        [SerializeField] protected string _displayName; //the display name
        [SerializeField] protected string _sceneName; //exact scene name in Unity

        [SerializeField] protected string _description;
        [SerializeField] protected string _thumbnailLocation;    //file location for thumbnail of level
        
        #endregion

        #region PROPERTIES
        public string Description => _description;
        public string DisplayName => _displayName;
        public string SceneName => _sceneName;
        public string Map => _map;
        public string Location => _location;
        public string Id => _id;
        public string ThumbnailLocation => _thumbnailLocation;

        public int SortOrder => _sortOrder;
        #endregion

        public LevelSpecs()
        {
            
        }
    }
}
