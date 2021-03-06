﻿using UnityEngine;

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
