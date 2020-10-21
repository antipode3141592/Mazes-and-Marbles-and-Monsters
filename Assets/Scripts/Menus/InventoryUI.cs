using MarblesAndMonsters.Items;
using MarblesAndMonsters.Characters;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace LevelManagement.Menus.Components
{
    public class InventoryUI : MonoBehaviour
    { 
        [SerializeField]
        private List<Image> contents;  //UI images of objects

        public void UpdateUI()
        {
            if (Player.Instance.Inventory != null)
            {
                //display the sprites of all items in inventory
                for (int i = 0; i < contents.Count; i++)
                {
                    if (i < Player.Instance.Inventory.Count)
                    {
                        //update sprite
                        contents[i].overrideSprite = Player.Instance.Inventory[i].GetUISprite();
                    }
                    else
                    {
                        //remove sprites from all other inventory images
                        contents[i].overrideSprite = null;
                    }
                }
            }else
            {
                foreach (var item in contents)
                {
                    item.overrideSprite = null;
                }
            }
        }
    }
}
