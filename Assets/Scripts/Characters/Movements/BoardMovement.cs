using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters
{
    public static class BoardMovement
    {
        public static float Acceleration = 9.816f;
        /// <summary>
        /// Simulates gravity effects on a tilting game board.
        /// Direction can come from any analog input source, typically the device's accelerometer
        /// </summary>
        /// <param name="rb">apply force to Rigidbody2D, proportional to its mass</param>
        /// <param name="direction">Normalized direction of force</param>
        /// <param name="characterModifier">scaling value for abilities/effects</param>
        /// <param name="sensitivity">sensitivity </param>
        public static void Move(Rigidbody2D rb, Vector2 direction, float characterModifier = 1.0f, float sensitivity = 1.0f) {
            rb.AddForce(direction * Acceleration * rb.mass * characterModifier * sensitivity);
        }
    }
}