using LevelManagement.DataPersistence;
using MarblesAndMonsters.Characters;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.Menus.Components
{
    public class MagicStaffController : MonoBehaviour
    {
        [SerializeField]
        public List<MagicStaffSlot> spellSlots;

        private void OnEnable()
        {
            int staffLevel = Player.Instance != null ? Player.Instance.StaffLevel : 2;
            for (int i = 0; i < spellSlots.Count; i++)
            {
                if (i < staffLevel)
                {
                    spellSlots[i].IsUnlocked = true;
                } else
                {
                    spellSlots[i].IsUnlocked = false;
                    spellSlots[i].BackgroundImage.color = Color.gray;
                }
            }
        }


        public void AssignQuickAccess(int slot, SpellStats spellStats)
        {
            spellSlots[slot]._AssignSlot(spellStats);
        }

        public void UnassignQuickAccess(int slot)
        {
            spellSlots[slot].UnassignSlot();
        }

        public void ClearAll()
        {
            for (int i = 0; i < spellSlots.Count; i++)
            {
                spellSlots[i].UnassignSlot();
            }
        }
    }
}