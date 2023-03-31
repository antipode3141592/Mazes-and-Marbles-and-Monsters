using Chronos;
using FiniteStateMachine;
using System;

namespace MarblesAndMonsters.States.GameStates
{
    public class Paused : IGameState
    {
        public Type Type { get => typeof(Paused); }

        Clock _rootClock;
        IGameManager _gameManager;
        ITimeTracker _timeTracker;

        public Paused(IGameManager gameManager, ITimeTracker timeTracker, Clock rootClock)
        {
            _gameManager = gameManager;
            _timeTracker = timeTracker;
            _rootClock = rootClock;
        }
        public void Enter()
        {
            _rootClock.localTimeScale = 0f;
            _timeTracker.EndLevelTimer();
        }

        public void Exit()
        {
            
        }

        public void HandleInput()
        {
            
        }

        public Type LogicUpdate(float deltaTime)
        {
            return (typeof(Paused));
        }

        public void PhysicsUpdate()
        {
            
        }
    }
}
