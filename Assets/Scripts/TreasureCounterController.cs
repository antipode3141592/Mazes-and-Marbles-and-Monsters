﻿using MarblesAndMonsters.Characters;
using UnityEngine;
using UnityEngine.UI;

namespace LevelManagement.Menus.Components
{
    public class TreasureCounterController : MonoBehaviour
    {
        int treasureCount;
        public Text treasureCountText;

        void Start()
        {
            UpdateTreasureCountUI();
        }

        public void UpdateTreasureCountUI()
        {
            if (treasureCountText != null)
            {
                treasureCount = Player.Instance.TreasureCount;
                treasureCountText.text = treasureCount.ToString();
            }
        }
    }
}
