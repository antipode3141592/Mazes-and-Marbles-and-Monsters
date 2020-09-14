//based on the pattern at based on https://www.raywenderlich.com/6034380-state-pattern-using-unity
namespace FiniteStateMachine
{

    public class State
    {
        protected StateMachine stateMachine;

        protected State(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public virtual void Enter()
        {
            //DisplayOnUI(UIManager.Alignment.Left);
        }

        public virtual void HandleInput()
        {
            
        }

        public virtual void LogicUpdate()
        {
            
        }

        public virtual void PhysicsUpdate()
        {
            
        }

        public virtual void Exit()
        {
        }
    }
}