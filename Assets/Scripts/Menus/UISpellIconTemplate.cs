using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MarblesAndMonsters.Menus
{
    public class UISpellIconTemplate : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public Image Icon;
        public Image SelectedBackground;
        public Image DragIcon;
        protected BackpackController backpackController;

        public Canvas parentCanvas;
        Vector2 pos;

        private void OnMouseDown()
        {
            SelectedBackground.color = Color.green;
        }

        private void OnMouseExit()
        {
            SelectedBackground.color = Color.white;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("Inventory Item OnBeginDrag()");
            DragIcon.enabled = true;
            DragIcon.sprite = Icon.sprite;
            DragIcon.color = Color.white;
            DragIcon.transform.position = transform.position;
            //RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, eventData.position, parentCanvas.worldCamera, out pos);
            //DragIcon.transform.position = parentCanvas.transform.TransformPoint(pos);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("Inventory Item OnDrag()");
            //RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, eventData.position, parentCanvas.worldCamera, out pos);
            //DragIcon.transform.position += parentCanvas.transform.TransformPoint(pos);
            DragIcon.transform.position += (Vector3)eventData.delta;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("Inventory Item OnEndDrag()");
            DragIcon.enabled = false;
        }

        private void Awake()
        {
            backpackController = GetComponentInParent<BackpackController>();
            DragIcon.enabled = false;
        }
    }
}