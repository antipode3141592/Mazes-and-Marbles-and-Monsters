using UnityEngine;

namespace MarblesAndMonsters.Objects
{
    public class PlayerExit : MonoBehaviour
    {
        [SerializeField]
        protected string GoToLevel = string.Empty;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    if (GoToLevel == string.Empty)
                    {
                        GameController.Instance.LevelWin();
                        //GameController.Instance.EndLevel(true);
                    } else
                    {
                        GameController.Instance.LevelWin(GoToLevel);
                    }
                }
            }
        }
    }
}
