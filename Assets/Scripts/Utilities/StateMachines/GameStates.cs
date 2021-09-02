﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Menus;
using MarblesAndMonsters;
using FiniteStateMachine;

namespace MarblesAndMonsters.States.GameStates
{
    //    START, PopulateLevel, Playing, Paused, Victory, Defeat, END


    //Start occurs when a new level/scene is loaded, so take this opportunity to check for Player, assign spawn points, etc
    public class START : GameState
    {
        public START(GameManager manager ): base(manager.gameObject)
        {
            _manager = manager;
        }

        public override Type LogicUpdate()
        {
            return typeof(PopulateLevel);
        }
    }

    public class PopulateLevel : GameState
    {
        public PopulateLevel(GameManager manager) : base(manager.gameObject)
        {
            _manager = manager;
        }

        public override Type LogicUpdate()
        {
            if (_manager.InitializeReferences() > 0)
            {
                _manager.SpawnAll();
                return typeof(Playing);
            }
            else
            {
                Debug.LogError(string.Format("InitializeReferences returned <= 0"));
                return typeof(PopulateLevel);
            }
        }
    }

    public class Playing : GameState
    {
        public Playing(GameManager manager) : base(manager.gameObject)
        {
            _manager = manager;
        }
        public override void Enter()
        {
            MenuManager.Instance.OpenMenu(MenuTypes.GameMenu);
            Time.timeScale = 1.0f;
        }
        public override void HandleInput()
        {
            //grab acceleration input
            _manager.Input_Acceleration = (Vector2)Input.acceleration;
        }

        public override void PhysicsUpdate()
        {
            _manager.MoveAll();  //characters should only move during play
        }
    }

    public class Paused : GameState
    {
        public Paused(GameManager manager) : base(manager.gameObject)
        {
            _manager = manager;
        }
        public override void Enter()
        {
            Time.timeScale = 0.0f;
        }
    }

    public class Victory : GameState
    {
        public Victory(GameManager manager) : base(manager.gameObject)
        {
            _manager = manager;
        }

        public override Type LogicUpdate()
        {
            return typeof(END);
        }
    }

    public class Defeat : GameState
    {
        public Defeat(GameManager manager) : base(manager.gameObject)
        {
            _manager = manager;
        }

        public override Type LogicUpdate()
        {
            _manager.ResetAll();
            return typeof(START);
        }
    }

    public class END : GameState
    {
        public END(GameManager manager) : base(manager.gameObject)
        {
            _manager = manager;
        }
    }
}