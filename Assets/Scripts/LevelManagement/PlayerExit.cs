using LevelManagement.Levels;
using UnityEngine;
using Zenject;

namespace MarblesAndMonsters.Objects
{
    public class PlayerExit : MonoBehaviour
    {
        protected IGameManager _gameManager;

        [SerializeField] protected LevelSpecs gotoLevel;

        [Inject]
        public void Init(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    _gameManager.LevelWin(gotoLevel);
                }
            }
        }
    }
}