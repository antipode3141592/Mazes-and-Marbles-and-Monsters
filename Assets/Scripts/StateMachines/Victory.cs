using Chronos;
using FiniteStateMachine;
using MarblesAndMonsters.Menus;
using System;

namespace MarblesAndMonsters.States.GameStates
{
    public class Victory : GameState
    {
        ICharacterManager _characterManager;
        IMenuManager _menuManager;
        ITimeTracker _timeTracker;
        Clock _rootClock;

        public override Type Type { get => typeof(Victory); }

        public Victory(IGameManager manager, IMenuManager menuManager, ICharacterManager characterManager, ITimeTracker timeTracker, Clock rootClock) : base()
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
            _rootClock.localTimeScale = 0f;
            _characterManager.ResetAll();
            _menuManager.OpenMenu(MenuTypes.WinMenu);
        }

        public override Type LogicUpdate(float deltaTime)
        {
            return typeof(START);
        }
    }
}
