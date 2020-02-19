using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    ////Rigidbody2D rigidbody2D;
    //GameController gameController;

    //public int maxHealth = 5;
    //int currentHealth = 3;

    //public float timeInvincible = 2.0f;
    //bool isInvincible;
    //float invincibleTimer;

    ////Public Properties
    //public int health { get { return currentHealth; } }

    //// Start is called before the first frame update
    //void Start()
    //{
    //    //initialize health to max health
    //    currentHealth = maxHealth;
    //    //rigidbody2D = GetComponent<Rigidbody2D>();
    //    gameController = FindObjectOfType<GameController>();
    //}

    //private void Update()
    //{
    //    //invincibleUpdate();
    //    //DeathCheck();
    //}

    //public void DeathCheck()
    //{
    //    if (currentHealth <= 0) 
    //    {
    //        gameController.DestroyPlayer();
    //    }
    //}

    //public void ChangeHealth(int amount)
    //{
    //    if (amount < 0)
    //    {
            
    //        if (!isInvincible)
    //        {
    //            //healthEffect.Play(); //play "hit" particle effect
    //            //animator.SetTrigger("Hit");
    //            //PlaySound(hitSound);
    //            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    //            Debug.Log("Player says: Ouch!  (" + currentHealth + ")");
    //            isInvincible = true;
    //            invincibleTimer = timeInvincible;
    //        }
    //    }
    //    else
    //    {
    //        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    //    }
    //}

    //private void invincibleUpdate()
    //{
    //    if (isInvincible)
    //    {
    //        invincibleTimer -= Time.deltaTime;
    //        if (invincibleTimer <= 0.0f)
    //        {
    //            isInvincible = false;
    //        }
    //    }
    //}
}
