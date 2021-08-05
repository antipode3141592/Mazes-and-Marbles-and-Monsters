using MarblesAndMonsters.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MarblesAndMonsters.Characters;

namespace MarblesAndMonsters.Menus.Components
{
    public class QuickAccessSlot : MonoBehaviour
    {
        public Button ItemImage;
        public Image BackgroundImage;
        public ItemAction StoredAction;

        protected void Awake()
        {
            ItemImage.image.color = Color.clear;
            BackgroundImage.color = Color.clear;
        }

        public void AssignSlot(ItemStatsBase itemStats)
        {
            ItemImage.image.sprite = itemStats.InventoryIcon;
            ItemImage.image.color = Color.white;
            ItemImage.onClick.AddListener(itemStats.Action);
            StoredAction = Player.Instance.MySheet.Actions.Find(x => x.ActionName == itemStats.AssociatedAction);
            if (StoredAction)
            {
                StoredAction.OnCooldownStart += CooldownStartHandler;
                StoredAction.OnCooldownEnd += CooldownEndHandler;
                StoredAction.CooldownTimer += CooldownHandler;
            }
            
        }

        public void UnassignSlot()
        {
            ItemImage.image.sprite = null;
            ItemImage.image.color = Color.clear;
            ItemImage.onClick.RemoveAllListeners();
            if (StoredAction)
            {
                StoredAction.OnCooldownStart -= CooldownStartHandler;
                StoredAction.OnCooldownEnd -= CooldownEndHandler;
                StoredAction.CooldownTimer -= CooldownHandler;
            }
        }

        public void CooldownStartHandler(object sender, EventArgs e)
        {
            BackgroundImage.color = Color.red;
            Debug.Log(string.Format("CooldownStartHandler is handling event from {0}", sender.ToString()));
        }

        public void CooldownEndHandler(object sender, EventArgs e)
        {
            BackgroundImage.color = Color.green;
            Debug.Log(string.Format("CooldownEndHandler is handling event from {0}", sender.ToString()));
        }

        public void CooldownHandler(object sender, UITimerEventArgs e)
        {
            Debug.Log(string.Format("CooldownHandler is handling event from {0} with a value {1:#.##}", sender.ToString(), e.PercentComplete));
        }
    }
}