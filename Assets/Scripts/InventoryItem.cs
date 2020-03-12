using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters;

namespace MarblesAndMonsters
{
    public abstract class InventoryItem<T> : InventoryItem where T : InventoryItem<T>
    {
        
    }

    public abstract class InventoryItem: MonoBehaviour
    {

    }
}
