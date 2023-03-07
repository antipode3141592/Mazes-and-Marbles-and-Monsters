using Chronos;
using FiniteStateMachine;
using MarblesAndMonsters.Menus;
using System;

namespace MarblesAndMonsters.States.GameStates
{
    public class Defeat : GameState
    {
        IMenuManager _menuManager;
        ICharacterManager _characterManager;
        ITimeTracker _timeTracker;
        Clock _rootClock;

        public override Type Type { get => typeof(Defeat); }

        public Defeat(IGameManager manager, IMenuManager menuManager, ICharacterManager characterManager, ITimeTracker timeTracker, Clock rootClock) : base()
        {
            _manager = manager;
            _menuManager = menuManager;
            _characterManager = characterManager;
            _timeTracker = timeTracker;
            _rootClock = rootClock;
        }

        public override void Enter()
        {
            base.Enter();
            _timeTracker.EndLevelTimer();
            _characterManager.ResetAll();
            _rootClock.localTimeScale = 0f;
            _menuManager.OpenMenu(MenuTypes.DefeatMenu);
        }

        public override Type LogicUpdate(float deltaTime)
        {
            return typeof(START);
        }
    }
}
