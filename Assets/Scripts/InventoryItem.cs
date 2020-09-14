using UnityEngine;

namespace MarblesAndMonsters.Items
{
    public abstract class InventoryItem<T> : InventoryItem where T : InventoryItem<T>
    {
        public override void Reset()
        {
            gameObject.SetActive(true);
        }
    }

    public abstract class InventoryItem: MonoBehaviour
    {
        public abstract void Reset();
    }
}
