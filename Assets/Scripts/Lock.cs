using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters;

namespace MarblesAndMonsters
{
    public class Lock : MonoBehaviour
    {
        //type of key required to 
        [SerializeField]
        private KeyType requiredKeyType;
        [SerializeField]
        private GameObject lockedItem;

        private bool _locked = true;
        private Player playerController;

        public bool Locked => _locked;
        public KeyType RequiredKeyType => requiredKeyType;

        void Awake()
        {
            playerController = GameObject.FindObjectOfType<Player>();
        }

        public bool IsLocked()
        {
            if (_locked) { return true; } else { return false; }
        }

        //public void UnLock(KeyItem keyItem)
        //{
        //    //check if the keytype matches the requirement
        //    if (keyItem.KeyType == requiredKeyType) 
        //    {
        //        //open the item!
        //    }
        //}

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other != null && other.gameObject.CompareTag("Player"))
            {
                //grab keys from inventory and test for match (or skeleton)
                var inventory = playerController.Inventory;
                foreach (KeyItem item in inventory)
                {
                    if (item.KeyType == requiredKeyType || item.KeyType == KeyType.Skeleton)
                    {
                        //unlock!
                        Debug.Log("the right key!  the lock clicks open!");
                        _locked = false;

                        //unlocking animation for locked item

                        //remove key from inventory
                        playerController.RemoveItemFromInventory(item);

                        //for now, disable the parent object representing the lock
                        gameObject.SetActive(false);
                        //lockedItem.SetActive(false);
                    }
                }
            }
        }
    }
}