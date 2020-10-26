using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  an object pooler attached to a transform
//      Spawns characters in queue at specified interval
namespace MarblesAndMonsters.Characters
{
    public abstract class SpawnPoint: MonoBehaviour
    {
        //configuration
        [SerializeField]
        protected CharacterSheetController characterPrefab;
        protected int CharactersToSpawn;    //# of characters this spawnpoint will spawn
        [SerializeField]
        protected int charactersToSpawn = 1;    //# of characters this spawnpoint will spawn

        //private Queue<CharacterSheetController> spawnQueue; //ephemeral 
        private List<CharacterSheetController> characters;  //the collection of all instantiated character objects
        
        private float spawnTimer;
        private bool isAvailable = true;   //true when the SpawnPoint's collider is clear and ready to start a spawn
        protected ContactFilter2D contactFilter;
        protected List<Collider2D> collidersInSpawnZone;

        protected Collider2D spawningZoneCollider;  //trigger area where the character will spawn
                                                    //will only allow a spawn while the spawning zone is clear


        #region Unity Scripts

        protected virtual void Awake()
        {
            contactFilter.NoFilter();
            spawningZoneCollider = gameObject.GetComponent<Collider2D>();
            characters = new List<CharacterSheetController>();
            collidersInSpawnZone = new List<Collider2D>();
        }

        protected virtual void Start()
        {
            //instantiate required characters, add them to Characters list, and disable them
            for (int i = 0; i < charactersToSpawn; i++)
            {
                CharacterSheetController character = Instantiate(characterPrefab, transform.position, Quaternion.identity);
                if (character != null)
                {
                    character.SetSpawnPoint(this);
                    characters.Add(character);
                    if (i > 0)
                    {
                        character.gameObject.SetActive(false);
                    }
                }
            }
        }
        #endregion

        public virtual void RemoteTriggerSpawn(float spawnDelay)
        {
            StartCoroutine(Spawn(spawnDelay));
        }

        public virtual IEnumerator Spawn(float spawnDelay)
        {
            yield return new WaitForSeconds(spawnDelay);
            SpawnCharacter();
        }

        public virtual void SpawnCharacter()
        {
            //most of these spawnpoints will only have a handful of characters assigned, so a foreach should be fine
            foreach (var _char in characters)
            {
                //find the first available inactive character, activate it, then exit the loop
                if (!_char.isActiveAndEnabled)
                {
                    _char.gameObject.SetActive(true);
                    _char.transform.position = transform.position;
                    //last stored rotation is sufficient
                    break;
                }
            }
        }
    }

    //public abstract class SpawnPoint : MonoBehaviour
    //{
        

    //    public abstract void RemoteTriggerSpawn(float spawnDelay);
    //    public abstract IEnumerator Spawn(float spawnDelay);
    //    public abstract void SpawnCharacter();
    //}
}