using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LevelManagement;
using LevelManagement.Data;
using LevelManagement.Levels;
using TMPro;    //text mesh pro library for UI stuff
using HutongGames.PlayMaker;
using MarblesAndMonsters;

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
    private PlayMakerFSM playerHealthManagerFSM;
    private PlayMakerFSM playerMovementManagerFSM;
    private PlayMakerFSM gameControllerFSM;
    string[] levelNameArray;
    private GameObject player;
    private List<Marble> marblePool;
    private List<Roller> rollers;
    private List<FollowPlayer> moveTowardPlayerObjects;
    private List<GameObject> marbleSpawnPoints;
    private Bag marbleBag;
    private List<GameObject> monsterSpawnPoints;
    private GameObject playerSpawnpoint;
    private List<GameObject> pickupSpawnPoints;


    private static GameController _instance;

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
        //lists
        marblePool = new List<Marble>();  //empty marble pool
        marbleSpawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Spawn_Marble"));  // find all marble spawn points
        monsterSpawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Spawn_Monster"));
        rollers = new List<Roller>(GameObject.FindObjectsOfType<Roller>());
        moveTowardPlayerObjects = new List<FollowPlayer>(GameObject.FindObjectsOfType<FollowPlayer>());
        pickupSpawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Pickup")); //find all pickups

        //initialize marble bag for randomizing marble spawns
        marbleBag = new Bag(marbleSpawnPoints.Count);

        playerSpawnpoint = GameObject.FindGameObjectWithTag("Spawn_Player");
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
    //private void Update()
    //{
    //    //if (_objective != null && _objective.IsComplete)
    //    //{
    //    //    EndLevel();
    //    //}
    //}

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

        Time.timeScale = 0f;    //pause time

        if (FsmVariables.GlobalVariables != null)
        {
            //if the Fsm global variables are available, save some of them to disk
            DataManager.Instance.PlayerMaxHealth = FsmVariables.GlobalVariables.FindFsmInt("PlayerMaxHealth_global").Value;
            DataManager.Instance.PlayerTotalDeathCount = FsmVariables.GlobalVariables.FindFsmInt("PlayerDeaths_global").Value;
            DataManager.Instance.PlayerTreasureCount = FsmVariables.GlobalVariables.FindFsmInt("TreasureCount_global").Value;
            
            DataManager.Instance.Save();
        }
        
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

    //public void MoveActiveObjects()
    //{
    //    //get controller input
    //    float horizontal = Input.acceleration.x;
    //    float vertical = Input.acceleration.y;
    //    Vector2 move = new Vector2(horizontal, vertical);
    //    //Debug.Log("acceration output: " + move);
    //    if (player != null && player.activeInHierarchy)
    //    {
    //        //var playerbody = player.GetComponent<Rigidbody2D>();
    //        //playerbody.AddForce(move * playerbody.mass *forceMultiplier);
    //        player.GetComponent<Player>().Move(move, forceMultiplier);
    //    }
    //    for(int i = 0; i < marblePool.Count; i++)
    //    {
    //        if (marblePool[i] != null && marblePool[i].gameObject.activeInHierarchy)
    //        {
    //            marblePool[i].Move(move, forceMultiplier);
    //            //var marbleBody = marblePool[i].GetComponent<Rigidbody2D>();
    //            //marbleBody.AddForce(move * marbleBody.mass * forceMultiplier);
    //        }
    //    }
    //    for(int i = 0; i< rollers.Count; i++)
    //    {
    //        if (rollers[i] != null)
    //        {
    //            rollers[i].Move(move, forceMultiplier);
    //        }
    //    }
    //    for (int i = 0; i < moveTowardPlayerObjects.Count; i++)
    //    {
    //        if (moveTowardPlayerObjects[i] != null && moveTowardPlayerObjects[i].gameObject.activeInHierarchy)
    //        {
    //            moveTowardPlayerObjects[i].Move(move, forceMultiplier);
    //        }
    //    }
    //}

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
            marblePool.Add(obj.GetComponent<Marble>());
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
                marble.gameObject.SetActive(false); //deactivate all marbles in marblePool
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
            if (!marblePool[i].gameObject.activeInHierarchy)   //grab first available inactive marble
            {
                marble = marblePool[i].gameObject;
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
