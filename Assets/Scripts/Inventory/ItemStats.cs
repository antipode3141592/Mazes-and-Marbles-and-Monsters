using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters
{
    [CreateAssetMenu(menuName = "Stats/Item Stats/Basic Item Stats")]
    public class ItemStats : ItemStatsBase
    {

    }

    public abstract class ItemStatsBase: ScriptableObject
    {
        public Sprite InventoryIcon;
        public AudioClip ClipPickup;
    }
}