using FiniteStateMachine;

namespace MarblesAndMonsters.States.CharacterStates
{
    public class Hunting : IState
    {
        protected IMover _mover;
        protected MeleeController _meleeController;
        protected RangedController _rangedController;
        protected int AttackCounter;
        protected CharacterStateMachine _characterStateMachine;

        public Hunting(IMover mover, MeleeController meleeController, CharacterStateMachine characterStateMachine)
        {
            _mover = mover;
            _meleeController = meleeController;
            _characterStateMachine = characterStateMachine; 
        }

        public void Tick()
        {
            _mover.Move();
            if (_meleeController.TryAttack() > 0)
            {
                AttackCounter++;
            }
        }

        public void OnEnter()
        {
            AttackCounter = 0;
            _mover.SetTarget(_characterStateMachine.CurrentTarget);
        }

        public void OnExit()
        {

        }
    }
}