using FiniteStateMachine;
using LevelManagement;
using LevelManagement.DataPersistence;
using MarblesAndMonsters.Characters;
using MarblesAndMonsters.Items;
using MarblesAndMonsters.Menus;
using MarblesAndMonsters.Spells;
using MarblesAndMonsters.States.GameStates;
using MarblesAndMonsters.Tiles;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace MarblesAndMonsters
{
    //persistent singleton class for controlling most of the game actions
    //  Dependencies:
    //      MenuManager singleton instance for menu functions
    //      DataManager singleton instance for saving/loading persistent data
    //      Player singleton instance
    public class GameManager : MonoBehaviour
    {
        #region Properties
        [SerializeField]
        private TransitionFader startTransition;
        [SerializeField]
        private TransitionFader endTransition;

        //for tracking playtime
        private TimeSpan sessionTimeElapsed;
        private DateTime startTime;
        private DateTime endTime;

        //state machine stuff
        protected StateMachine stateMachine;

        public BaseState CurrentState => stateMachine.CurrentState;

        protected LevelManager _levelManager;
        protected DataManager _dataManager;
        protected MenuManager _menuManager;
        protected CharacterManager _characterManager;

        public bool ShouldBeginLevel = false;
        #endregion

        [Inject]
        public void Init(DataManager dataManager, MenuManager menuManager, LevelManager levelManager, CharacterManager characterManager)
        {
            _dataManager = dataManager;
            _menuManager = menuManager;
            _levelManager = levelManager;
            _characterManager = characterManager;
        }

        #region Unity Overrides

        private void Awake()
        {
            stateMachine = GetComponent<StateMachine>();
            var states = new Dictionary<Type, BaseState>()
            {
                {typeof(START), new START(manager: this) },
                {typeof(PopulateLevel), new PopulateLevel(manager: this, characterManager: _characterManager) },
                {typeof(Playing), new Playing(manager: this, characterManager: _characterManager) },
                {typeof(Paused), new Paused(manager: this) },
                {typeof(Victory), new Victory(manager: this, characterManager: _characterManager) },
                {typeof(Defeat), new Defeat(manager: this, menuManager: _menuManager, characterManager: _characterManager) },
                {typeof(END), new END(manager: this) }
            };
            Debug.Log($"{name} is storing the following states:  {states.Keys.ToString()}");
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

        public void LevelWin()
        {
            stateMachine.SwitchToNewState(typeof(Victory));
            SaveGameData();
            StartCoroutine(WinRoutine());
        }

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

        private IEnumerator WinRoutine()
        {
            //TransitionFader.PlayTransition(endTransition);
            //yield return new WaitForSeconds(0.5f);
            //TransitionFader.PlayTransition(transitionPrefab);
            _levelManager.LoadLevel(string.Empty);
            //float fadeDelay = endTransition != null ? endTransition.Delay + endTransition.FadeOnDuration : 0f;
            yield return null;
        }

        private IEnumerator WinRoutine(string levelId)
        {

            //TransitionFader.PlayTransition(endTransition);
            //yield return new WaitForSeconds(0.5f);
            //TransitionFader.PlayTransition(transitionPrefab);
            _levelManager.LoadLevel(levelId);
            //float fadeDelay = endTransition != null ? endTransition.Delay + endTransition.FadeOnDuration : 0f;
            yield return null;
            
        }

        private IEnumerator DefeatRoutine()
        {
            yield return null;//delay execution for a frame

        }
        #endregion

        //TODO move this to a subcontroller
        public void SaveGameData()
        {
            try
            {
                if (_dataManager != null)
                {
                    if (Player.Instance != null)
                    {
                        _dataManager.PlayerMaxHealth = Player.Instance.MySheet.MaxHealth;
                        _dataManager.PlayerCurrentHealth = Player.Instance.MySheet.CurrentHealth;
                        _dataManager.PlayerTotalDeathCount = Player.Instance.DeathCount;
                        _dataManager.PlayerScrollCount = Player.Instance.TreasureCount;
                        if (_dataManager.CheckPointLevelId != string.Empty)
                        {
                            _dataManager.UpdateLevelSaves(new LevelSaveData(_dataManager.CheckPointLevelId,
                                _dataManager.SavedLocation, 0, true));
                        }
                        else
                        {
                            if (_levelManager != null)
                            {
                                string levelId = _levelManager.GetCurrentLevelId();
                                _dataManager.UpdateLevelSaves(new LevelSaveData(levelId,
                                    _levelManager.GetLevelSpecsById(levelId).Location, 0, true));
                            } else
                            {
                                throw new Exception("No LevelManager found for GetCurrentLevelId");
                            }
                        }
                        //store unlocked spells
                        foreach (var spell in Player.Instance.MySheet.Spells)
                        {
                            if (spell.Value.IsUnlocked)
                            {
                                _dataManager.UnlockedSpells.Add(new SpellData(spell.Value.SpellName, spell.Value.SpellStats, spell.Value.IsQuickSlotAssigned, spell.Value.QuickSlot));
                            }
                        }
                        //store collected keys
                        foreach (var key in Player.Instance.KeyChain)
                        {
                            _dataManager.CollectedKeys.Add(key);
                        }
                        _dataManager.Save();
                    }
                    else
                    {
                        throw new Exception("No Player Instance found when attempting to save!");
                    }
                }
                else 
                { 
                    throw new Exception("No DataManager Instance found when attempting to save!"); 
                }
            }
            catch(Exception ex)
            {
                Debug.LogError(string.Format("{0}", ex.Message));
            }
            finally
            {

            }
            
        }


        #region Time Tracking
        public void UpdateSessionTime()
        {
            endTime = DateTime.Now;
            sessionTimeElapsed += endTime - startTime;
            startTime = DateTime.Now;   //update 
        }

        public void StartSessionTime()
        {
            startTime = DateTime.Now;
        }
        #endregion
    }
}
