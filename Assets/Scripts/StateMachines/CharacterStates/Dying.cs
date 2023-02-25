using FiniteStateMachine;

namespace MarblesAndMonsters.States.CharacterStates
{
    /// <summary>
    /// Can enter a Dying state at any time
    /// While Dying, Character is mostly uninteractible
    /// </summary>
    public class Dying : IState
    {
        IMover _mover;
        AnimatorController _animatorController;

        public Dying(IMover mover, AnimatorController animatorController) 
        {
            _mover = mover;
            _animatorController = animatorController;
        }

        public void OnEnter()
        {
            _mover.Stop();
        }

        public void OnExit()
        {
            
        }

        public void Tick()
        {
            
        }
    }
}
