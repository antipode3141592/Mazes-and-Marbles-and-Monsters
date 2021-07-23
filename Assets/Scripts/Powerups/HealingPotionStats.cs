using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Characters;

namespace MarblesAndMonsters.Items
{
    [CreateAssetMenu(menuName = "Stats/Item Stats/Healing Item Stats")]

    public class HealingPotionStats: ItemStats
    {
        public override void Action()
        {
            if (Player.Instance != null)
            {
                //if damage is healed, consume potion
                if (Player.Instance.HealDamage(-1)) 
                {
                    Player.Instance.ConsumeItem(Id);
                }
            }
        }
    }
}