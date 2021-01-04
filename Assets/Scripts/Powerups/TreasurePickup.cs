using MarblesAndMonsters.Characters;
using UnityEngine;

namespace MarblesAndMonsters.Items
{
    public class TreasurePickup : InventoryItem
    {
        [SerializeField]
        private int value = 1;
        public override void Reset()
        {
            base.Reset();
            //Player.Instance.RemoveTreasure(value);
        }

        //private void OnCollisionEnter2D(Collision2D other)
        //{
        //    Player.Instance.AddTreasure(value);
        //    gameObject.SetActive(false);
        //}

        private void OnTriggerEnter2D(Collider2D other)
        {
            Player.Instance.AddTreasure(value);
            gameObject.SetActive(false);
        }
    }
}