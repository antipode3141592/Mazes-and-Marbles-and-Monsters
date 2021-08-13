using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Menus;
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
            stateMachine.ChangeState(GameManager.Instance.state_populateLevel);
        }
    }

    public class PopulateLevel : State
    {
        public PopulateLevel(StateMachine stateMachine) : base(stateMachine) { }
        public override void Enter()
        {
            base.Enter();
            
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (GameManager.Instance.InitializeReferences() > 0)
            {
                GameManager.Instance.SpawnAll();
                stateMachine.ChangeState(GameManager.Instance.state_playing);
            }
            else
            {
                Debug.LogError(string.Format("InitializeReferences returned <= 0"));
            }
        }

        public override void Exit()
        {
            base.Exit();
            
        }
    }

    public class Playing : State
    {
        public Playing(StateMachine stateMachine) : base(stateMachine) { }
        public override void Enter()
        {
            base.Enter();
            MenuManager.Instance.OpenMenu(MenuTypes.GameMenu);
            //GameController.Instance.StoreCharacters();
            Time.timeScale = 1.0f;
            //GameMenu.Open();
            //MenuManager.Instance.OpenMenu(MenuTypes.GameMenu);
            //GameMenu.Instance.RefreshUI();
        }
        public override void HandleInput()
        {
            base.HandleInput();
            //grab acceleration input
            GameManager.Instance.Input_Acceleration = (Vector2)Input.acceleration;
        }
        //public override void LogicUpdate()
        //{
        //    base.LogicUpdate();
        //    //check for out of bounds characters and kill them
        //    //GameController.Instance.CheckOutofBounds();
        //}

        public override void PhysicsUpdate()
        {
            GameManager.Instance.MoveAll();  //characters should only move during play
        }

        public override void Exit()
        {
            base.Exit();
            
        }
    }

    public class Paused : State
    {
        public Paused(StateMachine stateMachine) : base(stateMachine) { }
        public override void Enter()
        {
            base.Enter();
            Time.timeScale = 0.0f;
        }
    }

    public class Victory : State
    {
        public Victory(StateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            base.Enter();
            //WinMenu.Open();
            //MenuManager.Instance.OpenMenu(MenuTypes.WinMenu);
        }
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            stateMachine.ChangeState(GameManager.Instance.state_end);
        }

        public override void Exit()
        {
            base.Exit();
            //stateMachine.ChangeState(GameController.Instance.start);
        }
    }

    public class Defeat : State
    {
        public Defeat(StateMachine stateMachine) : base(stateMachine) { }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            GameManager.Instance.ResetAll();
            stateMachine.ChangeState(GameManager.Instance.state_start);
        }
    }

    public class END : State
    {
        public END(StateMachine stateMachine) : base(stateMachine) { }
    }
}
