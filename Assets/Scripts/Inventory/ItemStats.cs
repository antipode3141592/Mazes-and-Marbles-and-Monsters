using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters
{
    [CreateAssetMenu(menuName = "Stats/Item Stats/Basic Item Stats")]
    public class ItemStats : ScriptableObject
    {
        public Sprite InventoryIcon;
        public AudioClip ClipPickup;
    }
}