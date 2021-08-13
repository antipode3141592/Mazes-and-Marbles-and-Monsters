using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Characters;

namespace MarblesAndMonsters.Items
{
    [CreateAssetMenu(menuName = "Stats/Item Stats/Healing Item Stats")]

    public class HealingPotionStats: SpellStats
    {
        //public override void Action()
        //{
        //    base.Action();
        //    if (Player.Instance != null)
        //    {
        //        //if damage is healed, consume potion
        //        if (Player.Instance.HealDamage(-1)) 
        //        {
        //            Debug.Log(string.Format("Player healed damage by Item Id {0}", Id));
        //            //Player.Instance.ConsumeItem(Id);
        //        }
        //    }
        //}
    }
}