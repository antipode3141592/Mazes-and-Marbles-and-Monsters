using FiniteStateMachine;
using MarblesAndMonsters.Managers;
using System;
using UnityEngine;

namespace MarblesAndMonsters.States.GameStates
{
    public class PopulateLevel : IGameState
    {
        public Type Type { get => typeof(PopulateLevel); }

        IGameManager _gameManager;
        ICharacterManager _characterManager;
        ITimeTracker _timeTracker;
        ICameraManager _cameraManager;
        IAudioManager _audioManager;

        float timer = 0f;
        readonly float timerMax = 2f;
        int spawnPointReferences = 0;

        public PopulateLevel(IGameManager gameManager, ICharacterManager characterManager, ITimeTracker timeTracker, ICameraManager cameraManager, IAudioManager audioManager)
        {
            _gameManager = gameManager;
            _characterManager = characterManager;
            _timeTracker = timeTracker;
            _cameraManager = cameraManager;
            _audioManager = audioManager;
        }

        public void Enter()
        {
            timer = 0f;
            spawnPointReferences = _characterManager.InitializeReferences();
            _cameraManager.SetFollowCameraPriority(10);
            _audioManager.SetMasterLevel(PlayerPrefs.GetFloat("MasterVolume", 1f));
            _audioManager.SetSFXLevel(PlayerPrefs.GetFloat("SFXVolume", 1f));
            _audioManager.SetMusicLevel(PlayerPrefs.GetFloat("MusicVolume", 1f));
            _audioManager.SetUILevel(PlayerPrefs.GetFloat("UIVolume", 1f));
        }

        public Type LogicUpdate(float deltaTime)
        {
            timer += deltaTime;
            if (timer <= timerMax)
                return typeof(PopulateLevel);
            Debug.Log($"spawnPointReferences count: {spawnPointReferences}");
            if (spawnPointReferences <= 0)
            {
                spawnPointReferences = _characterManager.InitializeReferences();
                timer = 0f;
                return typeof(PopulateLevel);
            }
            _characterManager.SpawnAll();
            return typeof(Playing);
            
        }

        public void Exit()
        {
            _cameraManager.SetFollowCameraPriority(2000);
            _timeTracker.StartLevelTimer();
        }

        public void HandleInput()
        {
            
        }

        public void PhysicsUpdate()
        {
            
        }
    }
}
