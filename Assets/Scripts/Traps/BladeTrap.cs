using MarblesAndMonsters.Characters;
using System.Collections;
using UnityEngine;

namespace MarblesAndMonsters.Objects
{
    public class BladeTrap : Characters.CharacterControl
    {
        [SerializeField]
        private float explosiveForce = 100f;
        [SerializeField]
        private float attackDelay = 0.2f;
        ContactFilter2D contactFilter;
        [SerializeField]
        private bool isReady = true;
        [SerializeField]
        private float readyTimerDelay = 0.6f;


        //Animator animator;
        
        protected override void Awake()
        {
            base.Awake();
            contactFilter.NoFilter();
            animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //when blade trap switch is triggered, an explosive/impulse force is applied to all objects within the blade range
            if (other != null && isReady == true)
            {
                isReady = false;
                //animator
                animator.SetTrigger("isSprung");
                //Debug.Log(string.Format("You've activated my trap, {0}!", gameObject.name));
                StartCoroutine(SpringTrap(other));
                //Explosion2D((Vector2)transform.position, explosiveForce, 0.4641175f);
            }
        }

        private IEnumerator SpringTrap(Collider2D other)
        {
            yield return new WaitForSeconds(attackDelay);
            other.attachedRigidbody.AddForce(explosiveForce * (Vector2)(other.transform.position - transform.position).normalized, ForceMode2D.Impulse);
            StartCoroutine(TrapReset());
        }

        // removed because OverlapCircle is looking for full overlap/enclosure, not partial, and characters weren't fitting in the
        //  trigger zone
        //private void Explosion2D(Vector2 explosionOrigin, float force, float radius)
        //{
        //    Collider2D[] objectsInRange = { };
        //    int objectCount = Physics2D.OverlapCircle(explosionOrigin, radius, contactFilter, objectsInRange);
        //    if (objectCount > 0) {
        //        foreach (Collider2D other in objectsInRange)
        //        {
        //            //calculate normal vector between trap and object
        //            var direction = ((Vector2)other.transform.position - explosionOrigin).normalized;
        //            //apply impulse force
        //            Debug.Log(string.Format("Explosive force {0} applied to {1}", force * direction, other.name));
        //            other.attachedRigidbody.AddForce(force * direction, ForceMode2D.Impulse);
        //        }
        //    }
        //}

        private IEnumerator TrapReset()
        {
            //Debug.Log("Resetting trap!");
            yield return new WaitForSeconds(readyTimerDelay);
            isReady = true;
        }
    }
}