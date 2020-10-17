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
        private int spawnWaitCounter = 0;
        //use the base class constructor
        public START(StateMachine stateMachine) : base(stateMachine){}
        public override void Enter()
        {
            base.Enter();
            Time.timeScale = 0.0f;
            spawnWaitCounter = 0;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            //check if the tilemaps are done producing characters

            if (GameController.Instance.InitializeReferences() > 0)
            {
                stateMachine.ChangeState(GameController.Instance.populateLevel);
                Debug.Log(string.Format("spawnWaitCounter = {0}", spawnWaitCounter));
            } else
            {
                spawnWaitCounter++;
                if (spawnWaitCounter > 30)
                {
                    Debug.LogError("No spawnpoints found!");
                }
            }
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
            //if (GameController.Instance.StoreCharacters() == GameController.Instance.InitializeReferences()) { 
                stateMachine.ChangeState(GameController.Instance.playing);
            //}
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
            //GameController.Instance.StoreCharacters();
            Time.timeScale = 1.0f;
            GameMenu.Open();
        }

        //public override void LogicUpdate()
        //{
        //    base.LogicUpdate();
        //    //check for out of bounds characters and kill them
        //    //GameController.Instance.CheckOutofBounds();
        //}

        public override void PhysicsUpdate()
        {
            GameController.Instance.MoveAll();  //characters should only move during play
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
        //public override void Enter()
        //{
        //    base.Enter();
        //    PauseMenu.Open();
        //}
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
