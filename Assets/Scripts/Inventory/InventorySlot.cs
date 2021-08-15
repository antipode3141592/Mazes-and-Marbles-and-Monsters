using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters
{
    public class InventorySlot
    {
        public ItemStats ItemStats;
        public int Quantity;
        public bool IsAvailable;
        public int QuickAccessSlot;  //0-index for quick access slots, negative indicates no assignment

        public string Id => ItemStats.Id;

        public InventorySlot(ItemStats itemStats, int quantity = 1, bool isAvailable = true, int quickAccessSlot = -1)
        {
            ItemStats = itemStats;
            Quantity = quantity;
            IsAvailable = isAvailable;
            QuickAccessSlot = quickAccessSlot;
        }
    }
}