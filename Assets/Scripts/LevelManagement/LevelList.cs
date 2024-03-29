﻿using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement.Levels
{
    [CreateAssetMenu(fileName = "LevelList", menuName = "Levels/Create LevelList", order =1)]

    //a readonly list of all levels
    //  levelspecs are edited in Unity editor
    //  a levellist scriptable object is attached to objects on the map screen
    //  
    
    public class LevelList : ScriptableObject
    {
        #region INSPECTOR
        [SerializeField] private List<LevelSpecs> levels;
        [SerializeField] private string mapId;
        [SerializeField] private string firstLevelId;
        [SerializeField] public int managerSceneIndex = 0;   //splash screen is build index 0
        [SerializeField] public int lightingSceneIndex = 1;   //splash screen is build index 0
        #endregion

        public List<LevelSpecs> Levels => levels;
        public string MapId => mapId;
        public string FirstLevelId => firstLevelId;
    }
}