using System.Collections;
using UnityEngine;

namespace FiniteStateMachine
{
    public interface IState
    {
        void Tick();
        void OnEnter();
        void OnExit();
    }
}