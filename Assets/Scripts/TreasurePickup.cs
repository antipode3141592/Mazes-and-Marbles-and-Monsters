using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;


public class TreasurePickup : MonoBehaviour
{
    GameObject player;
    Player playerController;
    TreasureCounterController treasureUI;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = GameObject.FindObjectOfType<Player>();   //grab that player controller
        treasureUI = GameObject.FindObjectOfType<TreasureCounterController>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FsmVariables.GlobalVariables.FindFsmInt("TreasureCount_global").Value += 1;
            treasureUI.UpdateTreasureCountUI();
            Destroy(gameObject);
        }
    }
}
