using MarblesAndMonsters.Characters;
using UnityEngine;
using UnityEngine.UI;

namespace MarblesAndMonsters
{
    public class TreasureCounterController : MonoBehaviour
    {
        int treasureCount;
        public Text treasureCountText;

        void Start()
        {
            UpdateTreasureCountUI();
        }

        public void UpdateTreasureCountUI()
        {
            if (treasureCountText != null)
            {
                treasureCount = Player.Instance.TreasureCount;
                treasureCountText.text = treasureCount.ToString();
            }
        }
    }
}
