using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters;

public class Roller : BoardMovable<Roller>
{
    [SerializeField]
    private Vector2 orientation; //rollers only move along one axis.  probably only vertical and horizontal, but for now keep it open to any vector
    //public override void Move(Vector2 force, float forceMultiplier)
    //{
    //    Rigidbody2D rigidbody2D = this.GetComponent<Rigidbody2D>();
    //    Vector2 projectedForce = (force * orientation.normalized) * orientation.normalized;
    //    //rigidbody2D.velocity = rigidbody2D.velocity 
    //    rigidbody2D.AddForce(projectedForce * rigidbody2D.mass * forceMultiplier);
    //}
}
