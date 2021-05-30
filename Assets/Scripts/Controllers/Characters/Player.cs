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
    public class Player : CharacterControl
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

        [SerializeField]
        private LightingSettings lightingSettings;
        private GlobalLight globalLight;
        private PlayerTorch playerTorch;

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
            globalLight = FindObjectOfType<GlobalLight>();
            playerTorch = FindObjectOfType<PlayerTorch>();
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
                mySheet.MaxHealth = DataManager.Instance.PlayerMaxHealth > 3 ? DataManager.Instance.PlayerMaxHealth : mySheet.baseStats.MaxHealth;
                mySheet.CurrentHealth = DataManager.Instance.PlayerCurrentHealth > 0 ? DataManager.Instance.PlayerCurrentHealth : mySheet.MaxHealth;
            } else
            {
                deathCount = 0;
                treasureCount = 0;
                mySheet.MaxHealth = 3;
            }
            AdjustLight();
            base.Start();
        }

        private void AdjustLight()
        {
            if (globalLight.Intensity < 1.0) 
            {
                playerTorch.gameObject.SetActive(true);
                playerTorch.AdjustLight(lightingSettings.PlayerLightOnIntensity, lightingSettings.PlayerLightOnColor);
            } else
            {
                playerTorch.gameObject.SetActive(false);
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if (GameMenu.Instance != null)
            {
                GameMenu.Instance.RefreshUI();
            }
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
            //treasureEffect.Play();
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
            foreach(var item in inventory)
            {
                //
            }
            inventory.Clear();
            Debug.Log("Player:  Removed all items from inventory!");
            //GameMenu.Instance.inventoryUI.UpdateUI();
            //refresh player current stats?
        }

        //public override void CharacterSpawn()
        //{
        //    ////check for spawn location
        //    if (spawnPoint == null)
        //    {
        //        SetSpawnLocation();
        //    }
        //    base.CharacterSpawn();
        //    treasureCount = DataManager.Instance.PlayerTreasureCount > 0 ? DataManager.Instance.PlayerTreasureCount : 0;
        //}

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
                        animator.SetTrigger(aTriggerFalling);
                        audioSource.clip = MySheet.baseStats.ClipDeathFall;
                        audioSource.Play();
                        break;
                    case DeathType.Damage:
                        animator.SetTrigger(aTriggerDeathByDamage);
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
            
            ResetInventoryItems();
            float animationLength = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;

            Debug.Log(string.Format("DeathAnimation {0} has died of {1}!  the animation takes {2} sec", gameObject.name, deathType.ToString(), animationLength));
            yield return new WaitForSeconds(animationLength);  //death animations are 8 frames, current fps is 12
            //GameController.Instance.EndLevel(false);
            GameManager.Instance.LevelLose();
            gameObject.SetActive(false);
        }

        public override void TakeDamage(int damageAmount, DamageType damageType)
        {
            base.TakeDamage(damageAmount, damageType);
            animator.SetTrigger(aTriggerDamageNormal);
            GameMenu.Instance.healthBarController.UpdateHealth();
        }

        public void TakeDamage(int amount, DamageType damageType, Vector2 attackVector)
        {
            throw new System.NotImplementedException();
        }

        //collision stuff

        private void OnCollisionEnter2D(Collision2D collision)
        {
            audioSource.clip = MySheet.baseStats.ClipHit;
            audioSource.Play();
        }

        protected override void SetLookDirection()
        {
            Vector2 direction = myRigidbody.velocity;
            if (!Mathf.Approximately(direction.x, 0.0f) || !Mathf.Approximately(direction.y, 0.0f))
            {
                lookDirection = direction;
                lookDirection.Normalize();
            }

            animator.SetFloat(aFloatLookX, lookDirection.x);
            animator.SetFloat(aFloatLookY, lookDirection.y);
        }
    }
}
