using FiniteStateMachine;
using System;
using UnityEngine;

namespace MarblesAndMonsters.States.GameStates
{
    public class PopulateLevel : GameState
    {
        public override Type Type { get => typeof(PopulateLevel); }

        ICharacterManager _characterManager;

        public PopulateLevel(IGameManager manager, ICharacterManager characterManager) : base()
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
}
