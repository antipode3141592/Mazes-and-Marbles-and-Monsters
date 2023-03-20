﻿using MarblesAndMonsters.Characters;
using MarblesAndMonsters.Events;
using System;
using System.Collections;
using UnityEngine;

namespace MarblesAndMonsters
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorController : MonoBehaviour
    {
        protected Animator _animator;
        protected Rigidbody2D _rigidbody;
        protected SpriteRenderer _spriteRenderer;
        protected CharacterSheet _characterSheet;
        protected CharacterControl _characterControl;
        protected CharacterManager _characterManager;
        protected float _speed;
        protected Material _defaultMaterial;

        protected Vector2 lookDirection = new Vector2(1, 0); //default look right
        //for storing animator string hashes
        protected int aFloatSpeed;
        protected int aFloatLookX;
        protected int aFloatLookY;
        protected int aTriggerDamageNormal;
        protected int aTriggerFalling;
        protected int aTriggerDeathByDamage;
        protected int aIdle;

        public event EventHandler<DeathEventArgs> OnDeathAnimationComplete;

        void Awake()
        {
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _characterSheet = GetComponent<CharacterSheet>();
            _characterControl = GetComponent<CharacterControl>();
            _characterManager = FindObjectOfType<CharacterManager>();
            _defaultMaterial = _spriteRenderer.material;

            //cache hashes for animation strings
            aFloatSpeed = Animator.StringToHash("Speed");
            aFloatLookX = Animator.StringToHash("Look X");
            aFloatLookY = Animator.StringToHash("Look Y");
            aTriggerDamageNormal = Animator.StringToHash("DamageNormal");
            aTriggerFalling = Animator.StringToHash("Falling");
            aTriggerDeathByDamage = Animator.StringToHash("DeathByDamage");
            aIdle = Animator.StringToHash("Idle");
        }

        void OnEnable()
        {
            _animator.Play(aIdle);
        }

        void Start()
        {
            _characterControl.OnDying += OnDying;
            _characterControl.OnDamage += OnDamage;
        }

        void Update()
        {
            //grab acceleration input
            SetLookDirection();
            _animator.SetFloat(aFloatSpeed, _rigidbody.velocity.magnitude);
        }

        void OnDestroy()
        {
            _characterControl.OnDying -= OnDying;
            _characterControl.OnDamage -= OnDamage;
        }

        protected virtual void SetLookDirection()
        {
            lookDirection = _rigidbody.velocity.normalized;
            _animator.SetFloat(aFloatLookX, lookDirection.x);
            _animator.SetFloat(aFloatLookY, lookDirection.y);
        }

        public virtual void SetAnimationSpeed(float targetSpeed)
        {
            _animator.speed = targetSpeed;
        }

        public virtual void UpdateSpriteMaterial(Material material)
        {
            _spriteRenderer.material = material;
        }

        public virtual Material GetCurrentMaterial()
        {
            return _spriteRenderer.material;
        }

        public virtual void ResetMaterial()
        {
            _spriteRenderer.material = _defaultMaterial;
        }

        public void OnDamage(object sender, DamageEventArgs e)
        {
            switch (e.DamageType)
            {
                case DamageType.Normal:
                    _animator.SetTrigger(aTriggerDamageNormal);
                    break;
                default:
                    Debug.LogWarning("Unhandled damage type enum!", this);
                    break;
            }
        }

        public void OnDying(object sender, DeathEventArgs e)
        {
            switch (e.DeathType)
            {
                case DeathType.Falling:
                    _animator.SetTrigger(aTriggerFalling);
                    break;
                case DeathType.Damage:
                    _animator.SetTrigger(aTriggerDeathByDamage);
                    break;
                case DeathType.Fire:
                    break;
                case DeathType.Poison:
                    break;
                default:
                    Debug.LogWarning("Unhandled deathtype enum!");
                    break;
            }
            StartCoroutine(DeathAnimationDelay(delay: 1f, deathType: e.DeathType));

        }

        protected virtual IEnumerator DeathAnimationDelay(float delay, DeathType deathType)
        {
            Debug.Log($"{gameObject.name} death type {deathType}, animation delay {delay}", this);
            yield return new WaitForSeconds(delay);
            AfterDeathAnimation();
            OnDeathAnimationComplete?.Invoke(this, new DeathEventArgs(deathType));
        }

        protected virtual void AfterDeathAnimation()
        {

        }

        public void ResetAnimator()
        {
            
        }
    }
}
