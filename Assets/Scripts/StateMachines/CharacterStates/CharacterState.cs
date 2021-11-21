using FiniteStateMachine;
using MarblesAndMonsters.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.States.CharacterStates
{
    public abstract class CharacterState : IState
    {
        protected float timeToStateChangeTimer; //timer for automatic state change

        public abstract void OnEnter();
        public abstract void OnExit();
        public abstract void Tick();
    }
}