using MarblesAndMonsters.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.Items
{
    [CreateAssetMenu(menuName = "Stats/Item Stats/Levitating Item Stats")]
    public class LevitatingItemStats : ItemStats
    {
        public float Duration;
        public override void Action()
        {
            base.Action();
            if (Player.Instance != null)
            {
                Player.Instance.ApplyLevitate(Duration);
            }
        }
    }
}