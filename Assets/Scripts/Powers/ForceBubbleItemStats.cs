using MarblesAndMonsters.Actions;
using MarblesAndMonsters.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.Items
{
    [CreateAssetMenu(menuName = "Stats/Item Stats/Force Bubble Item Stats")]

    public class ForceBubbleItemStats : ItemStats
    {
        public override void Action()
        {
            base.Action();
            if (Player.Instance != null)
            {
                Player.Instance.MySheet.Actions.Find(x => x.ActionName == AssociatedAction).Action();
            }
        }
    }
}