using MarblesAndMonsters;
using UnityEngine;

namespace FiniteStateMachine
{

    public abstract class GameState : BaseState
    {
        //protected StateMachine stateMachine;
        protected GameManager _manager;

        public GameState(GameObject gameObject) : base(gameObject) { }
    }
}