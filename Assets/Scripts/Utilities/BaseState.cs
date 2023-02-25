using System;
using UnityEngine;

namespace FiniteStateMachine
{
    public abstract class BaseState
    {
        //protected GameObject _gameObject;
        //protected Transform _transform;
        public abstract Type Type { get ; }
        

        //public BaseState(GameObject gameObject)
        //{
        //    _gameObject = gameObject;
        //    _transform = gameObject.transform;
        //}

        public BaseState()
        {

        }

        public virtual void Enter() 
        {
            Debug.Log($"Entered {Type.Name} state.");
        }

        public virtual void HandleInput() { }

        public virtual Type LogicUpdate() { return null; }

        public virtual void PhysicsUpdate() { }

        public virtual void Exit() { }

        //to be called by the state machine to handle cleanup of event unsubsubscribing
        public virtual void OnDestroy()
        {

        }
    }
}