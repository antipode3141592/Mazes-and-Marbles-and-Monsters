using MarblesAndMonsters.Characters;
using System.Collections;
using UnityEngine;

namespace MarblesAndMonsters.Items
{
    public class HealingPotion : InventoryItem
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //only Player objects can pickup and use 
            if (collision.gameObject.CompareTag("Player"))
            {
                if (Player.Instance != null)
                {
                    Player.Instance.AddItemToInventory(this.ItemStats);
                    StartCoroutine(PickupItem());
                }
            }
        }

        private IEnumerator PickupItem()
        {
            yield return new WaitForSeconds(0.1f);
            gameObject.SetActive(false);
        }
    }
}