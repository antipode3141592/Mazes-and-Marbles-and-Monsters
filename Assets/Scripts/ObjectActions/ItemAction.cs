using System;
using MarblesAndMonsters.Events;
using MarblesAndMonsters.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters
{
    /// <summary>
    /// The action types
    /// </summary>
    public enum ActionName { Levitate, ForceBubble, TimeStop, Transfigure }

    /// <summary>
    /// Actions are objects that must be attached to an object with a CharacterControl
    /// 
    /// </summary>
    /// 
    public abstract class ItemAction : MonoBehaviour
    {
        protected Rigidbody2D _rigidbody;
        protected CharacterControl _characterControl;
        public ItemStatsBase ItemStats;
        public ParticleSystem ParticleEffect;

        [SerializeField]
        public ActionName ActionName;

        private float CooldownRemainingDuration;
        public event EventHandler<UITimerEventArgs> CooldownTimer;
        public event EventHandler OnCooldownStart;
        public event EventHandler OnCooldownEnd;

        private bool isAvailable;
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
                    CooldownRemainingDuration = ItemStats.CooldownDuration;
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

        public virtual void Update()
        {
            if (CooldownRemainingDuration > 0.0f)
            {
                CooldownRemainingDuration -= Time.deltaTime;
                CooldownTimer?.Invoke(this, new UITimerEventArgs(CooldownRemainingDuration / ItemStats.CooldownDuration));
                if (CooldownRemainingDuration <= 0.0f)
                {
                    IsAvailable = true;
                }
            }
        }
        #endregion

        public virtual void Action()
        {
            if (IsAvailable)
            {
                IsAvailable = false;
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Default On Action handler fires the particle effects (if they exist)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void ActionOnHandler(object sender, EventArgs e)
        {
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
        public virtual void ActionOffHandler(object sender, EventArgs e)
        {
            if (ParticleEffect)
            {
                ParticleEffect.Stop();
            }
        }
    }
}
