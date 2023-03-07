using FiniteStateMachine;

namespace MarblesAndMonsters
{
    public interface IGameManager
    {
        BaseState CurrentState { get; }
        bool ShouldBeginLevel { get; set; }
        bool ShouldLoadNextLevel { get; set; }


        void LevelLose();
        void LevelWin(string goToLevelId);
        void PauseGame();
        void SaveGameData();
        void UnpauseGame();
    }
}