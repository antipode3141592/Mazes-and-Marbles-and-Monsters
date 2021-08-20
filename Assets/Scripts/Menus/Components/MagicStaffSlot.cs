using System.Collections;
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
        public SpellStats SpellStats;
        private List<RaycastResult> raycastResults;

        protected void Awake()
        {
            raycastResults = new List<RaycastResult>();
            SpellIcon.color = Color.clear;
            //BackgroundImage.color = Color.clear;
            DragIcon.enabled = false;
        }

        public void AssignSlot(SpellStats spellStats)
        {
            SpellIcon.sprite = spellStats.InventoryIcon;
            SpellIcon.color = Color.white;
            //BackgroundImage.color = Color.green;
            SpellStats = spellStats;
        }

        public void UnassignSlot()
        {
            SpellIcon.sprite = null;
            SpellIcon.color = Color.clear;
            //BackgroundImage.color = Color.white;
            SpellStats = null;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            //if (IsUnlocked)
            //{
                DragIcon.enabled = true;
                DragIcon.sprite = SpellIcon.sprite;
                DragIcon.color = Color.white;
                DragIcon.transform.position = transform.position;
                BackpackMenu.Instance.QuickAccessDelete.Icon.color = Color.white;
            //}
        }

        public void OnDrag(PointerEventData eventData)
        {
            //if (DragIcon.enabled)
            //{
                DragIcon.transform.position += (Vector3)eventData.delta;
            //}
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //Debug.Log("Inventory Item OnEndDrag()");
            raycastResults.Clear();
            BackpackMenu.Instance.GraphicRaycaster.Raycast(eventData, raycastResults);
            foreach (var result in raycastResults)
            {
                QuickAccessDelete deleteIcon = result.gameObject.GetComponent<QuickAccessDelete>();
                if (deleteIcon)
                {
                    Debug.Log(string.Format("{0} has struck a delete icon, unassigning", name));
                    UnassignSlot();
                    break;
                }
                //MagicStaffSlot staffSlot = result.gameObject.GetComponent<MagicStaffSlot>();
                //if (staffSlot)
                //{
                //    //Debug.Log(string.Format("OnEndDrag() landed on MagicStaffSlot {0}", staffSlot.QuickSlot));
                //    staffSlot.AssignSlot(SpellStats);
                //    break;
                //}
            }
            BackpackMenu.Instance.QuickAccessDelete.Icon.color = Color.clear;
            DragIcon.enabled = false;
        }
    }
}