using MarblesAndMonsters.Characters;
using System.Collections;
using UnityEngine;

namespace MarblesAndMonsters.Items
{
    public class TreasurePickup : InventoryItem
    {
        [SerializeField]
        private int value = 1;
        public override void Reset()
        {
            base.Reset();
            //Player.Instance.RemoveTreasure(value);
        }

        //private void OnCollisionEnter2D(Collision2D other)
        //{
        //    Player.Instance.AddTreasure(value);
        //    gameObject.SetActive(false);
        //}

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            Player.Instance.AddTreasure(value);
            animator.SetTrigger(aTriggerPickup);
            audioSource.clip = ItemStats.ClipPickup;
            audioSource.Play();
            StartCoroutine(PickupScroll());
            
        }

        private IEnumerator PickupScroll()
        {
            yield return new WaitForSeconds(0.5f);
            gameObject.SetActive(false);
        }
    }
}