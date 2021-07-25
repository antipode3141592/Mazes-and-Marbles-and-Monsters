using MarblesAndMonsters.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.Items
{
    [CreateAssetMenu(menuName = "Stats/Item Stats/Magic Shield Item Stats")]

    public class MagicShieldItemStats : ItemStats
    {
        public float Radius;    //
        public float ForceMultiplier = 0.0f;    //

        /// <summary>
        /// 
        /// </summary>
        public override void Action()
        {
            if (Player.Instance != null)
            {
                Player.Instance.UseForceBubble();
            }
        }
    }
}