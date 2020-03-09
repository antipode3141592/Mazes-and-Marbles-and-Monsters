using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class AddHealthPotion : MonoBehaviour
{
    GameObject player;
    Player playerController;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = GameObject.FindObjectOfType<Player>();   //grab that player controller
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController.AddMaxHealthUI(1);
            playerController.PlayAddMaxHealthParticles();
            Destroy(gameObject);    //destroy self (these are relatively rare, so no need for pooling)
        }
    }
}
