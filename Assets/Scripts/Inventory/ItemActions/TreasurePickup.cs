using MarblesAndMonsters.Characters;
using UnityEngine;

namespace MarblesAndMonsters.Items
{
    public class TreasurePickup : InventoryItem
    {
        [SerializeField] int value = 1;
        public override void Reset()
        {
            base.Reset();
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Player.Instance.AddTreasure(value);
                animator.SetTrigger(aTriggerPickup);
                StartCoroutine(PickupItem(pickupDelay));
            }
            
        }
    }
}