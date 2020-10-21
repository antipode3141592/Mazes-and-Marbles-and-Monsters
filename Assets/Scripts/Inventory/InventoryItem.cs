using UnityEngine;

namespace MarblesAndMonsters.Items
{
    public abstract class InventoryItem<T> : InventoryItem where T : InventoryItem<T>
    {
        protected void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        public override void Reset()
        {
            gameObject.SetActive(true);
        }

        public override Sprite GetSprite()
        {
            throw new System.NotImplementedException();
        }

        public override Sprite GetUISprite()
        {
            return spriteRenderer.sprite;
        }
    }

    public abstract class InventoryItem: MonoBehaviour
    {
        protected SpriteRenderer spriteRenderer;
        protected Animator animator;

        public abstract Sprite GetSprite();     //return default sprite (typically one of the idle animations)
        public abstract Sprite GetUISprite();
        public abstract void Reset();
    }
}
