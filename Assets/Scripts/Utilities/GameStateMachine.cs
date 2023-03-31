using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FiniteStateMachine
{
    public class GameStateMachine : MonoBehaviour
    {
        protected IGameState currentState;
        protected Dictionary<Type, IGameState> availableStates;
        public event EventHandler<UITextUpdate> OnStateChange;
        public virtual IGameState CurrentState { get => currentState; private set => currentState = value; }

        public virtual void SetStates(Dictionary<Type, IGameState> states)
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
                    Debug.Log(string.Format("{0}: state transition {1} -> {2}", name, CurrentState.Type.Name, nextState.Name));
                }

                CurrentState = availableStates[nextState];
                CurrentState.Enter();
                OnStateChange?.Invoke(this, new UITextUpdate(CurrentState.Type.Name));
            }
        }

        protected virtual void Update()
        {
            if (CurrentState == null)
            {
                //if no state, set to first state in dictionary
                if (availableStates != null)
                {
                    SwitchToNewState(availableStates.Keys.First());
                }
                else
                {
                    Debug.LogWarning("no available states!", this.gameObject);
                }
            }
            else
            {
                CurrentState.HandleInput();
                Type nextState = CurrentState.LogicUpdate(Time.deltaTime);
                if (nextState != null && nextState != CurrentState?.GetType())
                {
                    SwitchToNewState(nextState);
                }
            }
        }

        protected virtual void FixedUpdate()
        {
            if (CurrentState != null)
            {
                CurrentState.PhysicsUpdate();
            }
        }
    }
}