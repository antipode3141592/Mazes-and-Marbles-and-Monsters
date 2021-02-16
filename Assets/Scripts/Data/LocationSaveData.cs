using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LevelManagement.Data
{
    [Serializable]
    public class LocationSaveData
    {
        public string LocationId;
        public List<LevelSaveData> Levels;

        public LocationSaveData(string id = null) 
        {
            LocationId = id;
            Levels = new List<LevelSaveData>();
        }
    }
}
