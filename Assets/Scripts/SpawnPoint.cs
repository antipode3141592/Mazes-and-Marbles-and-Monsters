using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.Characters
{
    public abstract class SpawnPoint<T> : SpawnPoint where T : SpawnPoint<T>
    {
        private Queue<CharacterSheetController> spawnQueue;
        private float spawnWaitPeriod = 1.5f;
        private float spawnTimer;

        private void Awake()
        {
            spawnQueue = new Queue<CharacterSheetController>();
        }

        private void Update()
        {
            
            if (spawnTimer <= 0f)
            {
                if (spawnQueue.Count > 0)
                {
                    Spawn();
                    spawnTimer = spawnWaitPeriod;   //reset spawn timer
                }
            } else { 
                spawnTimer -= Time.deltaTime; 
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
        public abstract void QueueSpawn(CharacterSheetController character);

        public abstract void Spawn();
    }
}