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
        protected CharacterStateMachine _stateMachine;
        protected float VisibilityDistance;

        public CharacterStateMachine StateMachine => _stateMachine;

        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new CharacterStateMachine();

            IMover mover = GetComponent<IMover>();
            RangedController rangedController = GetComponent<RangedController>();
            MeleeController meleeController = GetComponent<MeleeController>();

            Idle idle =  new Idle(mover);
            Roaming roaming = new Roaming(mover, rangedController);
            Hunting hunting = new Hunting(mover, meleeController, rangedController);
            Aiming aiming = new Aiming(mover, rangedController);
            Shooting shooting = new Shooting(mover, rangedController);
            Dying dying = new Dying(mover);

            At(to: roaming, from:idle, condition: TimeElapsed(5f));
            At(to: idle, from: roaming, TimeElapsed(10f));
            At(to: idle, from: hunting, TimeElapsed(10f));
            At(to: aiming, from: roaming, EnemyInLineOfSight());
            At(to: shooting, from: aiming, HasClearShot(minAimTime: 0.15f));
            At(to: hunting, from: shooting, ShotsFired(maxShots: 3));
            At(to: roaming, from: aiming, TimeElapsed(5f));
            AtAny(dying, IsDying());

            _stateMachine.SetState(idle);

            void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
            void AtAny(IState to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition);

            

            Func<bool> IsDying() => () => isDying == true;
            Func<bool> EnemyInLineOfSight() => () => rangedController.CurrentTarget != null;
            Func<bool> HasClearShot(float minAimTime) => () => aiming.TimeWithClearLineOfSight >= minAimTime;
            Func<bool> TimeElapsed(float time) => () => _stateMachine.TimeInState >= time;
            Func<bool> ShotsFired(int maxShots) => () => shooting.ShotsFired >= maxShots;
        }

        protected override void Update()
        {
            base.Update();
            _stateMachine.Tick();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            audioSource.clip = MySheet.baseStats.ClipHit;
            audioSource.Play(); //no matter what is struck, play the hit sound
        }
    }
}