using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManagement.Menus;
using MarblesAndMonsters.Characters;
using UnityEngine.UI;
using UnityEngine.Sprites;
using MarblesAndMonsters.Menus.Components;
using System.Linq;

namespace MarblesAndMonsters.Menus
{
    /// <summary>
    /// Backpack Menu has 
    /// </summary>
    public class BackpackMenu : Menu<BackpackMenu>
    {
        public Transform QuickAccessTransform;
        public Transform InventoryTransform;
        public Text SpellDescription;
        public MagicStaffController MagicStaffController;
        public QuickAccessDelete QuickAccessDelete;

        public GridLayoutGroup grid;
        public GraphicRaycaster GraphicRaycaster;
        //public UISpellIconTemplate ItemTemplate;

        private List<UISpellIconTemplate> spellBook;
        private List<MagicStaffSlot> currentStaffSlots;
        private List<MagicStaffSlot> newStaffSlots;

        protected override void Awake()
        {
            base.Awake();
            spellBook = new List<UISpellIconTemplate>(GetComponentsInChildren<UISpellIconTemplate>());
            //QuickAccessDelete = FindObjectOfType<QuickAccessDelete>();
            GraphicRaycaster = GetComponent<GraphicRaycaster>();
            //MagicStaffController = FindObjectOfType<MagicStaffController>();
            //currentStaffSlots = new List<MagicStaffSlot>();
            //newStaffSlots = new List<MagicStaffSlot>();
        }

        private void OnEnable()
        {
            Time.timeScale = 0.0f;
            QuickAccessDelete.Icon.color = Color.clear;
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

        private void ClearSlot(int i)
        {
            spellBook[i].Icon.sprite = null;
            spellBook[i].Icon.color = Color.grey;
            spellBook[i].SpellStats = null;
            spellBook[i].IsUnlocked = false;
        }

        private void OnDisable()
        {
            Time.timeScale = 1.0f;

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