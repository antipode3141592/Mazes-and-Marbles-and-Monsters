using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.Characters
{
    //Mummys are spawned from their sarcophagus when the Player enters the trigger area
    //Mummy does not respawn (but is reset upon player death 
    public class SpawnPoint_Mummy : SpawnPoint
    {
        private ParticleSystem triggerParticles;
        [SerializeField]
        private Collider2D triggerCollider;

        protected override void Awake()
        {
            base.Awake();
            triggerParticles = GetComponentInChildren<ParticleSystem>();
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && !collision.isTrigger)
            {
                animator.SetTrigger("Open");
                triggerCollider.enabled = false;
                triggerParticles.Stop();
                StartCoroutine(Open(.25f));
            }
        }

        public override void Reset()
        {
            base.Reset();
            animator.SetTrigger("Close");
            triggerCollider.enabled = true;
            triggerParticles.Play();
        }

        private IEnumerator Open(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            SpawnCharacter();
        }
    }
}