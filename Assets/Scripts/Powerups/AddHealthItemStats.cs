using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Characters;

namespace MarblesAndMonsters.Items
{
    [CreateAssetMenu(menuName = "Stats/Item Stats/Health Stats")]
    public class AddHealthItemStats : ItemStats
    {
        public int HealingStrength;

        public override void Action()
        {
            base.Action();
            if (Player.Instance != null)
            {
                Player.Instance.AddMaxHealth(HealingStrength);
            }
        }
    }

}