using System;
using UnityEngine;
//based on the pattern at based on https://www.raywenderlich.com/6034380-state-pattern-using-unity
namespace FiniteStateMachine
{
    public class StateMachine
    {
        private State currentState;

        public State CurrentState { get => currentState; set => currentState = value; }

        public void Initialize(State startingState)
        {
            currentState = startingState;
            startingState.Enter();
        }

        public void ChangeState(State newState)
        {
            Debug.Log(string.Format("GameController: state transition {0} -> {1}", currentState.ToString(), newState.ToString()));
            currentState.Exit();

            currentState = newState;
            newState.Enter();
        }
    }
}