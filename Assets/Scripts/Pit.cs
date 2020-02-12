using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pit : MonoBehaviour
{
    GameController gameController;
    // Start is called before the first frame update
    private void Awake()
    {
        gameController = GameController.FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other)
        {
            Debug.Log("Pit says: Go to sleep" + other.gameObject + "!");
            //Destroy(other.gameObject);
            if (other.gameObject.CompareTag("Marble"))
            {
                gameController.DestroyMarble(other.gameObject);
                gameController.SpawnMarble();
            }
            else if (other.gameObject.CompareTag("Player"))
            {
                gameController.DestroyPlayer();
            }
            
        }
    }
}
