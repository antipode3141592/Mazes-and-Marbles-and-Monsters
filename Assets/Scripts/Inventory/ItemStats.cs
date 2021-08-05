using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Events;

namespace MarblesAndMonsters
{
    [CreateAssetMenu(menuName = "Stats/Item Stats/Basic Item Stats")]
    public class ItemStats : ItemStatsBase
    {

    }

    public abstract class ItemStatsBase: ScriptableObject
    {
        [SerializeField]
        private string id;  //unique identifier
        public bool Stackable;
        public Sprite InventoryIcon;
        public AudioClip ClipPickup;
        public ActionName AssociatedAction;

        public float EffectDuration;
        public float CooldownDuration;

        public string Id => id;


        /// <summary>
        /// The game action performed by the item when used.  Can be used as callback for UI button presses
        /// </summary>
        public virtual void Action()
        {
            Debug.Log(string.Format("base Action() called for ItemStats with ID = {0}", Id));
        }
    }
}