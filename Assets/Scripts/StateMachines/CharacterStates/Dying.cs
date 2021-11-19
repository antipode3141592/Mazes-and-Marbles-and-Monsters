using UnityEngine;
using System;
using MarblesAndMonsters.Characters;
using Pathfinding;

namespace MarblesAndMonsters.States.CharacterStates
{
    /// <summary>
    /// Can enter a Dying state at any time
    /// While Dying, Character is mostly uninteractible
    /// </summary>
    public class Dying : CharacterState
    {
        public override Type Type { get => typeof(Dying); }

        public Dying(CharacterControl character) : base(character)
        {
        }

        public override Type LogicUpdate()
        {
            return typeof(Dying);
        }
    }
}
