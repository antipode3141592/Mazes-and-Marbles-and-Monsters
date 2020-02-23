using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;    //text mesh pro library for UI stuff
using HutongGames.PlayMaker;

public class GameController : MonoBehaviour
{
    public float ForceMultiplier;
    public int MarbleCount;
    public GameObject playerPrefab;

    PlayMakerFSM playerHealthManagerFSM;
    PlayMakerFSM playerMovementManagerFSM;
    string[] levelNameArray;
    GameObject player;
    List<GameObject> activeMarbles;
    GameObject monster;
    GameObject[] marbleSpawnPoints;
    GameObject playerSpawnpoint;
    DeathCounterController deathCountUI;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        activeMarbles = new List<GameObject>();
        marbleSpawnPoints = GameObject.FindGameObjectsWithTag("Spawn_Marble");
        playerSpawnpoint = GameObject.FindGameObjectWithTag("Spawn_Player");
        deathCountUI = GameObject.FindObjectOfType<DeathCounterController>();

        PlayMakerFSM[] playerFSMs;
        playerFSMs = player.GetComponents<PlayMakerFSM>();
        foreach (PlayMakerFSM fsm in playerFSMs)
        {
            if (fsm.FsmName == "MovementManager")
            {
                playerMovementManagerFSM = fsm;
            } else if (fsm.FsmName == "HealthManager")
            {
                playerHealthManagerFSM = fsm;
            }
        }
    }

    public void MoveActiveObjects()
    {
        //get controller input
        float horizontal = Input.acceleration.x;
        float vertical = Input.acceleration.y;
        Vector2 move = new Vector2(horizontal, vertical);
        //Debug.Log("acceration output: " + move);
        if (player.activeSelf)
        {
            var playerbody = player.GetComponent<Rigidbody2D>();
            playerbody.AddForce(move * playerbody.mass *ForceMultiplier);
        }
        if (activeMarbles.Count > 0)
        {
            for(int i = 0; i < activeMarbles.Count; i++)
            {
                var marbleBody = activeMarbles[i].GetComponent<Rigidbody2D>();
                marbleBody.AddForce(move * marbleBody.mass * ForceMultiplier);
            }
        }      
    }

    public void DestroyMarble(GameObject marble)
    {
        //Debug.Log("DestroyMarble says, 'Marble be gone!'");
        activeMarbles.Remove(marble);   //out of the active list (so they aren't moved)
        marble.SetActive(false);      //go to sleep
    }

    public void SpawnMarble()
    {
        //Debug.Log("Spawn a Marble!");
        int randomNumber = (int)Random.Range(0, marbleSpawnPoints.Length - 1);
        var spawnTransform = marbleSpawnPoints[randomNumber].transform;
        GameObject marble = ObjectPooler.SharedInstance.GetPooledObject("Marble");
        if (marble != null) //make sure it exists!
        {
            marble.transform.position = spawnTransform.position;
            marble.transform.rotation = spawnTransform.rotation;
            activeMarbles.Add(marble);  //add to active list so it can move
            marble.SetActive(true); //awake!
        }
     
    }

    public void SpawnMarbles(int N)
    {
        int i = 0;
        if (activeMarbles.Count > 0) { i = activeMarbles.Count; }
        for(; i < N; i++)
        {
            SpawnMarble();
        }
            
    }

    public void DestroyPlayer()
    {
        player.SetActive(false);
        deathCountUI.UpdateDeathCountUI();
        Debug.Log("DestroyPlayer(), player set active false");
        //SpawnPlayer();
    }

    public void SpawnPlayer()
        
    {
        Debug.Log("SpawnPlayer() called");
        if (player != null)
        {
            player.SetActive(true);
        }
        else
        {
            Debug.Log("Player arrives in level for first time!");
            player = (GameObject)Instantiate(playerPrefab);
        }
        Debug.Log("reset player position to spawn point");
        player.transform.position = playerSpawnpoint.transform.position;
        player.transform.rotation = playerSpawnpoint.transform.rotation;
        //fsm.SetState("Spawned");
        playerHealthManagerFSM.SendEvent("START");
        playerMovementManagerFSM.SendEvent("START");
    }
}
