using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                GameController.Instance.EndLevel();
            }
        }
    }
}
