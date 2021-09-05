using System;
using UnityEngine;

namespace FiniteStateMachine
{
    public abstract class BaseState
    {
        protected GameObject _gameObject;
        protected Transform _transform;
        public abstract Type Type { get ; }
        

        public BaseState(GameObject gameObject)
        {
            _gameObject = gameObject;
            _transform = gameObject.transform;
        }

        public virtual void Enter() 
        {
            Debug.Log($"{_gameObject.name} has entered {Type.Name} state");
        }

        public virtual void HandleInput() { }

        public virtual Type LogicUpdate() { return null; }

        public virtual void PhysicsUpdate() { }

        public virtual void Exit() { }
    }
}