using UnityEngine;

namespace MarblesAndMonsters.Objects
{
    public class PlayerExit : MonoBehaviour
    {
        [SerializeField]
        protected string GoToLevelId = string.Empty;
        protected GameManager _gameManager;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    _gameManager.LevelWin(GoToLevelId);
                }
            }
        }
    }
}