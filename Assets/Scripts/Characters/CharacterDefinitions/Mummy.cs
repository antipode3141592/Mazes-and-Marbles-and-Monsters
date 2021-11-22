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
        private CharacterStateMachine _stateMachine;
        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new CharacterStateMachine();

            IMover mover = GetComponent<IMover>();
            RangedController rangedController = GetComponent<RangedController>();
            MeleeController meleeController = GetComponent<MeleeController>();

            Idle idle = new Idle(mover);
            Roaming roaming = new Roaming(mover, rangedController);
            Hunting hunting = new Hunting(mover, meleeController, rangedController);
            Dying dying = new Dying(mover);

            //transitions

            _stateMachine.SetState(idle);

            void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
            void AtAny(IState to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition);



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