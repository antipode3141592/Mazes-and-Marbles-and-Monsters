using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    public float forceMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //invincible check

        //movement check
        MoveIt();

        //hit check
        
    }

    private void MoveIt()
    {
        //float x_acc = 0.0f;  //
        //float y_acc = 0.0f;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);

        //if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        //{
        //    lookDirection.Set(move.x, move.y);
        //    lookDirection.Normalize();
        //}

        //animator.SetFloat("Look X", lookDirection.x);
        //animator.SetFloat("Look Y", lookDirection.y);
        //animator.SetFloat("Speed", move.magnitude);

        //float _speed = speed * isRunning() * Time.deltaTime;
        //Vector2 position = rigidbody2D.position;

        //position.x += horizontal* _speed;
        //position.y += vertical* _speed;
        //position = position + move * _speed;
        //rigidbody2D.MovePosition(position);
        //Debug.Log("float force applied!");
        rigidbody2D.AddForce(move* forceMultiplier);
    }
}
