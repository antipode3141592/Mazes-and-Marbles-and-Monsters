using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Characters;

namespace MarblesAndMonsters.Spells
{
    public abstract class SpellPickupBase : MonoBehaviour
    {
        protected SpriteRenderer spriteRenderer;
        protected Animator animator;
        protected AudioSource audioSource;
        protected int aTriggerPickup;

        protected SpellStats _oldStats;

        //[SerializeField]
        //protected Sprite InventoryIcon;
        public SpellStats Stats;

        protected void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
            aTriggerPickup = Animator.StringToHash("Pickup");
        }

        public virtual void Reset()
        {
            if (Player.Instance)
            {
                Player.Instance.MySheet.Spells[Stats.SpellName].IsUnlocked = false;
                //if (_oldStats != null)
                //{
                //    Player.Instance.MySheet.Spells[Stats.SpellName].SpellStats = _oldStats;
                //}
            }
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
                    //_oldStats = Player.Instance.MySheet.Spells[Stats.SpellName].SpellStats;
                    //Player.Instance.MySheet.Spells[Stats.SpellName].SpellStats = Stats;
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