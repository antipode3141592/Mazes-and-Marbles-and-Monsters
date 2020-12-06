using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Characters;

namespace MarblesAndMonsters.Items
{

    //Add Health Potion - when consumed, +HealthStrength to max HP and heal to full
    public class AddHealthPotion : InventoryItem
    {
        [SerializeField]
        private AddHealthItemStats healthItemStats;


        //potion is consumed immediately (it would feel weird to have to activate it 
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