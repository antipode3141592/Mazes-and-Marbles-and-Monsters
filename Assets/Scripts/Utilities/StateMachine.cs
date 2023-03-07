using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FiniteStateMachine
{
    public class StateMachine : MonoBehaviour
    {
        protected BaseState currentState;
        protected Dictionary<Type, BaseState> availableStates;

        public event EventHandler<UITextUpdate> OnStateChange;

        public virtual BaseState CurrentState { get => currentState; private set => currentState = value; }

        public float LinearSpeed = 2.0f;

        public virtual void SetStates(Dictionary<Type, BaseState> states)
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
                    Debug.Log(string.Format("{0}: state transition {0} -> {1}", name, CurrentState.Type.Name, nextState.Name));
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

        void OnDestroy()
        {
            foreach (var state in availableStates)
            {
                state.Value.OnDestroy();
            }
        }
    }
}