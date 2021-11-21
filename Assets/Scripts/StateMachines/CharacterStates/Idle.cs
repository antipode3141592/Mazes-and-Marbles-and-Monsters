using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Characters;
using FiniteStateMachine;

namespace MarblesAndMonsters.States.CharacterStates
{
    public class Idle : IState
    {
        IMover _mover;
        public Idle(IMover mover)
        {
            _mover = mover;
        }

        public void Tick()
        {
            
        }

        public void OnEnter()
        {
            _mover.Stop();
        }

        public void OnExit()
        {
            
        }
    }
}