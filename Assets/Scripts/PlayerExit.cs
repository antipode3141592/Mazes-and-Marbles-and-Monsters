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
                    if (GoToLevelId == string.Empty)
                    {
                        GameController.Instance.LevelWin();
                    } else
                    {
                        GameController.Instance.LevelWin(GoToLevelId);
                    }
                }
            }
        }
    }
}