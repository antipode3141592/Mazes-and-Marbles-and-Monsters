﻿using System.Collections;
using UnityEngine;

namespace MarblesAndMonsters.Characters
{
    public class Marble : CharacterControl
    {
        protected override void Awake()
        {
            base.Awake();

        }
        //mables apply a touch attack to everything they collide with

        private void OnCollisionEnter2D(Collision2D collision)
        {
            audioSource.clip = MySheet.baseStats.ClipHit;
            audioSource.Play(); //no matter what is struck, play the hit sound
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (TouchAttackIsAvailable)
            {
                IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
                if (damagable != null)
                {
                    DealDamageTo(damagable);
                    StartCoroutine(TouchAttackCooldown());
                }
            }
        }

        protected override IEnumerator DeathAnimation(DeathType deathType)
        {
            yield return new WaitForSeconds(0.5f);  //death animations are 6 frames, current fps is 12
            Destroy(gameObject);
        }
    }
}
