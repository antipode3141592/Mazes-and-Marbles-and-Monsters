using MarblesAndMonsters.Characters;
using UnityEngine;
using UnityEngine.UI;

namespace LevelManagement.Menus.Components
{

    public class DeathCounterController : MonoBehaviour
    {
        public Text deathCountText;

        void Start()
        {
            UpdateDeathCountUI();
        }

        public void UpdateDeathCountUI()
        {
            Debug.Log(string.Format("UpdateDeathCountUI() deathcount = {0}", Player.Instance.DeathCount.ToString()));
            deathCountText.text = Player.Instance.DeathCount.ToString();
        }
    }
}