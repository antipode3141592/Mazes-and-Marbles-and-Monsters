using MarblesAndMonsters.Characters;
using UnityEngine;
using UnityEngine.UI;

namespace MarblesAndMonsters.Menus.Components
{
    public class TreasureCounterController : MonoBehaviour
    {
        int treasureCount;
        public Text treasureCountText;

        void Start()
        {
            UpdateTreasureCount();
        }

        public void UpdateTreasureCount()
        {
            if (treasureCountText != null && Player.Instance != null)
            {
                treasureCount = Player.Instance.TreasureCount;
                treasureCountText.text = treasureCount.ToString();
            }
        }
    }
}
