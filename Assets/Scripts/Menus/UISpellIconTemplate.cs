using System.Collections;
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
        protected BackpackController backpackController;
        public bool IsUnlocked;
        public SpellStats SpellStats;

        Vector2 pos;

        private void Awake()
        {
            //backpackController = GetComponentInParent<BackpackController>();
            DragIcon.enabled = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            SelectedBackground.color = Color.green;
            if (BackpackMenu.Instance)
            {
                BackpackMenu.Instance.SpellDescription.text = SpellStats.Description;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SelectedBackground.color = Color.clear;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("Inventory Item OnBeginDrag()");
            DragIcon.enabled = true;
            DragIcon.sprite = Icon.sprite;
            DragIcon.color = Color.white;
            DragIcon.transform.position = transform.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("Inventory Item OnDrag()");
            DragIcon.transform.position += (Vector3)eventData.delta;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("Inventory Item OnEndDrag()");
            DragIcon.enabled = false;
        }
    }
}