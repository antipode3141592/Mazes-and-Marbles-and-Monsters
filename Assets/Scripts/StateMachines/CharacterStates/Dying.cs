using UnityEngine;
using System;
using MarblesAndMonsters.Characters;
using Pathfinding;
using FiniteStateMachine;

namespace MarblesAndMonsters.States.CharacterStates
{
    /// <summary>
    /// Can enter a Dying state at any time
    /// While Dying, Character is mostly uninteractible
    /// </summary>
    public class Dying : IState
    {
        IMover _mover;
        public Dying(IMover mover) 
        {
            _mover = mover;
        }

        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            
        }

        public void Tick()
        {
            
        }
    }
}
