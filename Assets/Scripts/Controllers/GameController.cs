using FiniteStateMachine;
using FiniteStateMachine.States.GameStates;
using LevelManagement.Data;
using LevelManagement.Levels;
using LevelManagement.Menus;
using MarblesAndMonsters.Characters;
using MarblesAndMonsters.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace MarblesAndMonsters
{
    public enum DeathType { Falling, Fire, Poison , Damage, Push }

    //persistent singleton class for controlling most of the game actions
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private TransitionFader transitionPrefab;

        [SerializeField]
        private float defaultEffectTime = 3.0f;    //fire, poison, freeze lasts for N seconds

        public Vector2 Input_Acceleration { get; set; }

        private List<CharacterSheetController> characters;
        private List<InventoryItem> inventoryItems;
        private List<SpawnPoint> spawnPoints;

        private TimeSpan sessionTimeElapsed;
        private DateTime startTime;
        private DateTime endTime;

        protected StateMachine gameStateMachine;
        public START start;
        public PopulateLevel populateLevel;
        public Playing playing;
        public Paused paused;
        public Victory victory;
        public Defeat defeat;
        public END end;

        private static GameController _instance;

        public static GameController Instance
        {
            get { return _instance; }
        }

        public float DefaultEffectTime => defaultEffectTime;

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
                DontDestroyOnLoad(gameObject);
                //InitializeReferences();
            }
        }

        private void Start()
        {
            gameStateMachine = new StateMachine();

            start = new START(gameStateMachine);
            populateLevel = new PopulateLevel(gameStateMachine);
            playing = new Playing(gameStateMachine);
            paused = new Paused(gameStateMachine);
            victory = new Victory(gameStateMachine);
            defeat = new Defeat(gameStateMachine);
            end = new END(gameStateMachine);

            gameStateMachine.Initialize(start);
        }

        //gamecontroller doesn't move any rigidbodies, so only needs Update()
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
            gameStateMachine.ChangeState(playing);
            GameMenu.Open();
        }

        public void PauseGame()
        {
            gameStateMachine.ChangeState(paused);
            PauseMenu.Open();
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

        #region LevelManagement

        public void EndLevel(bool isVictorious = true)
        {
            //Time.timeScale = 0f;    //pause time
            if (isVictorious)
            {
                SaveGameData();
                StartCoroutine(WinRoutine());
            }
            else
            {
                if (DataManager.Instance != null)
                {
                    DataManager.Instance.PlayerTotalDeathCount = Player.Instance.DeathCount;  //increment, THEN store, silly
                    DataManager.Instance.Save();
                }
                gameStateMachine.ChangeState(defeat);
            }
        }

        public void LoadNextLevel()
        {
            gameStateMachine.ChangeState(start);
        }

        private IEnumerator WinRoutine()
        {
            yield return new WaitForSeconds(0.5f);
            //TransitionFader.PlayTransition(transitionPrefab);
            //float fadeDelay = transitionPrefab != null ? transitionPrefab.Delay + transitionPrefab.FadeOnDuration : 0f;
            //yield return new WaitForSeconds(fadeDelay);
            WinMenu.Open();
        }

        private IEnumerator DefeatRoutine()
        {
            yield return null;//delay execution for a frame

        }
        #endregion


        public void SaveGameData()
        {
            if (Player.Instance != null)
            {
                //if the Fsm global variables are available, save some of them to disk
                DataManager.Instance.PlayerMaxHealth = Player.Instance.MySheet.MaxHealth;
                DataManager.Instance.PlayerTotalDeathCount = Player.Instance.DeathCount;
                DataManager.Instance.PlayerTreasureCount = Player.Instance.TreasureCount;
                DataManager.Instance.Save();
            }
        }

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

        internal void SpawnAll()
        {
            foreach(SpawnPoint spawnPoint in spawnPoints)
            {
                //StartCoroutine(spawnPoint.Spawn(0.0f));
                spawnPoint.SpawnCharacter();
            }
            foreach (InventoryItem item in inventoryItems)
            {
                //item.Reset();
                if (!item.isActiveAndEnabled) { item.gameObject.SetActive(true); }
            }

        }

        internal int StoreCharacters()
        {
            characters = new List<CharacterSheetController>(FindObjectsOfType<CharacterSheetController>());

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
        internal bool StoreCharacter(CharacterSheetController character)
        {
            if (characters == null) { StoreCharacters(); }
            //{
                if (characters.Contains(character)) { return false; }
                else
                {
                    characters.Add(character);
                    return true;
                }
            //} else
            //{
                //characters = new List<CharacterSheetController>().Add(character);
            //}
            //return false;
        }


        //move all characters
        //  currently just moves characters.  if any objects/traps/whatever get movements, move is now an interface so this functions
        //  refactor will be easier to collect all available Moves and execute them
        public void MoveAll()
        {
            if (characters != null)
            {
                foreach (CharacterSheetController character in characters)
                {
                    if (character.gameObject.activeInHierarchy && !character.MySheet.IsAsleep)
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
