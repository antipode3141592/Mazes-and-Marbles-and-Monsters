using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExit : MonoBehaviour
{
    GameController gameController;

    private void Awake()
    {
        gameController = GameController.FindObjectOfType<GameController>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                gameController.PlayerLevelComplete();
            }
        }
    }
}
