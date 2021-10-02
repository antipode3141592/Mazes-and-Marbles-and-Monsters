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

        [SerializeField]
        private float defaultEffectTime = 3.0f;    //fire, poison, freeze lasts for N seconds
        [SerializeField]
        private float defaultInvincibilityTime = 1.0f;

        public Vector2 Input_Acceleration { get; set; }

        //references to various game objects
        private List<CharacterControl> characters;
        private List<InventoryItem> inventoryItems;
        private List<SpellPickupBase> spellPickups;
        private List<KeyItem> keyItems;
        private List<SpawnPoint> spawnPoints;
        private List<Gate> gates;
        private List<SpellEffectBase> spellEffects;

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

        public float DefaultEffectTime => defaultEffectTime;

        public float DefaultInvincibilityTime => defaultInvincibilityTime;

        public bool ShouldBeginLevel = false;
        #endregion

        [Inject]
        public void Init(DataManager dataManager, MenuManager menuManager, LevelManager levelManager)
        {
            _dataManager = dataManager;
            _menuManager = menuManager;
            _levelManager = levelManager;
        }

        #region Unity Overrides

        private void Awake()
        {
            stateMachine = GetComponent<StateMachine>();
            var states = new Dictionary<Type, BaseState>()
            {
                {typeof(START), new START(manager: this) },
                {typeof(PopulateLevel), new PopulateLevel(manager: this) },
                {typeof(Playing), new Playing(manager: this) },
                {typeof(Paused), new Paused(manager: this) },
                {typeof(Victory), new Victory(manager: this) },
                {typeof(Defeat), new Defeat(manager: this, menuManager: _menuManager) },
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
        
        /// <summary>
        /// cache references to all spawnpoints, inventory items, gates, and keys
        /// </summary>
        /// <returns>spawnpoint count (-1 if none found)</returns>
        public int InitializeReferences()
        {
            //TODO this doesn't find 
            inventoryItems = new List<InventoryItem>(FindObjectsOfType<InventoryItem>());
            keyItems = new List<KeyItem>(FindObjectsOfType<KeyItem>());
            spawnPoints = new List<SpawnPoint>(FindObjectsOfType<SpawnPoint>());
            gates = new List<Gate>(FindObjectsOfType<Gate>());
            spellPickups = new List<SpellPickupBase>(FindObjectsOfType<SpellPickupBase>());
            spellEffects = new List<SpellEffectBase>();
            if (spawnPoints.Count == 0) 
            { 
                //player start position is a spawnpoint, so a valid level shall have at least 1 spawnpoint
                return -1; 
            }
            else
            {
                return spawnPoints.Count;
            }
        }

        /// <summary>
        /// Reset all objects
        /// </summary>
        internal void ResetAll()
        {
            foreach(Gate gate in gates)
            {
                gate.Lock();
            }

            foreach (InventoryItem inventoryItem in inventoryItems)
            {
                inventoryItem.Reset();
            }
            foreach (SpawnPoint spawnPoint in spawnPoints)
            {
                spawnPoint.isAvailable = false;
                spawnPoint.Reset();
            }
            foreach (KeyItem keyItem in keyItems)
            {
                keyItem.Reset();
            }
            foreach (SpellPickupBase spellPickup in spellPickups)
            {
                spellPickup.Reset();
            }
            foreach(SpellEffectBase spellEffect in spellEffects.FindAll(x => x != null))
            {
                spellEffect.EndEffect();
            }
            foreach(Projectile projectile in FindObjectsOfType<Projectile>())
            {
                Destroy(projectile, 0.01f);
            }
            //Destroy(Player.Instance);
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

        internal void SpawnAll()
        {
            foreach(SpawnPoint spawnPoint in spawnPoints)
            {
                //StartCoroutine(spawnPoint.Spawn(0.0f));
                //some spawnPoints begin in an unavailable state (and made available by some trigger)
                spawnPoint.isAvailable = true;
                spawnPoint.SpawnCharacter();
            }
            foreach (InventoryItem item in inventoryItems)
            {
                //item.Reset();
                if (!item.isActiveAndEnabled) { item.gameObject.SetActive(true); }
            }

        }

        #region Character Management

        internal int StoreCharacters()
        {
            characters = new List<CharacterControl>(FindObjectsOfType<CharacterControl>());

            string charlist = "";
            foreach(var character in characters) { charlist += String.Format("{0}, ",character.gameObject.name); }
            //Debug.Log(String.Format("there are {0} characters : {1}", characters.Count,charlist));
            if (characters.Count > 0)
            {
                return characters.Count;
            }
            else
            {
                return -1;
            }
        }

        //return true if a character was added
        internal bool StoreCharacter(Characters.CharacterControl character)
        {
            if (characters == null) { StoreCharacters(); }
            if (characters.Contains(character)) { return false; }
            else
            {
                characters.Add(character);
                return true;
            }
        }
        #endregion


        //move all characters
        public void MoveAll()
        {
            try 
            {
                foreach (CharacterControl character in characters.FindAll(x => x != null))
                {
                    if (character.gameObject.activeInHierarchy && character.MySheet.IsBoardMovable)
                    {
                        BoardMovement.Move(character.MyRigidbody, Input_Acceleration, character.ForceMultiplier);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex.Message);
            }

        }

        public void StopAllCharacters(List<CharacterControl> exceptionList = null)
        {
            if (exceptionList != null)
            {
                
            }
        }
    }
}
