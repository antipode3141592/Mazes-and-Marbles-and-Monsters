using LevelManagement.Levels;
using System.Collections.Generic;

namespace LevelManagement
{
    public interface ILevelManager
    {
        public List<LocationSpecs> LocationSpecs { get; }
        public LevelSpecs GetFirstLevel();
        public LevelSpecs GetFirstLevelInLocation(LocationSpecs locationSpecs);
        public LevelSpecs GetLevelSpecsById(string id);
        public LevelSpecs GetMap(string mapId = "Main_Map");
        public LevelSpecs CurrentLevel();
        public LevelSpecs LoadLevel(string levelId = "");
        public void LoadMainMenuLevel();
    }
}