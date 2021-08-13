using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManagement.Menus;
using MarblesAndMonsters.Characters;
using UnityEngine.UI;

namespace MarblesAndMonsters.Menus
{
    /// <summary>
    /// Backpack Menu has 
    /// </summary>
    public class BackpackMenu : Menu<BackpackMenu>
    {
        public Transform QuickAccessTransform;
        public Transform InventoryTransform;

        public GridLayoutGroup grid;
        //public UISpellIconTemplate ItemTemplate;

        private List<UISpellIconTemplate> spellBook;
        //private List<UISpellIconTemplate> quickAccess;

        protected override void Awake()
        {
            base.Awake();
            spellBook = new List<UISpellIconTemplate>(GetComponentsInChildren<UISpellIconTemplate>());
            //quickAccess = new List<UISpellIconTemplate>();
        }

        private void OnEnable()
        {
            Time.timeScale = 0.0f;
            Debug.Log("BackpackMenu OnEnable()...");
            if (Player.Instance != null && grid != null)
            {
                int i = 0;
                foreach (KeyValuePair<SpellName, Spell> _spell in Player.Instance.MySheet.Spells)
                {
                    Debug.Log(string.Format("spell name: {0}, spell id: {1}, isUnlocked = {2}", _spell.Key.ToString(), _spell.Value.SpellStats.Id, _spell.Value.IsUnlocked.ToString()));
                    //var itemIcon = Instantiate<UISpellIconTemplate>(ItemTemplate, InventoryTransform);
                    //spellBook.Add(itemIcon);
                    spellBook[i].Icon.sprite = _spell.Value.SpellStats.InventoryIcon;
                    spellBook[i].Icon.color = Color.white;
                    i++;
                }
            }
        }

        private void OnDisable()
        {
            Time.timeScale = 1.0f;
            foreach (UISpellIconTemplate spellIcon in spellBook)
            {
                spellIcon.Icon.color = Color.clear;
            }
        }

        //inventory management actions

        //assign quickslot

        //unassign quickslot

        //TODO:  swap inventory items, if inventory order matters
    }
}