using UnityEngine;

namespace MarblesAndMonsters.Tiles
{
    [CreateAssetMenu(menuName = "Stats/Lockables/Gate Data")]
    public class GateData : ScriptableObject
    {
        public KeyType requiredKey;   //key type needed to open the gate
    }
}
