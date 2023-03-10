using LevelManagement.DataPersistence;
using MarblesAndMonsters.Characters;
using MoreMountains.Feedbacks;
using System.Collections;
using UnityEngine;

namespace MarblesAndMonsters.Spells
{
    public abstract class SpellPickupBase : MonoBehaviour
    {
        protected SpriteRenderer spriteRenderer;
        protected Animator animator;
        protected int aTriggerPickup;

        protected MMFeedbacks pickupFeedbacks;

        public SpellStatsBase Stats;

        protected void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            pickupFeedbacks = GetComponent<MMFeedbacks>();
            aTriggerPickup = Animator.StringToHash("Pickup");
        }

        public virtual void Reset()
        {
            if (Player.Instance)
                Player.Instance.MySheet.Spells[Stats.SpellName].IsUnlocked = false;
            gameObject.SetActive(true);
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            //only Player objects can pickup and use 
            if (collision.gameObject.CompareTag("Player"))
            {
                if (Player.Instance != null)
                {
                    Debug.Log(string.Format("InventoryItem {0} has been picked up by player!", gameObject.name));
                    Player.Instance.MySheet.Spells[Stats.SpellName].IsUnlocked = true;
                    Player.Instance.AddtoActiveSpells(Stats);
                    StartCoroutine(PickupItem());
                }
            }
        }

        IEnumerator PickupItem()
        {
            pickupFeedbacks.PlayFeedbacks();
            yield return new WaitForSeconds(0.1f);
            gameObject.SetActive(false);
        }
    }
}