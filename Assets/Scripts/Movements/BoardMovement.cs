using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters
{
    public static class BoardMovement
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rb">Rigidbody2D to apply force to</param>
        /// <param name="modifier">some characters can vary the influence of the board by modifying htis parameter</param>
        public static void Move(Rigidbody2D rb, Vector2 direction, float modifier = 1.0f) {
            rb.AddForce(direction * Gravity.Acceleration * rb.mass * modifier);
        }
    }
}