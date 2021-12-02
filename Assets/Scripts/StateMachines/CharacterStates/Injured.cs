using FiniteStateMachine;
using MarblesAndMonsters.Characters;
using System;

namespace MarblesAndMonsters.States
{
    public class Injured : IState
    {
        protected CharacterControl _characterControl;
        protected AnimatorController _animatorController;
        protected IMover _mover;

        public void OnEnter()
        {
            throw new NotImplementedException();
        }

        public void OnExit()
        {
            throw new NotImplementedException();
        }

        public void Tick()
        {
            throw new NotImplementedException();
        }
    }
}
