using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.Items
{
    [CreateAssetMenu(menuName = "Stats/Item Stats/Key Stats")]
    public class KeyStats : ItemStatsBase
    {
        public KeyType KeyType;
    }
}
