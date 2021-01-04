using UnityEngine;

namespace MarblesAndMonsters.Items
{
    public abstract class InventoryItem: MonoBehaviour
    {
        protected SpriteRenderer spriteRenderer;
        protected Animator animator;
        protected AudioSource audioSource;

        //[SerializeField]
        //protected Sprite InventoryIcon;
        public ItemStats ItemStats;

        protected void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
        }

        public virtual void Reset()
        {
            gameObject.SetActive(true);
        }

        public virtual Sprite GetSprite()
        {
            throw new System.NotImplementedException();
        }

        public virtual Sprite GetUISprite()
        {
            return ItemStats.InventoryIcon;
        }
    }
}
