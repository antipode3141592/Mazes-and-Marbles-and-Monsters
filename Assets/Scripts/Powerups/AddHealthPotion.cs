using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Characters;

namespace MarblesAndMonsters.Items
{
    public class AddHealthPotion : MonoBehaviour
    {
        [SerializeField]
        private int strength;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Player.Instance.AddMaxHealth(strength);
                Destroy(gameObject);    //destroy self (these are relatively rare, so no need for pooling)
            }
        }
    }
}