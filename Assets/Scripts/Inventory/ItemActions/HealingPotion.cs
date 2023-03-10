using MarblesAndMonsters.Characters;
using UnityEngine;

namespace MarblesAndMonsters.Items
{
    public class HealingPotion : InventoryItem
    {
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Player.Instance.HealDamage(-1); //heal all damage
                StartCoroutine(PickupItem(pickupDelay));
            }
        }
    }
}