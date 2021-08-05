using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MarblesAndMonsters.Menus
{
    public class InventoryItemUITemplate : MonoBehaviour
    {
        public Button button;
        public Text Quantity;

        private void Awake()
        {
            button = GetComponent<Button>();
        }


    }
}