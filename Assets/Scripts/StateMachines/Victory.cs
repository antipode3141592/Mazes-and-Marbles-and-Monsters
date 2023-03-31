using Chronos;
using FiniteStateMachine;
using MarblesAndMonsters.Menus;
using System;

namespace MarblesAndMonsters.States.GameStates
{
    public class Victory : IGameState
    {
        IGameManager _gamemanager;
        ICharacterManager _characterManager;
        IMenuManager _menuManager;
        ITimeTracker _timeTracker;
        Clock _rootClock;

        public Type Type { get => typeof(Victory); }

        public Victory(IGameManager gameManager, IMenuManager menuManager, ICharacterManager characterManager, ITimeTracker timeTracker, Clock rootClock)
        {
            _gamemanager = gameManager;
            _menuManager = menuManager;
            _characterManager = characterManager;
            _timeTracker = timeTracker;
            _rootClock = rootClock;
        }

        public void Enter()
        {
            _timeTracker.EndLevelTimer();
            _rootClock.localTimeScale = 0f;
            _characterManager.ResetAll();
            _menuManager.OpenMenu(MenuTypes.WinMenu);
        }

        public Type LogicUpdate(float deltaTime)
        {
            return typeof(END);
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
