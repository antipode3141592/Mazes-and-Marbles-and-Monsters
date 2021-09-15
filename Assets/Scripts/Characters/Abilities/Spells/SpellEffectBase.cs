using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Characters;

namespace MarblesAndMonsters.Spells
{
    public abstract class SpellEffectBase : MonoBehaviour
    {
        public Animator Animator;

        protected CharacterControl _caster;

        protected virtual void Awake()
        {
            Animator = GetComponent<Animator>();
        }

        internal virtual void SetCaster(CharacterControl characterControl)
        {
            _caster = characterControl;
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