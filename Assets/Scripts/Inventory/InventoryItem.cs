using MarblesAndMonsters.Characters;
using System.Collections;
using UnityEngine;

namespace MarblesAndMonsters.Items
{
    public abstract class InventoryItem: MonoBehaviour
    {
        protected SpriteRenderer spriteRenderer;
        protected Animator animator;
        protected AudioSource audioSource;
        protected int aTriggerPickup;

        //[SerializeField]
        //protected Sprite InventoryIcon;
        public ItemStatsBase ItemStats;

        protected void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
            aTriggerPickup = Animator.StringToHash("Pickup");
        }

        public virtual void Reset()
        {
            gameObject.SetActive(true);
        }

        //public virtual Sprite GetSprite()
        //{
        //    throw new System.NotImplementedException();
        //}

        public virtual Sprite GetUISprite()
        {
            return ItemStats.InventoryIcon;
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            //only Player objects can pickup and use 
            if (collision.gameObject.CompareTag("Player"))
            {
                if (Player.Instance != null)
                {
                    Debug.Log(string.Format("InventoryItem {0} has been picked up by player!", gameObject.name));
                    Player.Instance.AddItemToInventory(ItemStats);
                    StartCoroutine(PickupItem());
                }
            }
        }

        private IEnumerator PickupItem()
        {
            yield return new WaitForSeconds(0.1f);
            gameObject.SetActive(false);
        }
    }
}
