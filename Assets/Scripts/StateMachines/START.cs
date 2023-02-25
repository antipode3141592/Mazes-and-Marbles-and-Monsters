using FiniteStateMachine;
using System;

namespace MarblesAndMonsters.States.GameStates
{
    //    START, PopulateLevel, Playing, Paused, Victory, Defeat, END


    //Start occurs when a new level/scene is loaded, so take this opportunity to check for Player, assign spawn points, etc
    public class START : GameState
    {
        public override Type Type { get => typeof(START); }

        public START(IGameManager manager ): base()
        {
            _manager = manager;
        }

        public override Type LogicUpdate()
        {
            if (_manager.ShouldBeginLevel)
            {
                _manager.ShouldBeginLevel = false;
                return typeof(PopulateLevel);
            }
            return typeof(START);
        }
    }
}
