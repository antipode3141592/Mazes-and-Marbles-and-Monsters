using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MarblesAndMonsters.Characters;
using Pathfinding;
using FiniteStateMachine;

namespace MarblesAndMonsters.States.CharacterStates
{
    public class Hunting : IState
    {
        protected IMover _mover;
        protected MeleeController _meleeController;
        protected RangedController _rangedController;
        protected int AttackCounter;

        public Hunting(IMover mover, MeleeController meleeController, RangedController rangedController)
        {
            _mover = mover;
            _meleeController = meleeController;
            _rangedController = rangedController;
        }

        public void Tick()
        {
            _mover.Move();
            if (_meleeController.TryAttack() > 0)
            {
                AttackCounter++;
            }
        }

        public void OnEnter()
        {
            AttackCounter = 0;
            _mover.SetTarget(_rangedController.CurrentTarget);
        }

        public void OnExit()
        {

        }
    }
}