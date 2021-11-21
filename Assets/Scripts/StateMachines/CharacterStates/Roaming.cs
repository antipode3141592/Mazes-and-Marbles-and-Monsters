using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Characters;
using System;
using Pathfinding;
using Random = UnityEngine.Random;
using FiniteStateMachine;

namespace MarblesAndMonsters.States.CharacterStates
{
    public class Roaming : IState
    {
        public IMover _mover;
        protected RangedController _rangedController;


        public Roaming(IMover mover, RangedController rangedController)
        {
            _mover = mover;
            _rangedController = rangedController;
        }

        public void Tick()
        {
            _mover.Move();
            if (_rangedController.GetNearestEnemyWithLineOfSight(out Transform _transform))
            {
                _rangedController.CurrentTarget = _transform;
                _mover.SetTarget(_transform);
            } 
        }

        public void OnEnter()
        {
            _mover.SetTarget(null);
        }

        public void OnExit()
        {

        }
    }
}