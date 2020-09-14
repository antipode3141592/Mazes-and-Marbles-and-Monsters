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
                InitializeReferences();
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

        private void Update()
        {
            gameStateMachine.CurrentState.HandleInput();
            gameStateMachine.CurrentState.LogicUpdate();
        }

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

        //store the locations of each marb
        private void InitializeReferences()
        {
            startTime = DateTime.Now;
            sessionTimeElapsed = new TimeSpan();

            characters = new List<CharacterSheetController>(GameObject.FindObjectsOfType<CharacterSheetController>());
            inventoryItems = new List<InventoryItem>(GameObject.FindObjectsOfType<InventoryItem>());
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }

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
                Player.Instance.DeathCount++;
                DataManager.Instance.PlayerTotalDeathCount = Player.Instance.DeathCount;
                DataManager.Instance.Save();
                gameStateMachine.ChangeState(defeat);
            }
        }

        private IEnumerator WinRoutine()
        {
            yield return new WaitForSeconds(0.5f);
            //TransitionFader.PlayTransition(transitionPrefab);
            //float fadeDelay = transitionPrefab != null ? transitionPrefab.Delay + transitionPrefab.FadeOnDuration : 0f;
            //yield return new WaitForSeconds(fadeDelay);
            WinMenu.Open();
        }

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
            //deactivate all
            DeactivateAll();
            foreach (CharacterSheetController character in characters)
            {
                character.CharacterSpawn();
            }
            foreach (InventoryItem item in inventoryItems)
            {
                item.Reset();
            }
        }

        public void RespawnCharacter(CharacterSheetController character, float respawntime)
        {
            StartCoroutine(RespawnCharacterProcess(character, respawntime));
        }
        
        private IEnumerator RespawnCharacterProcess(CharacterSheetController character, float respawntime)
        {
            yield return new WaitForSeconds(respawntime);
            character.CharacterSpawn();
        }

        //set each active game piece inactive
        public void DeactivateAll()
        {
            foreach (CharacterSheetController character in characters)
            {
                character.CharacterReset();
            }
        }
    }
}
