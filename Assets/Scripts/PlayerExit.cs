using UnityEngine;

namespace MarblesAndMonsters.Objects
{
    public class PlayerExit : MonoBehaviour
    {
        [SerializeField]
        protected string GoToLevelId = string.Empty;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    GameManager.Instance.LevelWin(GoToLevelId);
                }
            }
        }
    }
}