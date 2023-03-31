using System;
using System.Collections;
using UnityEngine;

namespace MarblesAndMonsters.Spells
{
    public abstract class SpellEffectBase : MonoBehaviour
    {
        public Animator Animator;
        public Guid CasterGuid;

        protected virtual void Awake()
        {
            Animator = GetComponent<Animator>();
        }

        /// <summary>
        /// How long does this object get to live?  You decide!
        /// </summary>
        /// <param name="duration"></param>
        internal virtual void SetDeathTime(float duration)
        {
            StartCoroutine(TimeToDie(duration));
        }

        public virtual IEnumerator TimeToDie(float deathDelay)
        {
            yield return new WaitForSeconds(deathDelay);
            EndEffect();
        }

        public virtual void EndEffect()
        {
            if (Animator)
            {
                Animator.SetTrigger("EndEffect");
            }
            StartCoroutine(EndEffectCleanup(0.5f));
        }

        public virtual IEnumerator EndEffectCleanup(float cleanupDelay)
        {
            yield return new WaitForSeconds(cleanupDelay);
            Destroy(gameObject);
        }
    }
}