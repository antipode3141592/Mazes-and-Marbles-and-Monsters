using MarblesAndMonsters.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.Items
{
    [CreateAssetMenu(menuName = "Stats/Item Stats/Magic Shield Item Stats")]

    public class MagicShieldItemStats : ItemStats
    {
        public override void Action()
        {
            base.Action();
            if (Player.Instance != null)
            {
                Player.Instance.UseForceBubble();
            }
        }
    }
}