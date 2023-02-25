using FiniteStateMachine;
using MarblesAndMonsters.Menus;
using System;

namespace MarblesAndMonsters.States.GameStates
{
    public class Victory : GameState
    {
        ICharacterManager _characterManager;
        IMenuManager _menuManager;

        public override Type Type { get => typeof(Victory); }

        public Victory(IGameManager manager, IMenuManager menuManager, ICharacterManager characterManager) : base()
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
}
