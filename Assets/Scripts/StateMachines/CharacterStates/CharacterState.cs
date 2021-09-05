using FiniteStateMachine;
using MarblesAndMonsters.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.States.CharacterStates
{
    public abstract class CharacterState : BaseState
    {
        protected CharacterControl _character;

        protected CharacterState(CharacterControl character) : base(character.gameObject)
        {
            _character = character;
        }
    }
}