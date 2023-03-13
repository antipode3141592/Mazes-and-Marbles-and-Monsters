using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FiniteStateMachine
{
    /// <summary>
    /// based on Jason Weimann's source found here: https://game.courses/bots-ai-statemachines/
    ///  the accompanying video is well worth watching
    /// </summary>
    public class CharacterStateMachine
    {
        GameObject _parentObject;

        IState _currentState;

        public event EventHandler<UITextUpdate> OnStateChange;

        Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
        List<Transition> _currentTransitions = new List<Transition>();
        List<Transition> _anyTransitions = new List<Transition>();
        static List<Transition> EmptyTransitions = new List<Transition>(capacity: 0);

        public float TimeInState = 0f;
        public Transform CurrentTarget;

        public CharacterStateMachine(GameObject parentObject)
        {
            _parentObject = parentObject;
        }

        public void Tick()
        {
            var transition = GetTransition();
            if (transition != null)
                SetState(transition.To);

            _currentState?.Tick();
            TimeInState += Time.deltaTime;
        }

        public void SetState(IState state)
        {
            if (state == _currentState)
                return;

            _currentState?.OnExit();
            _currentState = state;

            _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
            if (_currentTransitions == null)
                _currentTransitions = EmptyTransitions;
            OnStateChange?.Invoke(this, new UITextUpdate(_currentState.ToString()));
            Debug.Log($"{_parentObject.name} now entering {_currentState} state");
            _currentState.OnEnter();
            TimeInState = 0f;
        }

        public void AddTransition(IState from, IState to, Func<bool> predicate)
        {
            if (_transitions.TryGetValue(from.GetType(), out var transitions) == false)
            {
                transitions = new List<Transition>();
                _transitions[from.GetType()] = transitions;
            }

            transitions.Add(new Transition(to, predicate));
        }

        public void AddAnyTransition(IState state, Func<bool> predicate)
        {
            _anyTransitions.Add(new Transition(state, predicate));
        }

        Transition GetTransition()
        {
            foreach (var transition in _anyTransitions)
                if (transition.Condition())
                    return transition;

            foreach (var transition in _currentTransitions)
                if (transition.Condition())
                    return transition;

            return null;
        }
    }
}