using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters;

namespace MarblesAndMonsters
{
    public class MummyController : MoveTowardPlayer
    {

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other != null)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    Debug.Log("Player has entered Mummy's trigger collider!");
                    isAwake = true;
                    var collider = this.gameObject.GetComponent<BoxCollider2D>();
                    collider.enabled = false;
                }
            }
        }
    }
}
