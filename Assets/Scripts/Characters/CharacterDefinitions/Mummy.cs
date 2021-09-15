using FiniteStateMachine;
using MarblesAndMonsters.States.CharacterStates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.Characters
{
    public class Mummy : CharacterControl
    {
        private StateMachine stateMachine;
        protected override void Awake()
        {
            base.Awake();
            stateMachine = GetComponent<StateMachine>();
            var states = new Dictionary<Type, BaseState>()
            {
                {typeof(Idle_Mummy), new Idle_Mummy(character: this) },
                {typeof(Hunting_Mummy), new Hunting_Mummy(character: this) },
                {typeof(Dying), new Dying(character: this) }
            };
            Debug.Log($"{name} is storing the following states:  {states.Keys.ToString()}");
            stateMachine.SetStates(states);
        }

        //for Mummy's, their look direction is their directional vector
        protected override void SetLookDirection()
        {
            Vector2 direction= MyRigidbody.velocity;
            if (!Mathf.Approximately(direction.x, 0.0f) || !Mathf.Approximately(direction.y, 0.0f))
            {
                lookDirection = direction;
                lookDirection.Normalize();
            }

            animator.SetFloat(aFloatLookX, lookDirection.x);
            animator.SetFloat(aFloatLookY, lookDirection.y);
        }
    }

}