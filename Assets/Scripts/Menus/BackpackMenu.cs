using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManagement.Menus;
using MarblesAndMonsters.Characters;
using UnityEngine.UI;
using UnityEngine.Sprites;

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
                for (int i = 0; i < spellBook.Count; i++)
                {
                    //Spell _spell = Player.Instance.MySheet.Spells[(SpellName)i];
                    Spell _spell;
                    if (Player.Instance.MySheet.Spells.TryGetValue((SpellName)i, out _spell))
                    {
                        if (_spell.IsUnlocked)
                        {
                            spellBook[i].SpellStats = _spell.SpellStats;  //grab relevant stats
                            spellBook[i].Icon.sprite = _spell.SpellStats.InventoryIcon;
                            spellBook[i].Icon.color = Color.white;
                        }
                    } else
                    {
                        spellBook[i].Icon.sprite = null;
                        spellBook[i].Icon.color = Color.grey;
                        spellBook[i].SpellStats = null;
                    }   
                }
            }
        }

        private void OnDisable()
        {
            Time.timeScale = 1.0f;
        }

        //inventory management actions

        //assign quickslot

        //unassign quickslot

        //TODO:  swap inventory items, if inventory order matters
    }
}