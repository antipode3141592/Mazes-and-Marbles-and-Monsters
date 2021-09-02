//based on the pattern at based on https://www.raywenderlich.com/6034380-state-pattern-using-unity
using MarblesAndMonsters;
using System;
using UnityEngine;

namespace FiniteStateMachine
{

    public abstract class GameState : BaseState
    {
        //protected StateMachine stateMachine;
        protected GameManager _manager;

        public GameState(GameObject gameObject) : base(gameObject) { }

        public virtual void HandleInput() { }
    }
}