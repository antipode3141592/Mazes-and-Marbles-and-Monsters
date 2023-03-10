using MarblesAndMonsters.Characters;
using System.Collections;
using UnityEngine;

namespace MarblesAndMonsters.Items
{
    //Add Health Potion - immediately consumed, add to max health and heal all damage
    public class AddHealthPotion : InventoryItem
    {
        [SerializeField] AddHealthItemStats healthItemStats;

        //potion is consumed immediately (it would feel weird to have to activate it) 
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Player.Instance.AddMaxHealth(healthItemStats.HealingStrength);
                StartCoroutine(PickupItem(pickupDelay));
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