using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Items;

namespace MarblesAndMonsters.Tiles
{
    [CreateAssetMenu(menuName = "Stats/Lockables/Gate Data")]
    public class GateData : ScriptableObject
    {
        public KeyType requiredKey;   //key type needed to open the gate
        public Sprite defaultSprite;
    }
}
