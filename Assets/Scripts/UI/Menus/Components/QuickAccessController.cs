using LevelManagement.DataPersistence;
using MarblesAndMonsters.Characters;
using System.Collections.Generic;
using UnityEngine;

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
        public void AssignQuickAccess(int slot, SpellStats spellStats)
        {
            quickSlot[slot].AssignSlot(spellStats);
        }

        public void UnassignQuickAccess(int slot)
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

        protected void OnEnable()
        {
            AssignAllSpellSlots();
        }

        public void AssignAllSpellSlots()
        {
            //check quickslots
            if (Player.Instance != null)
            {
                ClearAll();
                foreach (var spelldata in Player.Instance.MySheet.Spells)
                {
                    if (spelldata.Value.IsQuickSlotAssigned)
                    {
                        AssignQuickAccess(spelldata.Value.QuickSlot, spelldata.Value.SpellStats);
                    }
                }
            }
        }
    }
}