using MarblesAndMonsters.Characters;
using UnityEngine;

namespace MarblesAndMonsters.Items
{
    public class TreasurePickup : InventoryItem<TreasurePickup>
    {
        [SerializeField]
        private int value = 1;
        public override void Reset()
        {
            base.Reset();
            //Player.Instance.RemoveTreasure(value);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Player.Instance.AddTreasure(value);
            gameObject.SetActive(false);
        }
    }
}