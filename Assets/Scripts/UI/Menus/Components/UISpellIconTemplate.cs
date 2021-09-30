using LevelManagement.DataPersistence;
using MarblesAndMonsters.Menus.Components;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MarblesAndMonsters.Menus
{
    public class UISpellIconTemplate : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public Image Icon;
        public Image SelectedBackground;
        public Image DragIcon;
        //protected BackpackController backpackController;
        public MagicStaffController MagicStaffController;
        public bool IsUnlocked;
        public SpellStats SpellStats;
        public GraphicRaycaster graphicRaycaster;
        private List<RaycastResult> raycastResults;

        protected BackpackMenu _backpackMenu;

        Vector2 pos;

        private void Awake()
        {
            //backpackController = GetComponentInParent<BackpackController>();
            raycastResults = new List<RaycastResult>();
            graphicRaycaster = GetComponentInParent<GraphicRaycaster>();
            MagicStaffController = FindObjectOfType<MagicStaffController>();
            _backpackMenu = FindObjectOfType<BackpackMenu>(true);
            //DragIcon = FindObjectOfType<DragIcon>(true).GetComponent<Image>();
            //DragIcon.enabled = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (IsUnlocked)
            {
                SelectedBackground.color = Color.green;
                if (_backpackMenu)
                {
                    _backpackMenu.SpellDescription.text = SpellStats.Description;
                }
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SelectedBackground.color = Color.clear;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            //Debug.Log("Inventory Item OnBeginDrag()");
            DragIcon.enabled = true;
            DragIcon.sprite = Icon.sprite;
            DragIcon.color = Color.white;
            DragIcon.transform.position = transform.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            //Debug.Log("Inventory Item OnDrag()");
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
                    Debug.Log(string.Format("OnEndDrag() landed on MagicStaffSlot {0}", staffSlot.QuickSlot));
                    staffSlot.AssignSlot(SpellStats);
                    break;
                }
            }
            DragIcon.enabled = false;
        }
    }
}