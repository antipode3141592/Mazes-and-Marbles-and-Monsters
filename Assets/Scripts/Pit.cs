using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;
using HutongGames;
using HutongGames.Utility;

public class Pit : MonoBehaviour
{
    GameController gameController;
    //public GameObject playerController;
    // Start is called before the first frame update
    private void Awake()
    {
        gameController = GameController.FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other)
        {
            //Debug.Log("Pit says: Go to sleep" + other.gameObject + "!");
            //Destroy(other.gameObject);
            if (other.gameObject.CompareTag("Marble"))
            {
                gameController.DestroyMarble(other.gameObject);
                //Debug.Log("respawn a marble!");
                gameController.SpawnMarble();
            }
            else if (other.gameObject.CompareTag("Player"))
            {
                //Debug.Log("pit detects player collision");
                PlayMakerFSM[] fsms = other.gameObject.GetComponents<PlayMakerFSM>();
                PlayMakerFSM playerFSM = null;
                foreach(PlayMakerFSM fsm in fsms)
                {
                    if(fsm.FsmName == "MovementManager")
                    {
                        playerFSM = fsm;
                        break;
                    }
                }
                //fsm = FindFsmOnGameObject(other.gameObject, "MovementManager");
                if (playerFSM != null)
                {
                    playerFSM.SendEvent("IsFalling");
                }
            }

        }
    }
}
