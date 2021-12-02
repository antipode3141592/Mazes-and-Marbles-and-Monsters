using FiniteStateMachine;
using MarblesAndMonsters.States.CharacterStates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.Characters
{
    [RequireComponent(typeof(MeleeController))]
    public class Mummy : CharacterControl
    {
        private CharacterStateMachine _stateMachine;
        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new CharacterStateMachine();

            IMover mover = GetComponent<IMover>();
            MeleeController meleeController = GetComponent<MeleeController>();
            AnimatorController animatorController = GetComponent<AnimatorController>();

            Idle idle = new Idle(mover);
            Hunting hunting = new Hunting(mover, meleeController, _stateMachine);
            Dying dying = new Dying(mover, animatorController);

            //transitions

            At(from: idle, to: hunting, EnemyInLineOfSight());
            At(from: hunting, to: idle, TimeElapsed(30f));

            AtAny(dying, IsDying());

            _stateMachine.SetState(idle);

            void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
            void AtAny(IState to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition);


            Func<bool> IsDying() => () => isDying == true;
            Func<bool> EnemyInLineOfSight() => () =>
            {
                if (meleeController.FindNearestEnemyInLineOfSight(out var gameObject))
                {
                    _stateMachine.CurrentTarget = gameObject.transform;
                    return true;
                }
                return false;
            };
            Func<bool> TimeElapsed(float time) => () => _stateMachine.TimeInState >= time;
        }
    }

}