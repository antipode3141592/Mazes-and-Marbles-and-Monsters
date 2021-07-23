using MarblesAndMonsters.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MarblesAndMonsters.Menus.Components
{
    

    public class QuickAccessController : MonoBehaviour
    {
        [SerializeField]
        protected List<Button> contents;
        [SerializeField]
        protected List<Text> contentTexts;

        public static int QuickSlotMax = 2; //start with 2 quickslots, some items may change this

        /// <summary>
        /// 
        /// </summary>
        /// <param name="slot">index of quickslot to be assigned</param>
        /// <param name="item">reference to item</param>
        /// <returns></returns>
        public bool AssignQuickAccess(int slot, ItemStatsBase itemStats)
        {
            try
            {
                //verify slot is valid
                if (slot < 0 || slot >= 2)
                {
                    throw new Exception(string.Format("The slot value {0} is not valid", slot));
                }
                //set sprite
                contents[slot].image.sprite = itemStats.InventoryIcon;
                contents[slot].image.color = Color.white;
                contents[slot].onClick.AddListener(itemStats.Action);
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
            return false;
        }

        public bool UnassignQuickAccess(int slot)
        {
            try
            {
                if (slot < 0 || slot >= 2)
                {
                    throw new Exception(string.Format("The slot value {0} is not valid", slot));
                }
                contents[slot].image.sprite = null;
                contents[slot].image.color = Color.clear;
                contentTexts[slot].text = "";
                contents[slot].onClick.RemoveAllListeners();
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
            return false;
        }

        public void UpdateQuantity(int slot, int quantity)
        {
            //if (quantity > 1)
            //{
                contentTexts[slot].text = quantity.ToString();
            //}
        }

        public void ClearAll()
        {
            for (int i = 0; i < contents.Count; i++)
            {
                contents[i].image.sprite = null;
                contents[i].image.color = Color.clear;
                contentTexts[i].text = "";
                contents[i].onClick.RemoveAllListeners();
            }

        }
    }
}