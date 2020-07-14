using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class HealthPotion : MonoBehaviour
{
    //PlayMakerFSM playerHealthManagerFSM;
    GameObject player;
    Player playerController;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = GameObject.FindObjectOfType<Player>();   //grab that player controller
        //PlayMakerFSM[] playerFSMs;
        //playerFSMs = player.GetComponents<PlayMakerFSM>();
        //foreach (PlayMakerFSM fsm in playerFSMs)
        //{
        //    if (fsm.FsmName == "HealthManager")
        //    {
        //        playerHealthManagerFSM = fsm;
        //    }
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (FsmVariables.GlobalVariables.FindFsmInt("PlayerHealth_global").Value < FsmVariables.GlobalVariables.FindFsmInt("PlayerMaxHealth_global").Value)
            {
                playerController.AdjustHealthUI(+1);
                playerController.IsHealedEffectParticles();
                Destroy(gameObject);    //destroy self (these are relatively rare, so no need for pooling)
            }
            else
            {
                //nothing (only pick up when player has available health
            }
        }
    }
}