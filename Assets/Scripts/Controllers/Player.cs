using LevelManagement.Data;
using LevelManagement.Menus;
using MarblesAndMonsters.Items;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace MarblesAndMonsters.Characters
{
    // Player is a special type of Character Sheet Controller
    //  singleton pattern
    public class Player : CharacterSheetController
    {
        #region Properties
        //special player-only effects
        public ParticleSystem treasureEffect;
        public ParticleSystem addMaxHealthEffect;

        //
        private CinemachineVirtualCamera followCamera;  //some/all of the levels will have a camera follow the player around

        private int deathCount = 0;
        private int treasureCount = 0;
        private List<ItemStats> inventory;
        [SerializeField]
        private static readonly int inventoryMaxSize = 5;

        public List<ItemStats> Inventory => inventory;//read only accessor
                                                          //HealthBarController healthBarController;

        //singleton stuff
        private static Player _instance;
        public static Player Instance   //singleton accessor
        {
            get { return _instance; }
        }

        //accessors
        public int TreasureCount { get { return treasureCount; } set { treasureCount = value; } }
        public int DeathCount { get { return deathCount; } set { deathCount = value; } }
        #endregion

        #region Unity Functions
        protected override void Awake()
        {
            //singleton check
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = (Player)this;
                //DontDestroyOnLoad(gameObject);
            }

            inventory = new List<ItemStats>();
            base.Awake();
        }

        protected override void Start()
        {
            followCamera = FindObjectOfType<CinemachineVirtualCamera>();
            if (followCamera != null)
            {
                //set the 
                followCamera.Follow = gameObject.transform;
            }
            if (DataManager.Instance != null)
            {
                deathCount = DataManager.Instance.PlayerTotalDeathCount > 0 ? DataManager.Instance.PlayerTotalDeathCount : 0;
                treasureCount = DataManager.Instance.PlayerTreasureCount > 0 ? DataManager.Instance.PlayerTreasureCount : 0;
                mySheet.MaxHealth = DataManager.Instance.PlayerMaxHealth > 3 ? DataManager.Instance.PlayerMaxHealth : 3;
            } else
            {
                deathCount = 0;
                treasureCount = 0;
                mySheet.MaxHealth = 3;
            }
            base.Start();
        }

        //protected override void Update()
        //{
        //    base.Update();
        //    SetLookDirection();
        //}

        protected override void OnEnable()
        {
            base.OnEnable();
            GameMenu.Instance.RefreshUI();
        }

        //protected override void OnDisable()
        //{
        //    //base.OnDisable();
        //    Debug.Log("Player OnDisable() script");
        //}

        //cleanup for static instance
        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
        #endregion

        public void AddMaxHealth(int amount)
        {
            //adjust max health
            mySheet.MaxHealth += amount;
            //trigger animation
            //trigger particle effects
            addMaxHealthEffect.Play();
            //
            GameMenu.Instance.AddMaxHealthUI(amount);
        }

        public void AddTreasure(int value)
        {
            treasureCount += value;
            treasureEffect.Play();
            GameMenu.Instance.UpdateTreasureCounter();
        }

        public void RemoveTreasure(int value)
        {
            treasureCount -= value;
            GameMenu.Instance.UpdateTreasureCounter();
        }

        public void AddItemToInventory(ItemStats itemToAdd)
        {
            if (inventory == null) { inventory = new List<ItemStats>(); }
            inventory.Add(itemToAdd);
            GameMenu.Instance.UpdateInventoryUI();
            Debug.Log("Player added a " + itemToAdd.name + "to inventory!");
        }

        public void RemoveItemFromInventory(ItemStats itemToRemove)
        {
            
            inventory.Remove(itemToRemove);
            GameMenu.Instance.UpdateInventoryUI();
        }

        //remove all items from inventory
        public void ResetInventoryItems()
        {
            inventory.Clear();
            Debug.Log("Player:  Removed all items from inventory!");
            GameMenu.Instance.UpdateInventoryUI();  //update UI
            //refresh player current stats?
        }

        public override void CharacterSpawn()
        {
            ////check for spawn location
            if (spawnPoint == null)
            {
                SetSpawnLocation();
            }
            base.CharacterSpawn();
            treasureCount = DataManager.Instance.PlayerTreasureCount > 0 ? DataManager.Instance.PlayerTreasureCount : 0;
        }

        private void SetSpawnLocation()
        {
            spawnPoint = GameObject.FindObjectOfType<SpawnPoint_Player>();
            //mySheet.SetSpawnLocation(spawnPoint.transform);
        }

        public override void CharacterDeath()
        {
            deathCount++;
            Debug.Log(string.Format("CharacterDeath(), deathcount = ", deathCount.ToString()));
            Time.timeScale = 0.0f;  //stop time, for the drama, and to stop everything moving
            base.CharacterDeath();
            //trigger death on controller
            ResetInventoryItems();
            GameController.Instance.EndLevel(false);
        }

        public override void CharacterDeath(DeathType deathType)
        {
            deathCount++;
            Debug.Log(string.Format("CharacterDeath(), deathcount = ", deathCount.ToString()));
            Time.timeScale = 0.0f;  //stop time, for the drama, and to stop everything moving
            base.CharacterDeath(deathType);
            ResetInventoryItems();
            GameController.Instance.EndLevel(false);
        }

        public override void TakeDamage(int damageAmount, DamageType damageType)
        {
            base.TakeDamage(damageAmount, damageType);
            animator.SetTrigger("DamageNormal");
            GameMenu.Instance.ResetHealth();
        }

        public void TakeDamage(int amount, DamageType damageType, Vector2 attackVector)
        {
            throw new System.NotImplementedException();
        }
    }
}
