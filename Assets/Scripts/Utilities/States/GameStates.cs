using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManagement.Menus;
using MarblesAndMonsters;

namespace FiniteStateMachine.States.GameStates
{
    //    START, PopulateLevel, Playing, Paused, Victory, Defeat, END


    //Start occurs when a new level/scene is loaded, so take this opportunity to check for Player, assign spawn points, etc
    public class START : State
    {
        //use the base class constructor
        public START(StateMachine stateMachine) : base(stateMachine){}
        public override void Enter()
        {
            base.Enter();
            Time.timeScale = 0.0f;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            stateMachine.ChangeState(GameController.Instance.populateLevel);
        }
    }

    public class PopulateLevel : State
    {
        public PopulateLevel(StateMachine stateMachine) : base(stateMachine) { }
        public override void Enter()
        {
            base.Enter();
            GameController.Instance.SpawnAll();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            stateMachine.ChangeState(GameController.Instance.playing);
        }
    }

    public class Playing : State
    {
        public Playing(StateMachine stateMachine) : base(stateMachine) { }
        public override void Enter()
        {
            base.Enter();
            Time.timeScale = 1.0f;
            GameMenu.Open();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            //check for out of bounds characters and kill them
            //GameController.Instance.CheckOutofBounds();
        }

        public override void Exit()
        {
            base.Exit();
            Time.timeScale = 0.0f;
        }
    }

    public class Paused : State
    {
        public Paused(StateMachine stateMachine) : base(stateMachine) { }
        public override void Enter()
        {
            base.Enter();
            PauseMenu.Open();
        }
    }

    public class Victory : State
    {
        public Victory(StateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            base.Enter();
            WinMenu.Open();
        }

        public override void Exit()
        {
            base.Exit();
            stateMachine.ChangeState(GameController.Instance.start);
        }
    }

    public class Defeat : State
    {
        public Defeat(StateMachine stateMachine) : base(stateMachine) { }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            stateMachine.ChangeState(GameController.Instance.start);
        }
    }

    public class END : State
    {
        public END(StateMachine stateMachine) : base(stateMachine) { }
    }
}
