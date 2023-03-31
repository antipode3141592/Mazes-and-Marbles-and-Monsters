using FiniteStateMachine;
using System;

namespace MarblesAndMonsters.States.GameStates
{
    public class END : IGameState
    {
        IGameManager _gameManager;

        public Type Type { get => typeof(END); }

        public END(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public void Enter()
        {
            
        }

        public void HandleInput()
        {
            
        }

        public Type LogicUpdate(float deltaTime)
        {
            return typeof(END);
        }

        public void PhysicsUpdate()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}
