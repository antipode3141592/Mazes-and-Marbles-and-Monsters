using UnityEngine;
using System;
using MarblesAndMonsters.Characters;
using Pathfinding;
using System.Collections.Generic;
using FiniteStateMachine;

namespace MarblesAndMonsters.States.CharacterStates
{
    /// <summary>
    /// Aiming characters stand and wait for a clear shot to fire their projectile, moving to the Shooting state.
    /// 
    /// Character stop moving while Aiming (but can still be moved by collisions)
    /// 
    /// </summary>
    public class Aiming : IState
    {
        protected IMover _mover;
        protected RangedController _rangedController;
        public float TimeWithClearLineOfSight;
        public CharacterStateMachine _characterStateMachine;

        public Aiming(IMover mover, RangedController rangedController, CharacterStateMachine characterStateMachine)
        {
            _mover = mover;
            _rangedController = rangedController;
            _characterStateMachine = characterStateMachine;
        }

        public void Tick()
        {   
            if (_rangedController.HasLineOfSight(_rangedController.CurrentTarget, out Vector2 direction))
            {
                TimeWithClearLineOfSight += Time.deltaTime;
            } else
            {
                TimeWithClearLineOfSight = 0f;
            }
        }

        public void OnEnter()
        {
            _mover.Stop();
            _rangedController.CurrentTarget = _characterStateMachine.CurrentTarget;
            TimeWithClearLineOfSight = 0f;
        }

        public void OnExit()
        {

        }
    }
}