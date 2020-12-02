using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.Characters
{
    //Mummys are spawned from their sarcophagus when the Player enters the trigger area
    //Mummy does not respawn (but is reset upon player death 
    public class SpawnPoint_Mummy : SpawnPoint
    {
        

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                animator.SetTrigger("Open");
                StartCoroutine(Open(.25f));
            }
        }

        public override void Reset()
        {
            base.Reset();
            animator.SetTrigger("Close");
        }

        private IEnumerator Open(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            SpawnCharacter();
        }
    }
}