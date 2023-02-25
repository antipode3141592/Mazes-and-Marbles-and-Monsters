using LevelManagement.DataPersistence;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MarblesAndMonsters.Menus.Components
{
    public class MagicStaffSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Image SpellIcon;
        public Image BackgroundImage;
        public Image DragIcon;
        public int QuickSlot;
        public bool IsUnlocked;
        public SpellStatsBase SpellStats;
        private List<RaycastResult> raycastResults;
        private GraphicRaycaster graphicRaycaster;
        private List<MagicStaffSlot> otherSlots;

        protected void Awake()
        {
            otherSlots = new List<MagicStaffSlot>(FindObjectsOfType<MagicStaffSlot>(true));
            graphicRaycaster = GetComponentInParent<GraphicRaycaster>();
            raycastResults = new List<RaycastResult>();
            //SpellIcon.color = Color.clear;
            DragIcon.enabled = false;
        }

        public void AssignSlot(SpellStatsBase newStats)
        {
            if (IsUnlocked)
            {
                foreach (MagicStaffSlot otherSlot in otherSlots)
                {
                    //if any otherSlots share the newStats, unassign them
                    if (otherSlot.isActiveAndEnabled && (otherSlot.SpellStats == newStats))
                    {
                        //if this slots 
                        if (SpellStats == otherSlot.SpellStats)
                        {
                            otherSlot._AssignSlot(SpellStats);
                        }
                        else
                        {
                            otherSlot.UnassignSlot();
                        }
                    }
                }
                _AssignSlot(newStats);
            }
        }

        public void _AssignSlot(SpellStatsBase newStats)
        {
            SpellIcon.sprite = newStats.InventoryIcon;
            SpellIcon.color = Color.white;
            SpellStats = newStats;
        }

        public void UnassignSlot()
        {
            SpellIcon.sprite = null;
            SpellIcon.color = Color.clear;
            SpellStats = null;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (IsUnlocked)
            {
                DragIcon.enabled = true;
                DragIcon.sprite = SpellIcon.sprite;
                DragIcon.color = Color.white;
                DragIcon.transform.position = transform.position;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            DragIcon.transform.position += (Vector3)eventData.delta;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //Debug.Log("Inventory Item OnEndDrag()");
            raycastResults.Clear();
            graphicRaycaster.Raycast(eventData, raycastResults);
            foreach (var result in raycastResults)
            {
                MagicStaffSlot staffSlot = result.gameObject.GetComponent<MagicStaffSlot>();
                if (staffSlot)
                {
                    //Debug.Log(string.Format("OnEndDrag() landed on MagicStaffSlot {0}", staffSlot.QuickSlot));
                    staffSlot.AssignSlot(SpellStats);
                    break;
                }
            }
            DragIcon.enabled = false;
        }
    }
}