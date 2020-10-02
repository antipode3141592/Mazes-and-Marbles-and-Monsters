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
                InitializeReferences();
            }
        }

        internal void CheckOutofBounds()
        {
            foreach (CharacterSheetController character in characters)
            {
                if (character != null)
                {
                    if (!IsWithinRect(character.gameObject.transform.position))
                    {
                        character.CharacterDeath();
                    }
                }
            }
        }

        private bool IsWithinRect(Vector3 position)
        {
            if ((Math.Abs(position.x) > 1200f) || (Math.Abs(position.y) > 1200f))
            {
                return false;
            } else
            {
                return true;
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
        private void Update()
        {
            gameStateMachine.CurrentState.HandleInput();
            gameStateMachine.CurrentState.LogicUpdate();
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
            //GameMenu.Open();
        }

        public void PauseGame()
        {
            gameStateMachine.ChangeState(paused);
            //PauseMenu.Open();
        }

        protected void InitializeReferences()
        {
            //characters = new List<CharacterSheetController>(GameObject.FindObjectsOfType<CharacterSheetController>());
            inventoryItems = new List<InventoryItem>(GameObject.FindObjectsOfType<InventoryItem>());
            spawnPoints = new List<SpawnPoint>(GameObject.FindObjectsOfType<SpawnPoint>());
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

        public void SpawnAll()
        {
            foreach(SpawnPoint spawnPoint in spawnPoints)
            {
                spawnPoint.QueueAll();
            }
            foreach (InventoryItem item in inventoryItems)
            {
                item.Reset();
            }
        }
    }
}
