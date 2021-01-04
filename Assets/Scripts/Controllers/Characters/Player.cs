using Cinemachine;
using LevelManagement.Data;
using MarblesAndMonsters.Menus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        protected override void OnEnable()
        {
            base.OnEnable();
            GameMenu.Instance.RefreshUI();
        }

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
            mySheet.CurrentHealth += amount;
            //trigger animation
            //trigger particle effects
            if (amount > 0)
            {
                addMaxHealthEffect.Play();
            }
            //
            GameMenu.Instance.healthBarController.UpdateHealth();
        }

        public void AddTreasure(int value)
        {
            treasureCount += value;
            treasureEffect.Play();
            GameMenu.Instance.treasureUI.UpdateTreasureCount();
        }

        public void RemoveTreasure(int value)
        {
            treasureCount -= value;
            GameMenu.Instance.treasureUI.UpdateTreasureCount();
        }

        public void AddItemToInventory(ItemStats itemToAdd)
        {
            if (inventory == null) { inventory = new List<ItemStats>(); }
            inventory.Add(itemToAdd);
            GameMenu.Instance.inventoryUI.UpdateUI();
            Debug.Log("Player added a " + itemToAdd.name + "to inventory!");
        }

        public void RemoveItemFromInventory(ItemStats itemToRemove)
        {
            
            inventory.Remove(itemToRemove);
            GameMenu.Instance.inventoryUI.UpdateUI();
        }

        //remove all items from inventory
        public void ResetInventoryItems()
        {
            inventory.Clear();
            Debug.Log("Player:  Removed all items from inventory!");
            //GameMenu.Instance.inventoryUI.UpdateUI();
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

        //there's probably a better way to abstract the CharacterDeath to not copy/paste the base function
        public override void CharacterDeath(DeathType deathType)
        {
            if (isDying) { return; }
            else
            {
                //Time.timeScale = 0.0f;  //stop time, for the drama, and to stop everything moving
                isDying = true;
                myRigidbody.velocity = Vector2.zero;
                myRigidbody.Sleep();
                deathCount++;
                
                //Debug.Log(string.Format("CharacterDeath(DeathType deathType):  {0} has died by {1}", gameObject.name, deathType.ToString()));
                switch (deathType)
                {
                    case DeathType.Falling:
                        //animator.SetBool("Falling", true);
                        animator.SetTrigger("Falling");
                        break;
                    case DeathType.Damage:
                        animator.SetTrigger("DeathbyDamage");
                        break;
                    case DeathType.Fire:
                        break;
                    case DeathType.Poison:
                        break;
                    default:
                        Debug.LogError("Unhandled deathtype enum!");
                        break;

                }
                StartCoroutine(DeathAnimation(deathType));
            }
        }

        protected override IEnumerator DeathAnimation(DeathType deathType)
        {
            yield return new WaitForSeconds(0.667f);  //death animations are 8 frames, current fps is 12
            ResetInventoryItems();
            GameController.Instance.EndLevel(false);
            gameObject.SetActive(false);
        }

        public override void TakeDamage(int damageAmount, DamageType damageType)
        {
            base.TakeDamage(damageAmount, damageType);
            animator.SetTrigger("DamageNormal");
            GameMenu.Instance.healthBarController.UpdateHealth();
        }

        public void TakeDamage(int amount, DamageType damageType, Vector2 attackVector)
        {
            throw new System.NotImplementedException();
        }

        //collision stuff

        private void OnCollisionEnter2D(Collision2D collision)
        {
            audioSource.Play();
        }
    }
}
