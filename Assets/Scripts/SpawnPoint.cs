using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  Basically an object pooler attached to a transform.  Spawns characters in queue at specified interval
namespace MarblesAndMonsters.Characters
{
    public abstract class SpawnPoint<T> : SpawnPoint where T : SpawnPoint<T>
    {
        //configuration
        [SerializeField]
        protected int charactersToSpawn = 1;    //# of characters this spawnpoint will spawn
        [SerializeField]
        private float spawnWaitPeriod = 1.5f;

        //sets offscreen 
        private static readonly float offScreenDefault_x = -1000f;
        private static readonly float offScreenDefault_y = 1000f;

        private Queue<CharacterSheetController> spawnQueue; //ephemeral 
        private List<CharacterSheetController> characters;  //the collection of all instantiated character objects
        
        private float spawnTimer;
        private bool isAvailable = true;   //true when the SpawnPoint's collider is clear and ready to start a spawn
        protected ContactFilter2D contactFilter;
        protected List<Collider2D> collidersInSpawnZone;

        protected Collider2D spawningZoneCollider;  //trigger area where the character will spawn
            //will only allow a spawn while the spawning zone is clear

        //instantiate the characters offscreen
        private void Awake()
        {
            contactFilter.NoFilter();
            spawningZoneCollider = gameObject.GetComponent<Collider2D>();
            characters = new List<CharacterSheetController>();
            spawnQueue = new Queue<CharacterSheetController>();
            collidersInSpawnZone = new List<Collider2D>();
            for (int i = 0; i < charactersToSpawn; i++)
            {
                //instantiate far off screen
                CharacterSheetController character = Instantiate(characterPrefab, 
                    new Vector3(offScreenDefault_x, offScreenDefault_y, 0f), Quaternion.identity);
                if (character != null) {
                    character.SetSpawnPoint(this);
                    characters.Add(character);
                    //spawnQueue.Enqueue(character);
                    
                }
            }
            spawnTimer = 0f;
        }

        private void Update()
        {
            
            if (spawnTimer <= 0f)
            {
                if (spawnQueue.Count > 0)   //at least one character is staged
                {
                    //check for a clear spawning zone.  this implies blocking spawns as a strategy
                    if (spawningZoneCollider.OverlapCollider(contactFilter, collidersInSpawnZone) == 0)
                    {
                        Spawn();
                        spawnTimer = spawnWaitPeriod;   //reset spawn timer
                    } else
                    {
                        //Debug.Log(string.Format("{0}'s spawn zone is blocked!", gameObject.name));
                    }
                }
            } else { 
                spawnTimer -= Time.deltaTime; 
            }
        }

        //add all characters in SpawnPoint's list to the queue
        //usually called by the GameController
        public override void QueueAll()
        {
            foreach(var character in characters)
            {
                QueueSpawn(character);
            }
        }


        public override void QueueSpawn(CharacterSheetController character)
        {
            spawnQueue.Enqueue(character);
        }

        public override void Spawn()
        {
            if (spawnQueue.Count > 0)
            {
                spawnQueue.Dequeue().CharacterSpawn(transform.position);
            }
        }
    }

    public abstract class SpawnPoint : MonoBehaviour
    {
        [SerializeField]
        protected CharacterSheetController characterPrefab;
        protected int CharactersToSpawn;    //# of characters this spawnpoint will spawn
        public abstract void QueueSpawn(CharacterSheetController character);
        public abstract void QueueAll();
        public abstract void Spawn();
    }
}