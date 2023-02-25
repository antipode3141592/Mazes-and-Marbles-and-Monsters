using Chronos;
using FiniteStateMachine;
using System;

namespace MarblesAndMonsters.States.GameStates
{
    public class Playing : GameState
    {
        public override Type Type { get => typeof(Playing); }

        IInputManager _inputManager;
        ICharacterManager _characterManager;
        TimeTracker _timeTracker;
        Clock _rootClock;

        public Playing(IGameManager manager, IInputManager inputManager, ICharacterManager characterManager, TimeTracker timeTracker, Clock rootClock) : base()
        {
            _manager = manager;
            _inputManager = inputManager;
            _characterManager = characterManager;
            _timeTracker = timeTracker;
            _rootClock = rootClock;
        }
        public override void Enter()
        {
            _rootClock.localTimeScale = 1f;
            _timeTracker.StartSessionTime();
        }

        public override void HandleInput()
        {
            _inputManager.MeasureBoardTilt();
        }

        public override void PhysicsUpdate()
        {
            _characterManager.MoveAll();  //characters should only move during play
        }
    }
}
