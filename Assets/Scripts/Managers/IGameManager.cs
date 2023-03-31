using FiniteStateMachine;

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
        void LevelWin();
        void PauseGame();
        void SaveGameData();
        void UnpauseGame();
    }
}