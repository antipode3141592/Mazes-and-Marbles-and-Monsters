using FiniteStateMachine;
using System;

namespace MarblesAndMonsters.States.GameStates
{
    public class END : GameState
    {
        public override Type Type { get => typeof(END); }

        public END(IGameManager manager) : base()
        {
            _manager = manager;
        }
    }
}
