﻿using Chronos;
using FiniteStateMachine;
using LevelManagement;
using LevelManagement.DataPersistence;
using MarblesAndMonsters.Characters;
using MarblesAndMonsters.Managers;
using MarblesAndMonsters.Menus;
using MarblesAndMonsters.States.GameStates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace MarblesAndMonsters
{
    public class GameManager : MonoBehaviour, IGameManager
    {
        #region Properties
        [SerializeField] TransitionFader startTransition;
        [SerializeField] TransitionFader endTransition;
        [SerializeField] Clock _rootClock; 

        //state machine stuff
        protected StateMachine stateMachine;

        public BaseState CurrentState => stateMachine.CurrentState;

        protected ILevelManager _levelManager;
        protected IDataManager _dataManager;
        protected IMenuManager _menuManager;
        protected ICharacterManager _characterManager;
        protected IInputManager _inputManager;
        protected ITimeTracker _timeTracker;
        protected ICameraManager _cameraManager;
        protected IAudioManager _audioManager;

        public bool ShouldBeginLevel { get; set; } = false;
        public bool ShouldLoadNextLevel { get; set; } = false;
        #endregion

        [Inject]
        public void Init(IDataManager dataManager, IMenuManager menuManager, ILevelManager levelManager, ICharacterManager characterManager, IInputManager inputManager, ITimeTracker timeTracker, ICameraManager cameraManager, IAudioManager audioManager)
        {
            _dataManager = dataManager;
            _menuManager = menuManager;
            _levelManager = levelManager;
            _characterManager = characterManager;
            _inputManager = inputManager;
            _timeTracker = timeTracker;
            _cameraManager = cameraManager;
            _audioManager = audioManager;
        }

        #region Unity Overrides

        void Awake()
        {
            stateMachine = GetComponent<StateMachine>();
            var timeTracker = FindObjectOfType<TimeTracker>();
            var states = new Dictionary<Type, BaseState>()
            {
                {typeof(START), new START(manager: this) },
                {typeof(PopulateLevel), new PopulateLevel(manager: this, characterManager: _characterManager, timeTracker: _timeTracker, cameraManager: _cameraManager, audioManager: _audioManager) },
                {typeof(Playing), new Playing(manager: this, inputManager: _inputManager, characterManager: _characterManager, rootClock: _rootClock) },
                {typeof(Paused), new Paused(manager: this, timeTracker, rootClock: _rootClock) },
                {typeof(Victory), new Victory(manager: this, menuManager: _menuManager, characterManager: _characterManager, timeTracker: _timeTracker, rootClock: _rootClock) },
                {typeof(Defeat), new Defeat(manager: this, menuManager: _menuManager, characterManager: _characterManager, timeTracker: _timeTracker, rootClock: _rootClock) },
                {typeof(END), new END(manager: this) }
            };
            Debug.Log($"{name} is storing the following states:  {states.Keys}");
            stateMachine.SetStates(states);
        }
        #endregion

        public void UnpauseGame()
        {
            stateMachine.SwitchToNewState(typeof(Playing));
            _menuManager.OpenMenu(MenuTypes.GameMenu);
        }

        public void PauseGame()
        {
            stateMachine.SwitchToNewState(typeof(Paused));
            _menuManager.OpenMenu(MenuTypes.PauseMenu);
        }

        #region LevelManagement
        public void LevelWin(string goToLevelId)
        {
            stateMachine.SwitchToNewState(typeof(Victory));
            SaveGameData();
            StartCoroutine(WinRoutine(goToLevelId));
        }

        public void LevelLose()
        {
            if (_dataManager != null)
            {
                _dataManager.PlayerTotalDeathCount = Player.Instance.DeathCount;
                _dataManager.Save();
            }
            stateMachine.SwitchToNewState(typeof(Defeat));
        }

        IEnumerator WinRoutine(string levelId)
        {

            //TransitionFader.PlayTransition(endTransition);
            //yield return new WaitForSeconds(0.5f);
            //TransitionFader.PlayTransition(transitionPrefab);
            if (Debug.isDebugBuild)
                Debug.Log($"WinRoutine({levelId}): awaiting ShouldLoadNextLevel...", this);
            while (!ShouldLoadNextLevel)
                yield return null;

            if (Debug.isDebugBuild)
                Debug.Log($"Load Next Level", this);
            _levelManager.LoadLevel(levelId);
            //float fadeDelay = endTransition != null ? endTransition.Delay + endTransition.FadeOnDuration : 0f;
            yield return null;

        }
        #endregion

        //TODO move this to a subcontroller
        public void SaveGameData()
        {
            if (_dataManager is null)
                return;
            if (Player.Instance is null)
                return;
            _dataManager.PlayerMaxHealth = Player.Instance.MySheet.MaxHealth;
            _dataManager.PlayerCurrentHealth = Player.Instance.MySheet.CurrentHealth;
            _dataManager.PlayerTotalDeathCount = Player.Instance.DeathCount;
            _dataManager.PlayerScrollCount = Player.Instance.TreasureCount;
            if (_dataManager.CheckPointLevelId != string.Empty)
            {
                _dataManager.UpdateLevelSaves(new LevelSaveData(_dataManager.CheckPointLevelId,
                    _dataManager.SavedLocation, 0, true, _timeTracker.LevelTime));
            }
            else
            {
                if (_levelManager is null)
                {
                    Debug.LogWarning($"No LevelManager, skipping Save");
                    return;
                }
                string levelId = _levelManager.GetCurrentLevelId();
                _dataManager.UpdateLevelSaves(new LevelSaveData(levelId,
                    _levelManager.GetLevelSpecsById(levelId).Location, 0, true, _timeTracker.LevelTime));
            }
            //store unlocked spells
            foreach (var spell in Player.Instance.MySheet.Spells)
            {
                if (spell.Value.IsUnlocked)
                {
                    _dataManager.UnlockedSpells.Add(new SpellData(spell.Value.SpellName, spell.Value.SpellStats, spell.Value.IsQuickSlotAssigned, spell.Value.QuickSlot));
                }
            }
            _dataManager.Save();
        }
    }
}
