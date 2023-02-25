using System.Collections.Generic;
using UnityEngine;
using LevelManagement.Menus;
using MarblesAndMonsters.Characters;
using UnityEngine.UI;
using MarblesAndMonsters.Menus.Components;
using LevelManagement.DataPersistence;
using Chronos;

namespace MarblesAndMonsters.Menus
{
    public class BackpackMenu : Menu<BackpackMenu>
    {
        Clock _rootClock;

        public Transform QuickAccessTransform;
        public Transform InventoryTransform;
        public Text SpellDescription;
        public MagicStaffController MagicStaffController;

        public GridLayoutGroup grid;
        public GraphicRaycaster GraphicRaycaster;

        List<UISpellIconTemplate> spellBook;
        List<MagicStaffSlot> currentStaffSlots;
        List<MagicStaffSlot> newStaffSlots;

        public Clock RootClock
        {
            get
            {
                if (_rootClock is null)
                    _rootClock = Timekeeper.instance.Clock("Root");
                return _rootClock;
            }
        }

        protected void Awake()
        {
            spellBook = new List<UISpellIconTemplate>(GetComponentsInChildren<UISpellIconTemplate>());
            GraphicRaycaster = GetComponent<GraphicRaycaster>();
        }

        void OnEnable()
        {
            if (Player.Instance != null)
            {
                for (int i = 0; i < spellBook.Count; i++)
                {
                    Spell _spell;
                    if (Player.Instance.MySheet.Spells.TryGetValue((SpellName)i, out _spell))
                    {
                        if (_spell.IsUnlocked)
                        {
                            spellBook[i].SpellStats = _spell.SpellStats;  //grab relevant stats
                            spellBook[i].Icon.sprite = _spell.SpellStats.InventoryIcon;
                            spellBook[i].Icon.color = Color.white;
                            spellBook[i].IsUnlocked = true;
                            if (_spell.IsQuickSlotAssigned)
                            {
                                Debug.Log(string.Format("The spell {0} is assigned to quickslot {1}", _spell.SpellName.ToString(), _spell.QuickSlot));
                                MagicStaffController.AssignQuickAccess(_spell.QuickSlot, _spell.SpellStats);
                            }
                        } else
                        {
                            ClearSlot(i);
                        }
                    } else
                    {
                        ClearSlot(i);
                    }   
                }
            }
        }

        void ClearSlot(int i)
        {
            spellBook[i].Icon.sprite = null;
            spellBook[i].Icon.color = Color.grey;
            spellBook[i].SpellStats = null;
            spellBook[i].IsUnlocked = false;
        }

        void OnDisable()
        {
            //List<SpellName> quickslotSpells = new List<SpellName>();
            // clear quickslots of player's spells
            if (Player.Instance)
            {
                foreach (var spell in Player.Instance.MySheet.Spells)
                {
                    spell.Value.QuickSlot = -1;
                    spell.Value.IsQuickSlotAssigned = false;
                }
                // commit magic staff assignments to the Player's Sheet
                for (int i = 0; i < MagicStaffController.spellSlots.Count; i++)
                {
                    if (MagicStaffController.spellSlots[i].enabled && MagicStaffController.spellSlots[i].SpellStats != null)
                    {
                        SpellName spellName = MagicStaffController.spellSlots[i].SpellStats.SpellName;
                        //quickslotSpells.Add(MagicStaffController.spellSlots[i].SpellStats.SpellName);
                        Player.Instance.MySheet.Spells[spellName].QuickSlot = MagicStaffController.spellSlots[i].QuickSlot;
                        Player.Instance.MySheet.Spells[spellName].IsQuickSlotAssigned = true;
                    }
                }
            }
        }

        //inventory management actions

        //assign quickslot

        //unassign quickslot

        //TODO:  swap inventory items, if inventory order matters
    }
}