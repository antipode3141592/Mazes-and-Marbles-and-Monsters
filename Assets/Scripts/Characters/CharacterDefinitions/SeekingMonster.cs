using FiniteStateMachine;
using MarblesAndMonsters.States.CharacterStates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.Characters
{
    public class SeekingMonster : CharacterControl
    {
        private CharacterStateMachine _stateMachine;
        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new CharacterStateMachine();
            IMover mover = GetComponent<IMover>();
            MeleeController meleeController = GetComponent<MeleeController>();
            RangedController rangedController = GetComponent<RangedController>();
            AnimatorController animatorController = GetComponent<AnimatorController>();

            Idle idle = new Idle(mover);
            Hunting hunting = new Hunting(mover, meleeController, _stateMachine);
            Dying dying = new Dying(mover, animatorController);

            _stateMachine.SetState(idle);

            //void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
            //void AtAny(IState to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition);
        }
    }
}