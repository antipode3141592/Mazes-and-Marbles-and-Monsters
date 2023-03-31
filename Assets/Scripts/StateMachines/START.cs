using FiniteStateMachine;
using System;

namespace MarblesAndMonsters.States.GameStates
{
    //Start occurs when a new level/scene is loaded, so take this opportunity to check for Player, assign spawn points, etc
    public class START : IGameState
    {
        IGameManager _gameManager;

        public Type Type { get => typeof(START); }

        public START(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public Type LogicUpdate(float deltaTime)
        {
            if (_gameManager.ShouldBeginLevel)
            {
                _gameManager.ShouldBeginLevel = false;
                return typeof(PopulateLevel);
            }
            return typeof(START);
        }

        public void Enter()
        {
        }

        public void HandleInput()
        {
            
        }

        public void PhysicsUpdate()
        {
            
        }

        public void Exit()
        {
            
        }
    }

}
