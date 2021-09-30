using MarblesAndMonsters.States.GameStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace MarblesAndMonsters.Characters
{
    public class SpawnPoint: MonoBehaviour
    {
        //configuration
        [SerializeField]
        protected CharacterControl characterPrefab;
        [SerializeField]
        protected int simultaneousCharacters = 1;   //# of characters that can be active at a time (typically 1)
        [SerializeField]
        protected Vector2 spawnOffset;  //offset the spawn location, most useful for spawns on walls

        //private Queue<CharacterSheetController> spawnQueue; //ephemeral 
        protected List<CharacterControl> characters;  //the collection of all instantiated character objects

        private float spawnTimer;
        [SerializeField]
        public bool isAvailable = true;   //true when the SpawnPoint's collider is clear and ready to start a spawn
        
        protected Animator animator;
        protected int spawnTriggerHash;
        [SerializeField]
        protected float spawnAnimationDelay;

        GameManager _gameManager;

        #region Unity Scripts

        protected virtual void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>(true);
            characters = new List<CharacterControl>();
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
            }
            return character;
        }
        #endregion

        public virtual void RemoteTriggerSpawn(float spawnDelay)
        {
            StartCoroutine(Spawn(spawnDelay));
        }

        public virtual IEnumerator Spawn(float spawnDelay)
        {
            yield return new WaitForSeconds(spawnDelay);
            //if 
            //if (_gameManager.CurrentState.GetType().is
            //{
                SpawnCharacter();
            //}
        }

        /// <summary>
        /// Trigger the spawn animation (if available) and 
        /// </summary>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        public virtual void SpawnCharacter()
        {
            if (isAvailable)
            {
                if (animator)
                {
                    animator.SetTrigger(spawnTriggerHash);
                }
                StartCoroutine(SpawnAnimation());
            }
        }

        /// <summary>
        /// If not enough stored characters exist, instantiate a 
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerator SpawnAnimation()
        {
            characters.RemoveAll(x => x == null);
            yield return new WaitForSeconds(spawnAnimationDelay);     //includes 0.5 sec ready blinking and 0.25 sec opening
            if (characters.Count < simultaneousCharacters)
            {
                CharacterControl _char = InstantiateCharacter();
                AfterSpawn(_char);
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
                if (_char != null)
                {
                    Destroy(_char.gameObject, 0.01f);
                }
            }
            characters.Clear();
        }
    }
}