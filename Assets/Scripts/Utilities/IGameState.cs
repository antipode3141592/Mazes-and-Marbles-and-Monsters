using System;

namespace FiniteStateMachine
{
    public interface IGameState
    {
        public Type Type { get ; }

        public void Enter();

        public void HandleInput();

        public Type LogicUpdate(float deltaTime);

        public void PhysicsUpdate();

        public void Exit();
    }
}