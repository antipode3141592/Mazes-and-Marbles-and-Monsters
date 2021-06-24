using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MarblesAndMonsters.Menus.Components
{
    public abstract class InventoryUIBase : MonoBehaviour
    {
        [SerializeField]
        private List<Image> contents;  //UI images of objects, defined in editor

        public virtual void UpdateUI(List<Sprite> itemIcons)
        {
            if (itemIcons == null)
            {
                Clear();
            }
            else
            {
                //display the sprites of all items in inventory
                for (int i = 0; i < contents.Count; i++)
                {
                    if (i < itemIcons.Count)
                    {
                        //update sprite
                        contents[i].sprite = itemIcons[i];
                        contents[i].color = Color.white;
                    }
                    else
                    {
                        //remove sprites from all other inventory images, set image transparent
                        contents[i].sprite = null;
                        contents[i].color = Color.clear;
                    }
                }
            }
        }

        //remove icons from all images and set transparent
        public void Clear()
        {
            foreach (var item in contents)
            {
                item.sprite = null;
                item.color = Color.clear;
            }
        }
    }
}
