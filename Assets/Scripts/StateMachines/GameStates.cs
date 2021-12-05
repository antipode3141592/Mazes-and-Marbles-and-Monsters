using System;
using UnityEngine;
using FiniteStateMachine;
using MarblesAndMonsters.Menus;

namespace MarblesAndMonsters.States.GameStates
{
    //    START, PopulateLevel, Playing, Paused, Victory, Defeat, END


    //Start occurs when a new level/scene is loaded, so take this opportunity to check for Player, assign spawn points, etc
    public class START : GameState
    {
        public override Type Type { get => typeof(START); }

        public START(GameManager manager ): base(manager.gameObject)
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

    public class PopulateLevel : GameState
    {
        public override Type Type { get => typeof(PopulateLevel); }

        CharacterManager _characterManager;

        public PopulateLevel(GameManager manager, CharacterManager characterManager) : base(manager.gameObject)
        {
            _manager = manager;
            _characterManager = characterManager;
        }

        public override Type LogicUpdate()
        {
            if (_characterManager.InitializeReferences() > 0)
            {
                _characterManager.SpawnAll();
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
        public override Type Type { get => typeof(Playing); }

        CharacterManager _characterManager;
        TimeTracker _timeTracker;

        public Playing(GameManager manager, CharacterManager characterManager, TimeTracker timeTracker) : base(manager.gameObject)
        {
            _manager = manager;
            _characterManager = characterManager;
            _timeTracker = timeTracker;
        }
        public override void Enter()
        {
            //MenuManager.Instance.OpenMenu(MenuTypes.GameMenu);
            Time.timeScale = 1.0f;
            _timeTracker.StartSessionTime();
        }
        public override void HandleInput()
        {
            //grab acceleration input
            _characterManager.Input_Acceleration = (Vector2)Input.acceleration;
        }

        public override void PhysicsUpdate()
        {
            _characterManager.MoveAll();  //characters should only move during play
        }
    }

    public class Paused : GameState
    {
        public override Type Type { get => typeof(Paused); }

        TimeTracker _timeTracker;

        public Paused(GameManager manager, TimeTracker timeTracker) : base(manager.gameObject)
        {
            _manager = manager;
            _timeTracker = timeTracker;
        }
        public override void Enter()
        {
            Time.timeScale = 0.0f;
        }
    }

    public class Victory : GameState
    {
        CharacterManager _characterManager;
        MenuManager _menuManager;

        public override Type Type { get => typeof(Victory); }

        public Victory(GameManager manager, MenuManager menuManager, CharacterManager characterManager) : base(manager.gameObject)
        {
            _manager = manager;
            _menuManager = menuManager;
            _characterManager = characterManager;
        }

        public override void Enter()
        {
            base.Enter();
            _characterManager.ResetAll();
            _menuManager.OpenMenu(MenuTypes.WinMenu);
        }

        public override Type LogicUpdate()
        {
            return typeof(START);
        }
    }

    public class Defeat : GameState
    {
        MenuManager _menuManager;
        CharacterManager _characterManager;

        public override Type Type { get => typeof(Defeat); }

        public Defeat(GameManager manager, MenuManager menuManager, CharacterManager characterManager) : base(manager.gameObject)
        {
            _manager = manager;
            _menuManager = menuManager;
            _characterManager = characterManager;
        }

        public override void Enter()
        {
            base.Enter();
            _characterManager.ResetAll();
            _menuManager.OpenMenu(MenuTypes.DefeatMenu);
        }

        public override Type LogicUpdate()
        {
            return typeof(START);
        }
    }

    public class END : GameState
    {
        public override Type Type { get => typeof(END); }

        public END(GameManager manager) : base(manager.gameObject)
        {
            _manager = manager;
        }
    }
}
