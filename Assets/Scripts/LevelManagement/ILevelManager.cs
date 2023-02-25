using LevelManagement.Levels;

namespace LevelManagement
{
    public interface ILevelManager
    {
        public LevelSpecs CurrentLevel();
        public string GetCurrentLevelId();
        public LevelSpecs GetFirstLevel();
        public LevelSpecs GetFirstLevelInLocation(string location);
        public LevelSpecs GetLevelSpecsById(string id);
        public LevelSpecs GetMap(string mapId = "Main_Map");
        public LevelSpecs GetNextLevelSpecs(string currentId);
        public void LoadLevel(string levelId);
        public void LoadMainMenuLevel();
    }
}