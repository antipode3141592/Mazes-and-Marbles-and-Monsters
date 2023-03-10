using MarblesAndMonsters.Characters;
using MoreMountains.Feedbacks;
using System.Collections;
using UnityEngine;

namespace MarblesAndMonsters.Items
{

    public class KeyItem : MonoBehaviour
    {
        public KeyStats KeyStats;
        protected MMFeedbacks pickupFeedbacks;

        private void Awake()
        {
            pickupFeedbacks = GetComponent<MMFeedbacks>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other != null && other.gameObject.CompareTag("Player"))
            {
                //add key to player's 
                Player.Instance.AddToKeyChain(this);
                StartCoroutine(PickupKey());
            }
        }

        IEnumerator PickupKey()
        {
            pickupFeedbacks.PlayFeedbacks();
            yield return new WaitForSeconds(0.1f);
            gameObject.SetActive(false);
        }

        public void Reset()
        {
            gameObject.SetActive(true);
        }

    }
}
