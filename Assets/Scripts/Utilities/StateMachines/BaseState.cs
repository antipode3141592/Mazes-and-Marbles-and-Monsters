
using System;
using UnityEngine;

public abstract class BaseState
{
    protected GameObject _gameObject;
    protected Transform _transform;

    public BaseState(GameObject gameObject)
    {
        _gameObject = gameObject;
        _transform = gameObject.transform;
    }

    public virtual void Enter() { }

    public virtual Type LogicUpdate() { return null; }

    public virtual void PhysicsUpdate() { }

    public virtual void Exit() { }
}
