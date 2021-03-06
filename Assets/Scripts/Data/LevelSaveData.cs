using System;

namespace LevelManagement.Data
{
    /// <summary>
    /// Store data about a level given by LevelId
    /// </summary>

    [Serializable]
    public class LevelSaveData
    {
        public string LevelId;          //key
        public string LocationId;       //grouping
        public int CollectedScrolls;    //
        public bool Completed;          //

        public LevelSaveData(string id = null, string location = null, int scrolls = 0, bool completed = false) 
        {
            LevelId = id;
            LocationId = location;
            CollectedScrolls = scrolls;
            Completed = completed;
        }
    }
}