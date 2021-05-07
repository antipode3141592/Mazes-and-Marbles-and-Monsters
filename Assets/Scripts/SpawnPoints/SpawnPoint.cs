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
        protected CharacterControl characterPrefab;
        protected int CharactersToSpawn;    //# of characters this spawnpoint will spawn
        [SerializeField]
        protected int storedCharacters = 1;    //# of characters this spawnpoint will maintain
        [SerializeField]
        protected int simultaneousCharacters = 1;   //# of characters that can be active at a time (typically 1)
        [SerializeField]
        protected Vector2 spawnOffset;  //offset the spawn location, most useful for spawns on walls

        //private Queue<CharacterSheetController> spawnQueue; //ephemeral 
        protected List<CharacterControl> characters;  //the collection of all instantiated character objects

        private float spawnTimer;
        [SerializeField]
        public bool isAvailable = true;   //true when the SpawnPoint's collider is clear and ready to start a spawn
        protected ContactFilter2D contactFilter;
        protected List<Collider2D> collidersInSpawnZone;

        protected Animator animator;
        protected int spawnTriggerHash;
        [SerializeField]
        protected float spawnAnimationDelay;

        protected Collider2D spawningZoneCollider;  //trigger area where the character will spawn
                                                    //will only allow a spawn while the spawning zone is clear

        #region Unity Scripts

        protected virtual void Awake()
        {
            contactFilter.NoFilter();
            spawningZoneCollider = gameObject.GetComponent<Collider2D>();
            characters = new List<CharacterControl>();
            collidersInSpawnZone = new List<Collider2D>();
            animator = GetComponent<Animator>();
            spawnTriggerHash = Animator.StringToHash("Spawn");
        }

        /// <summary>
        /// Instantiate an object with an added positional offset (default 0,0,0), with no parent object (Collisions propagate upward to parents)
        /// </summary>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        private CharacterControl InstantiateCharacter()
        {
            CharacterControl character = Instantiate(characterPrefab, transform.position + (Vector3)spawnOffset, Quaternion.identity, null);
            if (character != null)
            {
                character.SetSpawnPoint(this);
                characters.Add(character);
                //if (i > 0 || !isAvailable)
                //{
                //    character.gameObject.SetActive(false);
                //}
            }
            return character;
        }
        #endregion

        public virtual void RemoteTriggerSpawn(float spawnDelay)
        {
            Debug.Log(string.Format("RemoteTriggerSpawn({0}) called", spawnDelay));
            StartCoroutine(Spawn(spawnDelay));
        }

        public virtual IEnumerator Spawn(float spawnDelay)
        {
            yield return new WaitForSeconds(spawnDelay);
            SpawnCharacter();
        }

        /// <summary>
        /// Trigger the spawn animation (if available) and 
        /// </summary>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        public virtual void SpawnCharacter()
        {
            if (animator)
            {
                animator.SetTrigger(spawnTriggerHash);
            }
            StartCoroutine(SpawnAnimation());
        }

        /// <summary>
        /// If not enough stored characters exist, instantiate a 
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerator SpawnAnimation()
        {            
            yield return new WaitForSeconds(spawnAnimationDelay);     //includes 0.5 sec ready blinking and 0.25 sec opening
            if (characters.Count < storedCharacters)
            {
                CharacterControl _char = InstantiateCharacter();
                AfterSpawn(_char);
            }
            else
            {
                //most of these spawnpoints will only have a handful of characters assigned, so a foreach should be fine
                foreach (var _char in characters)
                {
                    //find the first available inactive character, activate it, then exit the loop
                    if (!_char.isActiveAndEnabled)
                    {
                        _char.gameObject.SetActive(true);
                        _char.transform.position = transform.position + (Vector3)spawnOffset;
                        //last stored rotation is sufficient
                        AfterSpawn(_char);
                    }
                }
            }            
        }

        /// <summary>
        /// override to do something to the character after spawning
        /// </summary>
        /// <param name="character"></param>
        protected virtual void AfterSpawn(CharacterControl character)
        {
            Debug.Log(string.Format("{0} has been spawned!", character.name));
        }

        /// <summary>
        /// Disable each active game object in the characters list
        /// </summary>
        public virtual void Reset()
        {
            foreach (var _char in characters)
            {
                if (_char.isActiveAndEnabled)
                {
                    _char.gameObject.SetActive(false);
                }
            }
        }
    }
}