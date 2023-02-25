using FiniteStateMachine;
using MarblesAndMonsters.Menus;
using System;

namespace MarblesAndMonsters.States.GameStates
{
    public class Defeat : GameState
    {
        IMenuManager _menuManager;
        ICharacterManager _characterManager;

        public override Type Type { get => typeof(Defeat); }

        public Defeat(IGameManager manager, IMenuManager menuManager, ICharacterManager characterManager) : base()
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
}
