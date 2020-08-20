using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters;
using MarblesAndMonsters.Characters;

namespace MarblesAndMonsters.Items
{

    public enum KeyType { Red, Blue, Green, Purple, Skeleton }

    public class KeyItem : InventoryItem<KeyItem>
    {

        [SerializeField]
        private KeyType keyType;

        //private Player playerController;

        public KeyType KeyType => keyType;

        //void Awake()
        //{
        //    playerController = GameObject.FindObjectOfType<Player>(); //grab player controller
        //}

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other != null && other.gameObject.CompareTag("Player"))
            {
                //add key to player's inventory
                Player.Instance.AddItemToInventory(this);
                //disable the object
                this.gameObject.SetActive(false);
            }
        }

    }
}
