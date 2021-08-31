using MarblesAndMonsters.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters;
using MarblesAndMonsters.Spells;
using System;

public class VineElemental : CharacterControl
{
    [SerializeField]
    protected ContactFilter2D contactFilter;    //setup in Editor
    protected float VisibilityDistance;
    //[SerializeField]
    //protected float ProjectileRestPeriod = 1.0f;
    //protected bool RangedAttackIsAvailable = true;
    //[SerializeField]
    //protected float TouchAttackRestPeriod = 1.0f;
    //protected bool TouchAttackIsAvailable = true;
    public EntangleProjectile projectilePrefab;

    protected override void Awake()
    {
        base.Awake();
        //VisibilityDistance = 
    }

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (RangedAttackIsAvailable)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log(string.Format("{0} senses the Player, raycasting...  ", name));
                Vector2 origin = (Vector2)transform.position;
                Vector2 direction = (Vector2)(collision.transform.position - transform.position).normalized;
                float distance = (collision.transform.position - transform.position).magnitude;
                List<RaycastHit2D> hits = new List<RaycastHit2D>();
                //contact filter limits to NPC and Wall layers
                int results = Physics2D.Raycast(origin, direction, contactFilter, hits, distance);
                //if any results, do not fire, as something is in the way
                if (results > 0)
                {
                    Debug.Log(string.Format("{0} senses the player but does not have line of sight", name));
                }
                else
                {
                    //fire!
                    RangedAttackIsAvailable = false;
                    StartCoroutine(FireProjectile(0.33f, direction));
                    StartCoroutine(ProjectileCooldown());
                }
            }
        }
    }



    IEnumerator FireProjectile(float attackDelay, Vector2 direction)
    {
        Debug.Log("Fire!");
        //start attack animation
        yield return new WaitForSeconds(attackDelay);
        EntangleProjectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.Caster = this;
        projectile.Rigidbody2D.velocity = 5.0f * direction;

    }
}
