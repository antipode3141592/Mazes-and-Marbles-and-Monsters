using MarblesAndMonsters.Events;
using System;
using UnityEngine;
using UnityEngine.UI;
using MarblesAndMonsters.Characters;
using LevelManagement.DataPersistence;

namespace MarblesAndMonsters.Menus.Components
{
    public class QuickAccessSlot : MonoBehaviour
    {
        public Button ItemImage;
        public Image BackgroundImage;
        public Image SlotImage;
        public Image CooldownGuage;
        public Image DurationGuage;
        public SpellName storedSpellName;

        protected void Awake()
        {
            ItemImage.image.color = Color.clear;
            BackgroundImage.color = Color.clear;
        }

        public void AssignSlot(SpellStatsBase spellStats)
        {
            if (Debug.isDebugBuild)
                Debug.Log($"AssignSlot called for {spellStats.PublicName}", this);
            ItemImage.image.sprite = spellStats.InventoryIcon;
            ItemImage.image.color = Color.white;
            BackgroundImage.color = Color.green;
            SlotImage.color = Color.white;
            ItemImage.onClick.AddListener(Player.Instance.MySheet.Spells[spellStats.SpellName].Cast);
            storedSpellName = spellStats.SpellName;
            if (Player.Instance != null)
            {
                Player.Instance.MySheet.Spells[storedSpellName].OnCooldownStart += CooldownStartHandler;
                Player.Instance.MySheet.Spells[storedSpellName].OnCooldownEnd += CooldownEndHandler;
                Player.Instance.MySheet.Spells[storedSpellName].CooldownTimer += CooldownHandler;
                Player.Instance.MySheet.Spells[storedSpellName].DurationTimer += DurationHandler;
                Player.Instance.MySheet.Spells[storedSpellName].OnDurationStart += DurationStartHandler;
                Player.Instance.MySheet.Spells[storedSpellName].OnDurationEnd += DurationEndHandler;
            } else
            {
                Debug.LogWarning("No Player instance found when attempting to assign slot");
            }
        }

        public void UnassignSlot()
        {
            ItemImage.image.sprite = null;
            ItemImage.image.color = Color.clear;
            BackgroundImage.color = Color.clear;
            SlotImage.color = Color.clear;
            //CooldownGuage.color = Color.clear;
            //DurationGuage.color = Color.clear;
            ItemImage.onClick.RemoveAllListeners();
            if (Player.Instance != null)
            {
                Player.Instance.MySheet.Spells[storedSpellName].OnCooldownStart -= CooldownStartHandler;
                Player.Instance.MySheet.Spells[storedSpellName].OnCooldownEnd -= CooldownEndHandler;
                Player.Instance.MySheet.Spells[storedSpellName].CooldownTimer -= CooldownHandler;
                Player.Instance.MySheet.Spells[storedSpellName].DurationTimer -= DurationHandler;
                Player.Instance.MySheet.Spells[storedSpellName].OnDurationStart -= DurationStartHandler;
                Player.Instance.MySheet.Spells[storedSpellName].OnDurationEnd -= DurationEndHandler;
            }
            else
            {
                Debug.LogWarning("No Player instance found when unassigning slot");
            }
        }

        public void CooldownStartHandler(object sender, EventArgs e)
        {
            //CooldownGuage.color = Color.red;
            if (Debug.isDebugBuild)
                Debug.Log(string.Format("CooldownStartHandler is handling event from {0}", sender.ToString()));
        }

        public void CooldownEndHandler(object sender, EventArgs e)
        {
            //CooldownGuage.color = Color.green;
            if (Debug.isDebugBuild)
                Debug.Log(string.Format("CooldownEndHandler is handling event from {0}", sender.ToString()));
            CooldownGuage.rectTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        public void CooldownHandler(object sender, UITimerEventArgs e)
        {
            //if (Debug.isDebugBuild)
            //    Debug.Log($"CooldownHandler is handling event from {sender} with a value {e.PercentComplete:#.##}", this);
            CooldownGuage.rectTransform.localScale = new Vector3(1.0f, 1.0f-e.PercentComplete, 1.0f);

        }

        public void DurationStartHandler(object sender, EventArgs e)
        {
            //DurationGuage.color = Color.red;
            if (Debug.isDebugBuild)
                Debug.Log(string.Format("DurationStartHandler is handling event from {0}", sender.ToString()));
        }

        public void DurationEndHandler(object sender, EventArgs e)
        {
            //DurationGuage.color = Color.green;
            if (Debug.isDebugBuild)
                Debug.Log(string.Format("DurationEndHandler is handling event from {0}", sender.ToString()));
            DurationGuage.rectTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        public void DurationHandler(object sender, UITimerEventArgs e)
        {
            //if (Debug.isDebugBuild)
            //    Debug.Log($"CooldownHandler is handling event from {sender} with a value {e.PercentComplete:#.##}", this);
            DurationGuage.rectTransform.localScale = new Vector3(1.0f, 1.0f-e.PercentComplete, 1.0f);

        }
    }
}