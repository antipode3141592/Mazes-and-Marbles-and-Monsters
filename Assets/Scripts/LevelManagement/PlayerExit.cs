using UnityEngine;

namespace MarblesAndMonsters.Objects
{
    public class PlayerExit : MonoBehaviour
    {
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
                    _gameManager.LevelWin();
                }
            }
        }
    }
}