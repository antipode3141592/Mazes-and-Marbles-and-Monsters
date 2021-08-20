using MarblesAndMonsters.Characters;
using MarblesAndMonsters.Menus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackpackController : MonoBehaviour
{
    public Transform InventoryTransform;

    public GridLayoutGroup grid;
    public UISpellIconTemplate ItemTemplate;

    private List<UISpellIconTemplate> inventoryItems;
    //public Canvas ParentCanvas;

    protected void Awake()
    {
        inventoryItems = new List<UISpellIconTemplate>();
    }

    private void OnEnable()
    {
        //Time.timeScale = 0.0f;
        //Debug.Log("BackpackMenu OnEnable()...");
        if (Player.Instance != null && grid != null)
        {
            inventoryItems.Clear();
            foreach (var slot in Player.Instance.Inventory)
            {
                Debug.Log(string.Format("slot: {0}, item id: {1}", Player.Instance.Inventory.IndexOf(slot), slot.Id));
                var itemIcon = Instantiate<UISpellIconTemplate>(ItemTemplate);
                itemIcon.transform.SetParent(InventoryTransform,false);
                //itemIcon.parentCanvas = ParentCanvas;
                inventoryItems.Add(itemIcon);
                itemIcon.Icon.sprite = slot.ItemStats.InventoryIcon;
                itemIcon.Icon.color = Color.white;
            }
        }
    }

    private void OnDisable()
    {
        Debug.Log("BackpackController.OnDisable() called...");
        //Time.timeScale = 1.0f;
        foreach (var item in inventoryItems)
        {
            Destroy(item.gameObject);
        }
        inventoryItems.Clear();
    }

    public void OnExitBackpack()
    {
        Debug.Log("OnExitBackpack() called...");
        gameObject.SetActive(false);
    }

    //inventory management actions

    //assign quickslot

    //unassign quickslot

    //TODO:  swap inventory items, if inventory order matters

    
}
