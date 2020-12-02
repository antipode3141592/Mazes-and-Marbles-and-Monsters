using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MarblesAndMonsters.Items
{
    [CreateAssetMenu(menuName = "Stats/Item Stats/Health Stats")]
    public class AddHealthItemStats : ItemStats
    {
        public int HealingStrength;
    }

}