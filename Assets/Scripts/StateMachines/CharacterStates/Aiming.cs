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

        public Aiming(IMover mover, RangedController rangedController)
        {
            _mover = mover;
            _rangedController = rangedController;
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
            TimeWithClearLineOfSight = 0f;
        }

        public void OnExit()
        {

        }
    }
}