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
        public InventoryItemUITemplate ItemTemplate;

        private List<InventoryItemUITemplate> inventoryItems;
        private List<InventoryItemUITemplate> quickAccess;

        protected override void Awake()
        {
            base.Awake();
            inventoryItems = new List<InventoryItemUITemplate>();
            quickAccess = new List<InventoryItemUITemplate>();
        }

        private void OnEnable()
        {
            Time.timeScale = 0.0f;
            //Debug.Log("BackpackMenu OnEnable()...");
            if (Player.Instance != null && grid != null)
            {
                inventoryItems.Clear();
                quickAccess.Clear();
                foreach (var slot in Player.Instance.Inventory)
                {
                    Debug.Log(string.Format("slot: {0}, item id: {1}", Player.Instance.Inventory.IndexOf(slot), slot.Id));
                    var itemIcon = Instantiate<InventoryItemUITemplate>(ItemTemplate, InventoryTransform);
                    inventoryItems.Add(itemIcon);
                    itemIcon.button.image.sprite = slot.ItemStats.InventoryIcon;
                    itemIcon.button.image.color = Color.white;
                    itemIcon.Quantity.text = slot.Quantity.ToString();
                    if (slot.QuickAccessSlot >= 0)
                    {
                        var itemIcon2 = Instantiate<InventoryItemUITemplate>(ItemTemplate, QuickAccessTransform);
                        quickAccess.Add(itemIcon2);
                        itemIcon2.button.image.sprite = slot.ItemStats.InventoryIcon;
                        itemIcon2.button.image.color = Color.white;
                        itemIcon2.Quantity.text = slot.Quantity.ToString();
                    }
                }
            }
        }

        private void OnDisable()
        {
            Time.timeScale = 1.0f;
            foreach (var item in inventoryItems)
            {
                Destroy(item.gameObject);
            }
            foreach (var item in quickAccess)
            {
                Destroy(item.gameObject);
            }
            inventoryItems.Clear();
            quickAccess.Clear();
        }

        //inventory management actions

        //assign quickslot

        //unassign quickslot

        //TODO:  swap inventory items, if inventory order matters
    }
}