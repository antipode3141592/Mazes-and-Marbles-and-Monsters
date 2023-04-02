using FiniteStateMachine;
using LevelManagement.Levels;

namespace MarblesAndMonsters
{
    public interface IGameManager
    {
        IGameState CurrentState { get; }
        bool ShouldBeginLevel { get; set; }
        bool ShouldLoadNextLevel { get; set; }

        void EnterLocation();
        void OpenWorldMap();
        void LevelLose();
        void LevelWin(LevelSpecs gotoLevel);
        void PauseGame();
        void SaveGameData();
        void UnpauseGame();
    }
}