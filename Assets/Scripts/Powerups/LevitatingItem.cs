using MarblesAndMonsters.Characters;
using System.Collections;
using UnityEngine;

namespace MarblesAndMonsters.Items
{
    /// <summary>
    /// Floating Item is a non-consummable item with the power to levitate
    ///     (levitate:  for the duration of the effect, character cannot fall into pits and is unaffected by Board Move)
    /// </summary>
    public class LevitatingItem : InventoryItem
    {
        //private void OnTriggerEnter2D(Collider2D collision)
        //{
        //    //only Player objects can pickup and use 
        //    if (collision.gameObject.CompareTag("Player"))
        //    {
        //        if (Player.Instance != null)
        //        {
        //            Player.Instance.AddItemToInventory(this.ItemStats);
        //            StartCoroutine(PickupItem());
        //        }
        //    }
        //}

        //private IEnumerator PickupItem()
        //{
        //    yield return new WaitForSeconds(0.1f);
        //    gameObject.SetActive(false);
        //}
    }
}