using UnityEngine;
using System;
using MarblesAndMonsters.Characters;
using Pathfinding;
using FiniteStateMachine;

namespace MarblesAndMonsters.States.CharacterStates
{
    public class Shooting : IState
    {
        public int ShotsFired = 0;
        protected RangedController _rangedController;
        protected IMover _mover;

        public Shooting(IMover mover, RangedController rangedController)
        {
            _mover = mover;
            _rangedController = rangedController;
        }

        public void OnEnter()
        {
            ShotsFired = 0;
            _mover.Stop();
        }

        public void OnExit()
        {
        }

        public void Tick()
        {
            if (_rangedController.TryAttack() > 0)
            {
                ShotsFired++;
            }
        }
    }
}
