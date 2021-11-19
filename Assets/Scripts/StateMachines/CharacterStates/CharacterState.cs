using FiniteStateMachine;
using MarblesAndMonsters.Characters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.States.CharacterStates
{
    public abstract class CharacterState : BaseState
    {
        protected CharacterControl _character;
        protected StateMachine stateMachine;

        protected float timeToStateChange; //time until an automatic state change occurs (optional)
        protected float timeToStateChangeTimer; //timer for automatic state change

        protected CharacterState(CharacterControl character) : base(character.gameObject)
        {
            _character = character;
            stateMachine = _character.GetComponent<StateMachine>();
        }
    }
}