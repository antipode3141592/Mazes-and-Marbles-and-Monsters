using LevelManagement.Levels;
using System.Collections.Generic;

namespace LevelManagement
{
    public interface ILevelManager
    {
        public Dictionary<string, LevelSpecs> LevelSpecsById { get; }
        public List<LocationSpecs> LocationSpecs { get; }
        public LevelSpecs CurrentLevel();
        public string GetCurrentLevelId();
        public LevelSpecs GetFirstLevel();
        public LevelSpecs GetFirstLevelInLocation(string location);
        public LevelSpecs GetLevelSpecsById(string id);
        public LevelSpecs GetMap(string mapId = "Main_Map");
        public LevelSpecs GetNextLevelSpecs(string currentId);
        public LevelSpecs LoadLevel(string levelId = "");
        public void LoadMainMenuLevel();
    }
}