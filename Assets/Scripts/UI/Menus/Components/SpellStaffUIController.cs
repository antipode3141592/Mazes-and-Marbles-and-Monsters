using LevelManagement.DataPersistence;
using MarblesAndMonsters.Characters;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.Menus.Components
{


    public class SpellStaffUIController : MonoBehaviour
    {
        [SerializeField]
        protected List<QuickAccessSlot> quickSlot;

        public static int QuickSlotMax = 2;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="slot">index of quickslot to be assigned</param>
        /// <param name="item">reference to item</param>
        /// <returns></returns>
        public void AssignSpellSlot(int slot, SpellStatsBase spellStats)
        {
            if (!quickSlot[slot].isActiveAndEnabled)
            {
                quickSlot[slot].gameObject.SetActive(true);
            }
            quickSlot[slot].AssignSlot(spellStats);
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

        protected void OnDisable()
        {
            ClearAll();
        }

        public void AssignAllSpellSlots()
        {
            if (Player.Instance != null)
            {
                foreach (var spelldata in Player.Instance.MySheet.Spells)
                {
                    if (spelldata.Value.IsQuickSlotAssigned)
                    {
                        AssignSpellSlot(spelldata.Value.QuickSlot, spelldata.Value.SpellStats);
                    }
                }
            }
        }
    }
}