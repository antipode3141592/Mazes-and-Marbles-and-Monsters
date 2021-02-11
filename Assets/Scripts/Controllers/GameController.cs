using FiniteStateMachine;
using FiniteStateMachine.States.GameStates;
using LevelManagement;
using LevelManagement.Data;
using MarblesAndMonsters.Characters;
using MarblesAndMonsters.Items;
using MarblesAndMonsters.Menus;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters
{
    public enum DeathType { Falling, Fire, Poison , Damage, Push }

    //persistent singleton class for controlling most of the game actions
    //  Dependencies:
    //      MenuManager singleton instance for menu functions
    //      DataManager singleton instance for saving/loading persistent data
    //      Player singleton instance
    public class GameController : MonoBehaviour
    {
        #region Properties
        [SerializeField]
        private TransitionFader transitionPrefab;

        [SerializeField]
        private float defaultEffectTime = 3.0f;    //fire, poison, freeze lasts for N seconds

        public Vector2 Input_Acceleration { get; set; }

        //references to various game objects
        private List<Characters.CharacterControl> characters;
        private List<InventoryItem> inventoryItems;
        private List<SpawnPoint> spawnPoints;

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

        private LevelLoader levelLoader;

        //singleton stuff
        private static GameController _instance;

        public static GameController Instance
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
                //DontDestroyOnLoad(gameObject);
                //InitializeReferences();
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

            levelLoader = FindObjectOfType<LevelLoader>();

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
        
        public int InitializeReferences()
        {
            //TODO this doesn't find 
            inventoryItems = new List<InventoryItem>(GameObject.FindObjectsOfType<InventoryItem>());
            
            spawnPoints = new List<SpawnPoint>(GameObject.FindObjectsOfType<SpawnPoint>());

            string spawnlist = "";
            string inventoryList = "";
            foreach (SpawnPoint _spawnpoint in spawnPoints) { spawnlist += String.Format("{0}, ", _spawnpoint.gameObject.name); }
            foreach (InventoryItem _inventory in inventoryItems) { inventoryList += String.Format("{0}, ", _inventory.gameObject.name); }
            Debug.Log(String.Format("InitializeReferences(), there are {0} spawnpoints : {1}", spawnPoints.Count, spawnlist));
            Debug.Log(String.Format("InitializeReferences(), there are {0} inventoryItems : {1}", inventoryItems.Count, inventoryList));
            if (spawnPoints.Count == 0) 
            { 
                return -1; 
            }
            else
            {
                return spawnPoints.Count;
            }
        }

        internal void ResetAll()
        {
            foreach(SpawnPoint spawnPoint in spawnPoints)
            {
                spawnPoint.Reset();
            }

            foreach (InventoryItem inventoryItem in inventoryItems)
            {
                inventoryItem.Reset();
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
            TransitionFader.PlayTransition(transitionPrefab);
            //yield return new WaitForSeconds(0.5f);
            //TransitionFader.PlayTransition(transitionPrefab);
            float fadeDelay = transitionPrefab != null ? transitionPrefab.Delay + transitionPrefab.FadeOnDuration : 0f;
            yield return new WaitForSeconds(fadeDelay);
            levelLoader.LoadNextLevel();
        }

        private IEnumerator WinRoutine(string levelId)
        {
            TransitionFader.PlayTransition(transitionPrefab);
            //yield return new WaitForSeconds(0.5f);
            //TransitionFader.PlayTransition(transitionPrefab);
            float fadeDelay = transitionPrefab != null ? transitionPrefab.Delay + transitionPrefab.FadeOnDuration : 0f;
            yield return new WaitForSeconds(fadeDelay);
            levelLoader.LoadLevel(levelId);
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
                    //if the Fsm global variables are available, save some of them to disk
                    DataManager.Instance.PlayerMaxHealth = Player.Instance.MySheet.MaxHealth;
                    DataManager.Instance.PlayerTotalDeathCount = Player.Instance.DeathCount;
                    DataManager.Instance.PlayerTreasureCount = Player.Instance.TreasureCount;
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
                foreach (Characters.CharacterControl character in characters)
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
    }
}
