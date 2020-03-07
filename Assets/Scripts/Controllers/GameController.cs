using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LevelManagement;
using LevelManagement.Data;
using LevelManagement.Levels;
using TMPro;    //text mesh pro library for UI stuff
using HutongGames.PlayMaker;

//persistent singleton class for controlling most of the game actions
public class GameController : MonoBehaviour
{
    [SerializeField]
    private float forceMultiplier;
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private TransitionFader transitionPrefab;
    [SerializeField]
    private GameObject marblePrefab;

    private LevelSelector levelSelector;
    private static GameController _instance;

    PlayMakerFSM playerHealthManagerFSM;
    PlayMakerFSM playerMovementManagerFSM;
    PlayMakerFSM gameControllerFSM;
    string[] levelNameArray;
    GameObject player;
    //List<GameObject> activeMarbles;
    List<GameObject> marblePool;
    //GameObject monster;
    List<GameObject> marbleSpawnPoints;
    Bag marbleBag;
    List<GameObject> monsterSpawnPoints;
    //List<GameObject> treasureSpawnPoints;
    GameObject playerSpawnpoint;
    //DeathCounterController deathCountUI;

    public static GameController Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        //that singleton pattern
        if (_instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            //DontDestroyOnLoad(this.gameObject);
            InitializeReferences();
        }
    }

    private void InitializeReferences()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //activeMarbles = new List<GameObject>(); //empty array of active Marbles
        marblePool = new List<GameObject>();
        
        marbleSpawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Spawn_Marble"));
        Debug.Log("There are " + marbleSpawnPoints.Count + " marble spawn points!");
        marbleBag = new Bag(marbleSpawnPoints.Count);
        monsterSpawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Spawn_Monster"));
        //treasureSpawnPoints = new List<GameObject>(GameObject.Findga
        playerSpawnpoint = GameObject.FindGameObjectWithTag("Spawn_Player");
        //deathCountUI = GameObject.FindObjectOfType<DeathCounterController>();

        levelSelector = Object.FindObjectOfType<LevelSelector>();


        PlayMakerFSM[] playerFSMs;
        playerFSMs = player.GetComponents<PlayMakerFSM>();
        foreach (PlayMakerFSM fsm in playerFSMs)
        {
            if (fsm.FsmName == "MovementManager")
            {
                playerMovementManagerFSM = fsm;
            }
            else if (fsm.FsmName == "HealthManager")
            {
                playerHealthManagerFSM = fsm;
            }
        }

        gameControllerFSM = Instance.GetComponent<PlayMakerFSM>();
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
    // check for the end game condition on each frame
    private void Update()
    {
        //if (_objective != null && _objective.IsComplete)
        //{
        //    EndLevel();
        //}
    }

    public void EndLevel()
    {
        //if (_player != null)
        //{
        //    // disable the player controls
        //    ThirdPersonUserControl thirdPersonControl =
        //        _player.GetComponent<ThirdPersonUserControl>();

        //    if (thirdPersonControl != null)
        //    {
        //        thirdPersonControl.enabled = false;
        //    }

        //    // remove any existing motion on the player
        //    Rigidbody rbody = _player.GetComponent<Rigidbody>();
        //    if (rbody != null)
        //    {
        //        rbody.velocity = Vector3.zero;
        //    }

        //    // force the player to a stand still
        //    _player.Move(Vector3.zero, false, false);
        //}

        //// check if we have set IsGameOver to true, only run this logic once
        //if (_goalEffect != null && !_isGameOver)
        //{


        //    _isGameOver = true;
        //    _goalEffect.PlayEffect();
        //    StartCoroutine(WinRoutine());
        //}

        DataManager.Instance.HigestLevelUnlocked = levelSelector.CurrentIndex+1;    //unlock next index
        //DataManager.Instance.
        DataManager.Instance.Save();
        
        gameControllerFSM.SendEvent("WinningGame");

        WinMenu.Open();
        //if (true)
        //{
        //    StartCoroutine(WinRoutine());
        //}
    }

    private IEnumerator WinRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        //TransitionFader.PlayTransition(transitionPrefab);
        //float fadeDelay = transitionPrefab != null ? transitionPrefab.Delay + transitionPrefab.FadeOnDuration : 0f;
        //yield return new WaitForSeconds(fadeDelay);
        WinMenu.Open();
    }

    public void MoveActiveObjects()
    {
        //get controller input
        float horizontal = Input.acceleration.x;
        float vertical = Input.acceleration.y;
        Vector2 move = new Vector2(horizontal, vertical);
        //Debug.Log("acceration output: " + move);
        if (player != null && player.activeInHierarchy)
        {
            var playerbody = player.GetComponent<Rigidbody2D>();
            playerbody.AddForce(move * playerbody.mass *forceMultiplier);
        }
        for(int i = 0; i < marblePool.Count; i++)
        {
            if (marblePool[i] != null && marblePool[i].activeInHierarchy)
            {
                var marbleBody = marblePool[i].GetComponent<Rigidbody2D>();
                marbleBody.AddForce(move * marbleBody.mass * forceMultiplier);
            }
        }      
    }

    public void SpawnAll()
    {
        //deactivate all
        DeactivateAll();

        //spawn marbles equal to number of marble spawn points
            //instantiate any marbles that are not all ready in the scene (in case of respawn)
        for (int i = marblePool.Count; i < marbleSpawnPoints.Count; i++)
        {
            GameObject obj = (GameObject)Instantiate(marblePrefab);
            obj.SetActive(false);
            marblePool.Add(obj);
        }   //this should ensure that there are an equal number of instantiated (inactive) marbles to marble SpawnPoints
        //now place marbles on each spawn point
        for (int i = 0; i < marblePool.Count; i++)
        {
            SpawnMarble();
        }
        

        //placeholder for monster respawns

        //placeholder for treasure respawn and treasure counter reset

    }

    public void DeactivateAll()
    {

        foreach (var marble in marblePool)
        {
            if (marble != null) 
            { 
                marble.SetActive(false); //deactivate all marbles in marblePool
            }
        }
    }

    public void DestroyMarble(GameObject marble)
    {
        //Debug.Log("DestroyMarble says, 'Marble be gone!'");
        //activeMarbles.Remove(marble);   //out of the active list (so they aren't moved)
        marble.SetActive(false);      //go to sleep
    }

    public void SpawnMarble()
    {
        //Debug.Log("Spawn a Marble!");
        //int randomNumber = (int)Random.Range(0, marbleSpawnPoints.Count - 1);
        var spawnTransform = marbleSpawnPoints[marbleBag.DrawFromBag()].transform;
        GameObject marble = null;  // = ObjectPooler.SharedInstance.GetPooledObject("Marble");

        for (int i = 0; i < marblePool.Count; i++)
        {
            if (!marblePool[i].activeInHierarchy)   //grab first available inactive marble
            {
                marble = marblePool[i];
                break;
            }
        }
        if (marble != null) //make sure it exists, most to spawn point and then activate!
        {
            marble.transform.position = spawnTransform.position;
            marble.transform.rotation = spawnTransform.rotation;
            //activeMarbles.Add(marble);  //add to active list so it can move
            marble.SetActive(true); //awake!
            //probably need to add a spawn animation here
        }

    }

    public void SpawnMarbles(int N)
    {
        int i = 0;
        if (marblePool.Count > 0) { i = marblePool.Count; }
        for(; i < N; i++)
        {
            SpawnMarble();
        }
            
    }

    public void DestroyPlayer()
    {
        player.SetActive(false);
        //deathCountUI.UpdateDeathCountUI();
        GameMenu.Instance.UpdateDeathCount();
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
