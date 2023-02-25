using MarblesAndMonsters;

namespace FiniteStateMachine
{

    public abstract class GameState : BaseState
    {
        //protected StateMachine stateMachine;
        protected IGameManager _manager;

        public GameState() : base() { }
    }
}