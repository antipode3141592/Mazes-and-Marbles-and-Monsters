using System;

namespace LevelManagement.Data
{
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