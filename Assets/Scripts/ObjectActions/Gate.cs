using MarblesAndMonsters.Characters;
using MarblesAndMonsters.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.Tiles
{
    public class Gate : MonoBehaviour, ILockable
    {
        [SerializeField]
        protected GateData gateData;

        private Animator animator;
        private List<Collider2D> collider2Ds;
        private AudioSource audioSource;

        private int aIsLocked;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            collider2Ds = new List<Collider2D>(GetComponents<Collider2D>());
            audioSource = GetComponent<AudioSource>();
            aIsLocked = Animator.StringToHash("isLocked");
        }

        public bool Lock()
        {
            StartCoroutine(CloseAnimation());
            return true;
        }

        public bool Unlock(KeyItem testKey)
        {
            if (gateData.requiredKey == testKey.KeyStats.KeyType || testKey.KeyStats.KeyType == KeyType.Skeleton)
            {
                return true;
            }
            return false;
        }



        private void OnTriggerEnter2D(Collider2D other)
        {
            //only a Player can open the gate
            if (other.CompareTag("Player"))
            {
                //play key insertion sound
                foreach (KeyItem keyTest in Player.Instance.KeyChain)
                {
                    if (Unlock(keyTest))
                    {
                        //play gate opening sound

                        //remove key from player inventory
                        Player.Instance.RemoveKeyFromKeyChain(keyTest);

                        //start unlock animator
                        StartCoroutine(OpenAnimation());
                    }
                }
            }
        }

        private IEnumerator OpenAnimation()
        {
            animator.SetBool(aIsLocked, false);
            
            yield return new WaitForSeconds(0.5f);

            foreach(var collider in collider2Ds) { collider.enabled = false; }
        }

        private IEnumerator CloseAnimation()
        {
            animator.SetBool(aIsLocked, true);
            yield return new WaitForSeconds(0.5f);

            foreach (var collider in collider2Ds) { collider.enabled = true; }
        }
    }
}