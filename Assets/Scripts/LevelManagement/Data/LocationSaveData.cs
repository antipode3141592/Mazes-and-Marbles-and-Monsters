using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LevelManagement.DataPersistence
{
    /// <summary>
    /// Container for save data given by LocationId
    /// </summary>
    [Serializable]
    public class LocationSaveData
    {
        public string LocationId;   //key
        public bool Completed;      //true if player has completed all 
        public string CheckpointLevelId;    //level ID to restore to

        public LocationSaveData(string id = null , string checkpointLevelId = null, bool complete = false) 
        {
            LocationId = id;
            Completed = complete;
            CheckpointLevelId = checkpointLevelId;
        }

        public bool LocationCompletionCheck()
        {
            //if all levels with SortOrder => 0 are Completed, set Completed to true and return true

            //else, location is incomplete and return false
            return false;
        }
    }
}
