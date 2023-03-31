using Chronos;
using FiniteStateMachine;
using MarblesAndMonsters.Menus;
using System;

namespace MarblesAndMonsters.States.GameStates
{
    public class Defeat : IGameState
    {
        IGameManager _gameManager;
        IMenuManager _menuManager;
        ICharacterManager _characterManager;
        ITimeTracker _timeTracker;
        Clock _rootClock;

        public Type Type { get => typeof(Defeat); }

        public Defeat(IGameManager gameManager, IMenuManager menuManager, ICharacterManager characterManager, ITimeTracker timeTracker, Clock rootClock)
        {
            _gameManager = gameManager;
            _menuManager = menuManager;
            _characterManager = characterManager;
            _timeTracker = timeTracker;
            _rootClock = rootClock;
        }

        public void Enter()
        {
            _timeTracker.EndLevelTimer();
            _characterManager.ResetAll();
            _rootClock.localTimeScale = 0f;
            _menuManager.OpenMenu(MenuTypes.DefeatMenu);
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
