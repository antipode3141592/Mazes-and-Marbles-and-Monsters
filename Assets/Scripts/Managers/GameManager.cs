using FiniteStateMachine;
using FiniteStateMachine.States.GameStates;
using LevelManagement;
using LevelManagement.Data;
using MarblesAndMonsters.Characters;
using MarblesAndMonsters.Items;
using MarblesAndMonsters.Menus;
using MarblesAndMonsters.Tiles;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MarblesAndMonsters
{
    public enum DeathType { Falling, Fire, Poison , Damage, Push }

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

        public Vector2 Input_Acceleration { get; set; }

        //references to various game objects
        private List<Characters.CharacterControl> characters;
        private List<InventoryItem> inventoryItems;
        private List<KeyItem> keyItems;
        private List<SpawnPoint> spawnPoints;
        private List<Gate> gates;

        //for tracking playtime
        private TimeSpan sessionTimeElapsed;
        private DateTime startTime;
        private DateTime endTime;

        //state machine stuff
        protected StateMachine gameStateMachine;
        public START state_start;
        public PopulateLevel state_populateLevel;
        public Playing state_playing;
        public Paused state_paused;
        public Victory state_victory;
        public Defeat state_defeat;
        public END state_end;

        public State CurrentState => gameStateMachine.CurrentState;

        private LevelManager levelLoader;
        [SerializeField]
        private Tilemap spawnPointTileMap;

        //singleton stuff
        private static GameManager _instance;

        public static GameManager Instance
        {
            get { return _instance; }
        }

        public float DefaultEffectTime => defaultEffectTime;
        #endregion

        #region Unity Overrides

        private void Awake()
        {
            //that singleton pattern
            if (_instance != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        private void Start()
        {
            gameStateMachine = new StateMachine();

            state_start = new START(gameStateMachine);
            state_populateLevel = new PopulateLevel(gameStateMachine);
            state_playing = new Playing(gameStateMachine);
            state_paused = new Paused(gameStateMachine);
            state_victory = new Victory(gameStateMachine);
            state_defeat = new Defeat(gameStateMachine);
            state_end = new END(gameStateMachine);

            levelLoader = FindObjectOfType<LevelManager>();

            gameStateMachine.Initialize(state_start);


        }

        public void Update()
        {
            gameStateMachine.CurrentState.HandleInput();
            gameStateMachine.CurrentState.LogicUpdate();
        }

        public void FixedUpdate()
        {
            gameStateMachine.CurrentState.PhysicsUpdate();   
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
        #endregion

        public void ResetStateMachine()
        {
            gameStateMachine.ChangeState(state_start);
        }

        public void UnpauseGame()
        {
            gameStateMachine.ChangeState(state_playing);
            MenuManager.Instance.OpenMenu(MenuTypes.GameMenu);
        }

        public void PauseGame()
        {
            gameStateMachine.ChangeState(state_paused);
            MenuManager.Instance.OpenMenu(MenuTypes.PauseMenu);
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

            ////log the 
            //string spawnlist = "";
            //string inventoryList = "";
            //foreach (SpawnPoint _spawnpoint in spawnPoints) { spawnlist += String.Format("{0}, ", _spawnpoint.gameObject.name); }
            //foreach (InventoryItem _inventory in inventoryItems) { inventoryList += String.Format("{0}, ", _inventory.gameObject.name); }
            //Debug.Log(String.Format("InitializeReferences(), there are {0} spawnpoints : {1}", spawnPoints.Count, spawnlist));
            //Debug.Log(String.Format("InitializeReferences(), there are {0} inventoryItems : {1}", inventoryItems.Count, inventoryList));
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
                spawnPoint.Reset();
            }
            foreach (KeyItem keyItem in keyItems)
            {
                keyItem.Reset();
            }
        }

        #region LevelManagement

        public void LevelWin()
        {
            gameStateMachine.ChangeState(state_victory);
            SaveGameData();
            StartCoroutine(WinRoutine());
        }

        public void LevelWin(string goToLevelId)
        {
            gameStateMachine.ChangeState(state_victory);
            SaveGameData();
            StartCoroutine(WinRoutine(goToLevelId));
        }

        public void LevelLose()
        {
            if (DataManager.Instance != null)
            {
                DataManager.Instance.PlayerTotalDeathCount = Player.Instance.DeathCount;  //increment, THEN store, silly
                DataManager.Instance.Save();
            }
            gameStateMachine.ChangeState(state_defeat);
        }

        private IEnumerator WinRoutine()
        {
            //TransitionFader.PlayTransition(endTransition);
            //yield return new WaitForSeconds(0.5f);
            //TransitionFader.PlayTransition(transitionPrefab);
            levelLoader.LoadLevel(string.Empty);
            //float fadeDelay = endTransition != null ? endTransition.Delay + endTransition.FadeOnDuration : 0f;
            yield return null;
        }

        private IEnumerator WinRoutine(string levelId)
        {
            //TransitionFader.PlayTransition(endTransition);
            //yield return new WaitForSeconds(0.5f);
            //TransitionFader.PlayTransition(transitionPrefab);
            levelLoader.LoadLevel(levelId);
            //float fadeDelay = endTransition != null ? endTransition.Delay + endTransition.FadeOnDuration : 0f;
            yield return null;
            
        }

        private IEnumerator DefeatRoutine()
        {
            yield return null;//delay execution for a frame

        }
        #endregion


        public void SaveGameData()
        {
            if (DataManager.Instance != null)
            {
                if (Player.Instance != null)
                {
                    DataManager.Instance.PlayerMaxHealth = Player.Instance.MySheet.MaxHealth;
                    DataManager.Instance.PlayerCurrentHealth = Player.Instance.MySheet.CurrentHealth;
                    DataManager.Instance.PlayerTotalDeathCount = Player.Instance.DeathCount;
                    DataManager.Instance.PlayerTreasureCount = Player.Instance.TreasureCount;
                    if (DataManager.Instance.CheckPointLevelId != string.Empty)
                    {
                        DataManager.Instance.UpdateLevelSaves(new LevelSaveData(DataManager.Instance.CheckPointLevelId,
                            DataManager.Instance.SavedLocation, 0, true));
                    } else
                    {
                        string levelId = LevelManager.Instance.GetCurrentLevelId();
                        DataManager.Instance.UpdateLevelSaves(new LevelSaveData(levelId, 
                            LevelManager.Instance.GetLevelSpecsById(levelId).Location, 0, true));
                    }
                    //DataManager.Instance.UpdateLocationSaves(new LocationSaveData(DataManager.Instance.loc))
                    DataManager.Instance.Save();
                } else
                {
                    Debug.LogWarning("No Player Instance found when attempting to save!");
                }
            }
            else { Debug.LogWarning("No DataManager Instance found when attempting to save!"); }
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
                if (spawnPoint.isAvailable)
                {
                    spawnPoint.SpawnCharacter();
                }
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
            Debug.Log(String.Format("there are {0} characters : {1}", characters.Count,charlist));
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
        //  currently just moves characters.  if any objects/traps/whatever get movements, move is now an interface so this functions
        //  refactor will be easier to collect all available Moves and execute them
        public void MoveAll()
        {
            if (characters != null)
            {
                foreach (CharacterControl character in characters)
                {
                    if (character != null && character.gameObject.activeInHierarchy && !character.MySheet.IsAsleep && !character.isDying )
                    {
                        if (character.MySheet.Movements.Count > 0)
                        {
                            foreach (Movement movement in character.MySheet.Movements)
                            {
                                movement.Move();
                            }
                        }
                    }
                }
            } else {
                Debug.LogError("Characters list null, calling StoreCharacters()");
                StoreCharacters(); 
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
