using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class FixedMovementMonster : MonoBehaviour
{
    public int strength;
    //GameController gameController;
    ////public GameObject playerController;
    //// Start is called before the first frame update
    //private void Awake()
    //{
    //    gameController = GameController.FindObjectOfType<GameController>();
    //}


    //Damage Player upon collision
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("pit detects player collision");
            PlayMakerFSM[] fsms = other.gameObject.GetComponents<PlayMakerFSM>();
            PlayMakerFSM playerFSM = null;
            foreach (PlayMakerFSM fsm in fsms)
            {
                if (fsm.FsmName == "HealthManager")
                {
                    playerFSM = fsm;
                    break;
                }
            }
            if (playerFSM != null)
            {
                FsmBool invincibleCheck = FsmVariables.GlobalVariables.FindFsmBool("PlayerInvincible");
                //var invincibleCheck = playerFSM.FsmVariables.GetFsmBool("PlayerInvincible");
                if (!invincibleCheck.Value)
                {
                    playerFSM.SendEvent("IsHit");
                }
            }
        }
    }
}
