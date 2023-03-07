using FiniteStateMachine;
using System;
using UnityEngine;

namespace MarblesAndMonsters.States.GameStates
{
    public class PopulateLevel : GameState
    {
        public override Type Type { get => typeof(PopulateLevel); }

        ICharacterManager _characterManager;
        ITimeTracker _timeTracker;
        ICameraManager _cameraManager;

        float timer = 0f;
        float timerMax = 2f;

        public PopulateLevel(IGameManager manager, ICharacterManager characterManager, ITimeTracker timeTracker, ICameraManager cameraManager) : base()
        {
            _manager = manager;
            _characterManager = characterManager;
            _timeTracker = timeTracker;
            _cameraManager = cameraManager;
        }

        public override void Enter()
        {
            timer = 0f;
            _characterManager.InitializeReferences();
            _cameraManager.SetFollowCameraPriority(10);
            
        }

        public override Type LogicUpdate(float deltaTime)
        {
            timer += deltaTime;
            if (timer > timerMax)
            {
                _characterManager.SpawnAll();
                return typeof(Playing);
            }
            return typeof(PopulateLevel);
        }

        public override void Exit()
        {
            _cameraManager.SetFollowCameraPriority(2000);
            _timeTracker.StartLevelTimer();
        }
    }
}
