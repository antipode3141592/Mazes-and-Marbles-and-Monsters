using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Characters;

namespace MarblesAndMonsters.Items
{
    public class AddHealthPotion : InventoryItem
    {
        [SerializeField]
        private AddHealthItemStats healthItemStats;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Player.Instance.AddMaxHealth(healthItemStats.HealingStrength);
                //Destroy(gameObject);    //destroy self (these are relatively rare, so no need for pooling)
                gameObject.SetActive(false);
            }
        }

        public override void Reset()
        {
            if (!gameObject.activeInHierarchy)
            {
                base.Reset();
                Player.Instance.AddMaxHealth(-healthItemStats.HealingStrength);
            }
        }
    }
}