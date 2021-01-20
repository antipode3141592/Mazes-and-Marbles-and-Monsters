using MarblesAndMonsters.Items;
using MarblesAndMonsters.Characters;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace MarblesAndMonsters.Menus.Components
{
    public class InventoryUI : MonoBehaviour
    { 
        [SerializeField]
        private List<Image> contents;  //UI images of objects

        public void UpdateUI()
        {
            //check for access to player's inventory
            if (Player.Instance != null && Player.Instance.Inventory != null)
            {
                //display the sprites of all items in inventory
                for (int i = 0; i < contents.Count; i++)
                {
                    if (i < Player.Instance.Inventory.Count)
                    {
                        //update sprite
                        contents[i].sprite = Player.Instance.Inventory[i].InventoryIcon;
                        contents[i].color = Color.white;
                    }
                    else
                    {
                        //remove sprites from all other inventory images, set image transparent
                        contents[i].sprite = null;
                        contents[i].color = Color.clear;
                    }
                }
            }else
            {
                InventoryImageClear();
            }
        }

        //remove icons from all images and set transparent
        private void InventoryImageClear()
        {
            foreach (var item in contents)
            {
                item.sprite = null;
                item.color = Color.clear;
            }
        }
    }
}
