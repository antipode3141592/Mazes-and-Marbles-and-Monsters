using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;
using HutongGames;
using HutongGames.Utility;

public class Pit : MonoBehaviour
{
    //GameController gameController;
    GameObject player;
    PlayMakerFSM playerFSM;
    PlayMakerFSM[] playerFSMs;
    //public GameObject playerController;
    // Start is called before the first frame update
    private void Awake()
    {
        //gameController = FindObjectOfType<GameController>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerFSMs = player.GetComponents<PlayMakerFSM>();
        foreach (PlayMakerFSM fsm in playerFSMs)
        {
            if (fsm.FsmName == "MovementManager")
            {
                playerFSM = fsm;
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other)
        {
            //Debug.Log("Pit says: Go to sleep" + other.gameObject + "!");
            //Destroy(other.gameObject);
            if (other.gameObject.CompareTag("Marble"))
            {
                GameController.Instance.DestroyMarble(other.gameObject);
                //Debug.Log("respawn a marble!");
                StartCoroutine(SpawnMarble());
                //GameController.Instance.SpawnMarble();
            }
            else if (other.gameObject.CompareTag("Monster"))
            {
                other.gameObject.SetActive(false);  //go to sleep, monster!
            }
            else if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("pit detects player collision");
                //PlayMakerFSM[] fsms = other.gameObject.GetComponents<PlayMakerFSM>();
                //PlayMakerFSM playerFSM = null;
                //foreach(PlayMakerFSM fsm in fsms)
                //{
                //    if(fsm.FsmName == "MovementManager")
                //    {
                //        playerFSM = fsm;
                //        break;
                //    }
                //}
                ////fsm = FindFsmOnGameObject(other.gameObject, "MovementManager");
                if (playerFSM != null)
                {
                    Debug.Log("Send IsFalling event from pit collider script");
                    playerFSM.SendEvent("IsFalling");
                }
            }

        }
    }

    private IEnumerator SpawnMarble()
    {
        yield return new WaitForSeconds(1.5f);
        GameController.Instance.SpawnMarble();
    }
}
