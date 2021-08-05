using MarblesAndMonsters.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MarblesAndMonsters.Menus.Components
{
    

    public class QuickAccessController : MonoBehaviour
    {
        [SerializeField]
        protected List<QuickAccessSlot> quickSlot;

        public static int QuickSlotMax = 2; //start with 2 quickslots, some items may change this

        /// <summary>
        /// 
        /// </summary>
        /// <param name="slot">index of quickslot to be assigned</param>
        /// <param name="item">reference to item</param>
        /// <returns></returns>
        public void AssignQuickAccess(int slot, ItemStatsBase itemStats)
        {
            quickSlot[slot].AssignSlot(itemStats);
        }

        public void UnassignQuickAccess(int slot, ItemStatsBase itemStats)
        {
            quickSlot[slot].UnassignSlot();
        }

        public void ClearAll()
        {
            for (int i = 0; i < quickSlot.Count; i++)
            {
                quickSlot[i].UnassignSlot();
            }
        }

       
    }
}