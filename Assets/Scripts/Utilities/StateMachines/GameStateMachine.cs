using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace FiniteStateMachine
{
    public class GameStateMachine: MonoBehaviour
    {
        private GameState currentState;
        private Dictionary<Type, GameState> availableStates;

        public event EventHandler OnStateChange;

        public GameState CurrentState { get => currentState; set => currentState = value; }

        //public void Initialize(GameState startingState)
        //{
        //    currentState = startingState;
        //    startingState.Enter();
        //}

        public void SetStates(Dictionary<Type, GameState> states)
        {
            availableStates = states;
        }

        public void SwitchToNewState(Type nextState)
        {
            if (nextState != null)
            {
                if (CurrentState != null)
                {
                    CurrentState.Exit();
                    Debug.Log(string.Format("{0}: state transition {0} -> {1}", name, CurrentState.ToString(), nextState.ToString()));
                }
                
                CurrentState = availableStates[nextState];
                CurrentState.Enter();
                OnStateChange?.Invoke(this, EventArgs.Empty);
            }
        }

        private void Update()
        {
            if (CurrentState == null)
            {
                //if no state, set to first state in dictionary
                if (availableStates != null)
                {
                    SwitchToNewState(availableStates.Keys.First());
                } else
                {
                    Debug.LogWarning("no available states!");
                }
            }
            else
            {
                CurrentState.HandleInput();
                Type nextState = CurrentState.LogicUpdate();
                if (nextState != null && nextState != CurrentState?.GetType())
                {
                    SwitchToNewState(nextState);
                }
            }
        }

        private void FixedUpdate()
        {
            if (CurrentState != null)
            {
                CurrentState.PhysicsUpdate();
            }
        }
    }
}