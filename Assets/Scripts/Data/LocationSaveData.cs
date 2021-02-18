using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LevelManagement.Data
{
    /// <summary>
    /// Container for save data given by LocationId
    /// </summary>
    [Serializable]
    public class LocationSaveData
    {
        public string LocationId;   //key
        public bool Completed;      //true if player has completed all 

        public LocationSaveData(string id = null, bool complete = false) 
        {
            LocationId = id;
            Completed = complete;
        }

        public bool LocationCompletionCheck()
        {
            //if all levels with SortOrder => 0 are Completed, set Completed to true and return true

            //else, location is incomplete and return false
            return false;
        }
    }
}
