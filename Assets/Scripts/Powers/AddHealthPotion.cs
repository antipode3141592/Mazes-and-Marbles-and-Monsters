using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Characters;

namespace MarblesAndMonsters.Items
{

    //Add Health Potion - immediately consumed, add to max health and heal all damage
    public class AddHealthPotion : InventoryItem
    {
        [SerializeField]
        private AddHealthItemStats healthItemStats;
        


        //potion is consumed immediately (it would feel weird to have to activate it) 
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                audioSource.Play();
                //healthItemStats.Action();
                //Destroy(gameObject);    //destroy self (these are relatively rare, so no need for pooling)
                //gameObject.SetActive(false);
                
                StartCoroutine(HideItem());
            }
        }

        public IEnumerator HideItem()
        {
            yield return new WaitForSeconds(0.2f);
            gameObject.SetActive(false);
        }

        public override void Reset()
        {
            if (!gameObject.activeInHierarchy)
            {
                base.Reset();
                Player.Instance.AddMaxHealth(-healthItemStats.HealingStrength);
            }
        }
    }
}