using Chronos;
using FiniteStateMachine;
using System;

namespace MarblesAndMonsters.States.GameStates
{
    public class Paused : GameState
    {
        public override Type Type { get => typeof(Paused); }

        Clock _rootClock;
        ITimeTracker _timeTracker;

        public Paused(IGameManager manager, ITimeTracker timeTracker, Clock rootClock) : base()
        {
            _manager = manager;
            _timeTracker = timeTracker;
            _rootClock = rootClock;
        }
        public override void Enter()
        {
            _rootClock.localTimeScale = 0f;
            _timeTracker.EndLevelTimer();
        }

        public override void Exit()
        {
            
        }
    }
}
