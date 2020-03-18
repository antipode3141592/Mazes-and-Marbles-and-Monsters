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
                    isAwake = true;
                }
            }
        }
    }
}
