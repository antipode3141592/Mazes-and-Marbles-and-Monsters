using System;

namespace LevelManagement.DataPersistence
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
        public float ElapsedGameTimeInSeconds;

        public LevelSaveData(string id = null, string location = null, int scrolls = 0, bool completed = false, float elapsedGameTimeInSeconds = 0f) 
        {
            LevelId = id;
            LocationId = location;
            CollectedScrolls = scrolls;
            Completed = completed;
            ElapsedGameTimeInSeconds = elapsedGameTimeInSeconds;
        }
    }
}