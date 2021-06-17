using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters;
using MarblesAndMonsters.Characters;
using MarblesAndMonsters.Items;

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

        public bool Locked => _locked;
        public KeyType RequiredKeyType => requiredKeyType;

        public bool IsLocked()
        {
            if (_locked) { return true; } else { return false; }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other != null && other.gameObject.CompareTag("Player"))
            {
                //grab keys from inventory and test for match (or skeleton)
                foreach (KeyItem item in Player.Instance.KeyChain)
                {
                    if (item.KeyStats.KeyType == requiredKeyType || item.KeyStats.KeyType == KeyType.Skeleton)
                    {
                        //unlock!
                        Debug.Log("the right key!  the lock clicks open!");
                        _locked = false;

                        //unlocking animation for locked item
                        StartCoroutine(Unlock(item));
                        //remove key from inventory
                        //playerController.RemoveItemFromInventory(item);

                        ////for now, disable the parent object representing the lock
                        //gameObject.SetActive(false);
                        //lockedItem.SetActive(false);
                        break;  //we broke the enumeration by removing it, so stop the loop
                    }
                }
            }
        }

        private IEnumerator Unlock(KeyItem keyItem)
        {
            yield return new WaitForSeconds(0.2f);

            Player.Instance.RemoveKeyFromKeyChain(keyItem);

            //for now, disable the parent object representing the lock
            gameObject.SetActive(false);
        }
    }
}