using MarblesAndMonsters.Characters;
using MoreMountains.Feedbacks;
using System.Collections;
using UnityEngine;

namespace MarblesAndMonsters.Items
{
    public abstract class InventoryItem: MonoBehaviour
    {
        protected SpriteRenderer spriteRenderer;
        protected Animator animator;
        protected MMFeedbacks pickupFeedbacks;
        protected int aTriggerPickup;
        [SerializeField] protected float pickupDelay = 0.1f;

        public ItemStats Stats;

        protected void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            pickupFeedbacks = GetComponent<MMFeedbacks>();
            aTriggerPickup = Animator.StringToHash("Pickup");
        }

        public virtual void Reset()
        {
            gameObject.SetActive(true);
        }

        public virtual Sprite GetUISprite()
        {
            return Stats.InventoryIcon;
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            //only Player objects can pickup and use 
            if (collision.gameObject.CompareTag("Player"))
            {
                if (Player.Instance != null)
                {
                    Debug.Log(string.Format("InventoryItem {0} has been picked up by player!", gameObject.name));
                    Player.Instance.AddItemToInventory(Stats);
                    StartCoroutine(PickupItem(pickupDelay));
                }
            }
        }

        protected IEnumerator PickupItem(float pickupDelay)
        {
            pickupFeedbacks.PlayFeedbacks();
            yield return new WaitForSeconds(pickupDelay);
            gameObject.SetActive(false);
        }
    }
}
