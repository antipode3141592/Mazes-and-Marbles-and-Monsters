using System;
using MarblesAndMonsters.Events;
using MarblesAndMonsters.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters
{
    /// <summary>
    /// The spell types
    /// </summary>
    public enum SpellName { Levitate, ForceBubble, TimeSlow, Transmute, Entangle, StoneForm, ForcePush, Teleport, GhostForm }

    public class SpellEventArgs : EventArgs
    {
        public SpellStats SpellStats;

        public SpellEventArgs(SpellStats spellStats)
        {
            SpellStats = spellStats;
        }
    }

    /// <summary>
    /// Spells are objects that must be attached to an object with a CharacterControl.
    ///     - 
    ///     - CharacterSheet object creates a dictionary of attached spells, indexed by the SpellName
    /// </summary>
    /// 
    public abstract class Spell : MonoBehaviour
    {
        protected Rigidbody2D _rigidbody;
        protected CharacterControl _characterControl;
        public SpellStats SpellStats;
        public ParticleSystem ParticleEffect;

        public SpellName SpellName;

        public float RemainingDuration;
        public float CooldownRemainingDuration;
        public event EventHandler<UITimerEventArgs> DurationTimer;
        public event EventHandler<UITimerEventArgs> CooldownTimer;
        public event EventHandler OnCooldownStart;
        public event EventHandler OnCooldownEnd;
        public event EventHandler OnSpellStart;
        public event EventHandler OnSpellEnd;

        public bool IsQuickSlotAssigned;
        public int QuickSlot = -1;
        public event EventHandler<SpellEventArgs> OnUnlock;

        private bool isUnlocked; //has the attached character unlocked this ability?
        public bool IsUnlocked
        {
            get { return isUnlocked; }
            set
            {
                isUnlocked = value;
                if (value)
                {
                    OnUnlock?.Invoke(this, new SpellEventArgs(SpellStats));
                }
            }
        } 

        private bool isAvailable = true;  //is this spell available to cast?  false during cooldown
        public bool IsAvailable
        {
            get { return isAvailable; }
            set
            {
                isAvailable = value;
                if (value)
                {
                    OnCooldownEnd?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    OnCooldownStart?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        #region Unity Functions
        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _characterControl = GetComponentInParent<CharacterControl>();
        }

        protected virtual void OnEnable()
        {
            OnSpellStart += SpellStartHandler;
            OnSpellEnd += SpellEndHandler;
        }

        protected virtual void Update()
        {
            if (RemainingDuration > 0.0f)
            {
                RemainingDuration -= Time.deltaTime;
                DurationTimer?.Invoke(this, new UITimerEventArgs(RemainingDuration / SpellStats.Duration));
                if (RemainingDuration <= 0.0f)
                {
                    OnSpellEnd?.Invoke(this, EventArgs.Empty);
                }
            }
            if (CooldownRemainingDuration > 0.0f)
            {
                CooldownRemainingDuration -= Time.deltaTime;
                CooldownTimer?.Invoke(this, new UITimerEventArgs(CooldownRemainingDuration / SpellStats.CooldownDuration));
                if (CooldownRemainingDuration <= 0.0f)
                {
                    IsAvailable = true;
                }
            }
        }

        protected virtual void OnDisable()
        {
            OnSpellStart -= SpellStartHandler;
            OnSpellEnd -= SpellEndHandler;
        }
        #endregion


        /// <summary>
        /// Cast the spell.  If spell not available, do not cast.  If available, set to not available and cast
        /// Set Duration and Cooldown Timers
        /// 
        /// </summary>
        public virtual void Cast()
        {
            if (IsAvailable)
            {
                IsAvailable = false;
                RemainingDuration = SpellStats.Duration;
                CooldownRemainingDuration = SpellStats.CooldownDuration;
                OnSpellStart?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                Debug.Log(string.Format("Spell {0} is not available, skipping cast", SpellName.ToString()));
                return;
            }
        }

        /// <summary>
        /// Default On Action handler fires the particle effects (if they exist)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SpellStartHandler(object sender, EventArgs e)
        {
            Debug.Log(string.Format("{0} has called SpellStartHandler", SpellName.ToString()));
            if (ParticleEffect)
            {
                ParticleEffect.Play();
            }
        }

        /// <summary>
        /// Default Off Action handler stops the particle effects (if they exist)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void SpellEndHandler(object sender, EventArgs e)
        {
            Debug.Log(string.Format("{0} has called SpellEndHandler", SpellName.ToString()));
            if (ParticleEffect)
            {
                ParticleEffect.Stop();
            }
        }
    }
}
