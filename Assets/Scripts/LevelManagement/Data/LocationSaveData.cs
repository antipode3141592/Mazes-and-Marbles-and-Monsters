using System;

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
        public bool IsKnown;
        public bool IsAvailable;

        public LocationSaveData(string id = null, string checkpointLevelId = null, bool complete = false, bool isKnown = false, bool isAvailable = false) 
        {
            LocationId = id;
            Completed = complete;
            CheckpointLevelId = checkpointLevelId;
            IsKnown = isKnown;
            IsAvailable = isAvailable;
        }
    }
}
