using UnityEngine;
using System;
using MarblesAndMonsters.Characters;
using Pathfinding;

namespace MarblesAndMonsters.States.CharacterStates
{
    public class Shooting : CharacterState
    {
        public override Type Type { get => typeof(Shooting); }

        public Shooting(CharacterControl character) : base(character)
        {
        }

        public override Type LogicUpdate()
        {
            return typeof(Roaming);
        }
    }
}
