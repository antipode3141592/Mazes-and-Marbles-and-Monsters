using FiniteStateMachine;
using MarblesAndMonsters.Spells;
using MarblesAndMonsters.States.CharacterStates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.Characters
{
    public class VineElemental : CharacterControl
    {
        private StateMachine stateMachine;
        protected float VisibilityDistance;

        protected override void Awake()
        {
            base.Awake();
            stateMachine = GetComponent<StateMachine>();
            var states = new Dictionary<Type, BaseState>()
            {
                {typeof(Idle), new Idle(character: this) },
                {typeof(Roaming), new Roaming(character: this) },
                {typeof(Hunting), new Hunting(character: this) },
                {typeof(Aiming), new Aiming(character: this) },
                {typeof(Dying), new Dying(character: this) }
            };
            Debug.Log($"{name} is storing the following states:  {states.Keys.ToString()}");
            stateMachine.SetStates(states);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            audioSource.clip = MySheet.baseStats.ClipHit;
            audioSource.Play(); //no matter what is struck, play the hit sound
        }

        //protected override void SetLookDirection()
        //{
        //    Vector2 direction = MyRigidbody.velocity;
        //    if (!Mathf.Approximately(direction.x, 0.0f) || !Mathf.Approximately(direction.y, 0.0f))
        //    {
        //        lookDirection = direction;
        //        lookDirection.Normalize();
        //    }

        //    animator.SetFloat(aFloatLookX, lookDirection.x);
        //    animator.SetFloat(aFloatLookY, lookDirection.y);
        //}
    }
}