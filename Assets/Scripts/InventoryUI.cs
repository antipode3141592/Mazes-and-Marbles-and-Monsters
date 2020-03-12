using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MarblesAndMonsters;


namespace MarblesAndMonsters {
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField]
        private int capacity;   //number of inventory items that can be carried
        [SerializeField]
        private List<GameObject> contents;


        private List<InventoryItem> inventoryItems; //list of 


        public int Capacity => capacity;
        public List<InventoryItem> InventoryItems => inventoryItems;

        
        void Awake()
        {
            inventoryItems = new List<InventoryItem>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddInventoryItem(InventoryItem item)
        {
            inventoryItems.Add(item);
            UpdateUI();
        }

        public void RemoveInventoryItem(InventoryItem item)
        {
            inventoryItems.Remove(item);
            UpdateUI();
        }

        public void UpdateUI()
        {
            //display the sprites of all items in inventory
            for (int i = 0; i < capacity; i++)
            {
                if (i < inventoryItems.Count)
                {
                    Debug.Log("UpdateUI()  inventory item: " + inventoryItems[i].name);
                    //show image
                    if (!contents[i].activeInHierarchy) { contents[i].SetActive(true); }
                    contents[i].GetComponent<Image>().sprite = inventoryItems[i].gameObject.GetComponent<SpriteRenderer>().sprite;
                } 
                else
                {
                    contents[i].SetActive(false);
                }
            }
        }
    }
}
