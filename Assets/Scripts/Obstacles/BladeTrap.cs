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
        protected int aTriggerisSprung;

        protected Animator animator;
        
        protected override void Awake()
        {
            base.Awake();
            contactFilter.NoFilter();
            animator = GetComponent<Animator>();
            aTriggerisSprung = Animator.StringToHash("isSprung");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //when blade trap switch is triggered, an explosive/impulse force is applied to all objects within the blade range
            if (other != null && isReady == true && !other.isTrigger)
            {
                isReady = false;
                animator.SetTrigger(aTriggerisSprung);
                StartCoroutine(SpringTrap(other));
            }
        }

        private IEnumerator SpringTrap(Collider2D other)
        {
            yield return new WaitForSeconds(attackDelay);
            if (other != null)
            { 
                other.attachedRigidbody.AddForce(explosiveForce * (Vector2)(other.transform.position - transform.position).normalized, ForceMode2D.Impulse); 
            }
            StartCoroutine(TrapReset());
        }

        private IEnumerator TrapReset()
        {
            yield return new WaitForSeconds(readyTimerDelay);
            isReady = true;
        }
    }
}