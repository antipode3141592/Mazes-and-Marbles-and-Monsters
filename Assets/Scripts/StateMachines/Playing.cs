using Chronos;
using FiniteStateMachine;
using MarblesAndMonsters.Menus;
using System;
using UnityEngine;

namespace MarblesAndMonsters.States.GameStates
{
    public class Playing : IGameState
    {
        public Type Type { get => typeof(Playing); }

        IGameManager _gameManager;
        IInputManager _inputManager;
        ICharacterManager _characterManager;
        IMenuManager _menuManager;
        Clock _rootClock;

        public Playing(IGameManager gameManager, IInputManager inputManager, ICharacterManager characterManager, IMenuManager menuManager, Clock rootClock)
        {
            _gameManager = gameManager;
            _inputManager = inputManager;
            _characterManager = characterManager;
            _menuManager = menuManager;
            _rootClock = rootClock;
        }

        public void Enter()
        {
            _rootClock.localTimeScale = 1f;
            _characterManager.SetAccelerometerSensitivity(PlayerPrefs.GetFloat("Sensitivity", 1f));
            _menuManager.OpenMenu(MenuTypes.GameMenu);
        }

        public void HandleInput()
        {
            _inputManager.MeasureBoardTilt();
        }

        public void PhysicsUpdate()
        {
            _characterManager.MoveAll();  //characters should only move during play
        }

        public Type LogicUpdate(float deltaTime)
        {
            return (typeof(Playing));
        }

        public void Exit()
        {
            
        }
    }
}
