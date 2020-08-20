using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Characters;

namespace MarblesAndMonsters.Items
{
    public class HealthPotion : MonoBehaviour
    {
        [SerializeField]
        private int strength;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //only Player objects can pickup and use 
            if (collision.gameObject.CompareTag("Player"))
            {
                if (Player.Instance != null)
                {
                    //check for damage
                    if (Player.Instance.MySheet.CurrentHealth < Player.Instance.MySheet.MaxHealth)
                    {
                        Player.Instance.AdjustHealth(strength);
                        Destroy(gameObject);    //destroy self (these are relatively rare, so no need for pooling)
                    }
                    else
                    {
                        //nothing
                    }
                }
            }
        }
    }
}