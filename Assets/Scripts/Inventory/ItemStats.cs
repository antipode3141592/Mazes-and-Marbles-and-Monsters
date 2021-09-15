using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.Items
{
    [CreateAssetMenu(menuName = "Stats/Item Stats/Base Item Stats")]
    public class ItemStats : ItemStatsBase
    {

    }

    public abstract class ItemStatsBase : ScriptableObject
    {
        [SerializeField]
        private string id;  //unique identifier
        public Sprite InventoryIcon;
        public AudioClip ClipPickup;
        public string Description;
        public string PublicName;

        public string Id => id;
    }
}