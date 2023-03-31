using FiniteStateMachine;
using System;

namespace MarblesAndMonsters.States.GameStates
{
    public class ViewingMap : IGameState
    {
        public Type Type { get => typeof(ViewingMap); }

        public ViewingMap()
        {

        }

        public Type LogicUpdate(float deltaTime)
        {
            return typeof(ViewingMap);
        }

        public void Enter()
        {
            
        }

        public void HandleInput()
        {
            
        }

        public void PhysicsUpdate()
        {
            
        }

        public void Exit()
        {
            
        }
    }

}
