using MarblesAndMonsters.Characters;
using UnityEngine;
using UnityEngine.UI;

namespace MarblesAndMonsters.Menus.Components
{
    public class ScrollCounterController : MonoBehaviour
    {
        int scrollCount;
        public Text ScrollCountText;

        void Start()
        {
            UpdateTreasureCount();
        }

        public void UpdateTreasureCount()
        {
            if (ScrollCountText != null && Player.Instance != null)
            {
                scrollCount = Player.Instance.TreasureCount;
                ScrollCountText.text = scrollCount.ToString();
            }
        }
    }
}
