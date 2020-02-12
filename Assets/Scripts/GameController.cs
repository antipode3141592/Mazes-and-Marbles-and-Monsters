using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;    //text mesh pro library for UI stuff

public class GameController : MonoBehaviour
{
    public float ForceMultiplier;
    public int MarbleCount;
    TextMeshProUGUI winText;

    bool isPaused;

    GameObject player;
    List<GameObject> activeMarbles;
    GameObject monster;
    GameObject[] marbleSpawnPoints;
    GameObject playerSpawnpoint;
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        activeMarbles = new List<GameObject>();
        //activeMarbles.AddRange(GameObject.FindGameObjectsWithTag("Marble"));
        marbleSpawnPoints = GameObject.FindGameObjectsWithTag("Spawn_Marble");
        playerSpawnpoint = GameObject.FindGameObjectWithTag("Spawn_Player");
        winText = GameObject.FindObjectOfType<TextMeshProUGUI>();
        winText.text = "";
    }

    private void Start()
    {
        //activeMarbles.AddRange(GameObject.FindGameObjectsWithTag("Marble"));
        for (int i=0; i < MarbleCount; i++)
        {
            SpawnMarble();
        }
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            MoveActiveObjects();
        }
    }

    void MoveActiveObjects()
    {
        //get controller input
        float horizontal = Input.acceleration.x;
        float vertical = Input.acceleration.y;
        Vector2 move = new Vector2(horizontal, vertical);

        //Debug.Log("acceration output: " + move);
        if (player)
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
        
        //collect active gameObjects (pc, npc, marble)


        //GetComponent<Rigidbody2D>().AddForce(move * forceMultiplier);
    }

    public void DestroyMarble(GameObject marble)
    {
        Debug.Log("DestroyMarble says, 'Marble be gone!'");
        activeMarbles.Remove(marble);   //out of the active list (so they aren't moved)
        marble.SetActive(false);      //go to sleep
    }

    public void SpawnMarble()
    {
        Debug.Log("Spawn a Marble!");
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

    public void PlayerLevelComplete()
    {
        Debug.Log("Player got to the exit!");
        isPaused = true;
        //winText.text = "NAILED IT!";
        SceneManager.LoadSceneAsync("Scenes/Level_02");
    }

    public void DestroyPlayer()
    {
        player.SetActive(false);
        Debug.Log("Player died!  So sad!");
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        if (player != null)
        {
            player.transform.position = playerSpawnpoint.transform.position;
            player.transform.rotation = playerSpawnpoint.transform.rotation;
            player.SetActive(true);
        }
    }
}
