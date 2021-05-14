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

        //private GateController gateController;
        private Animator animator;
        private List<Collider2D> collider2Ds;

        private int aIsLocked;

        private void Awake()
        {
            //gateController = FindObjectOfType<GateController>();
            animator = GetComponent<Animator>();
            collider2Ds = new List<Collider2D>(GetComponents<Collider2D>());
            aIsLocked = Animator.StringToHash("isLocked");
        }

        public bool Lock()
        {
            StartCoroutine(CloseAnimation());
            return true;
        }

        public bool Unlock()
        {
            foreach (KeyStats keyTest in Player.Instance.Inventory)
            {
                if (gateData.requiredKey == keyTest.KeyType || keyTest.KeyType == KeyType.Skeleton)
                {
                    return true;
                }
            }
            return false;
        }



        private void OnTriggerEnter2D(Collider2D other)
        {
            //only a Player can open the gate
            if (other.CompareTag("Player"))
            {
                foreach (KeyStats keyTest in Player.Instance.Inventory)
                {
                    if (Unlock())
                    {
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