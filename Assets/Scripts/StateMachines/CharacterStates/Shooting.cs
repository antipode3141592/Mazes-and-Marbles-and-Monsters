using UnityEngine;
using System;
using MarblesAndMonsters.Characters;
using Pathfinding;

namespace MarblesAndMonsters.States.CharacterStates
{
    public class Shooting : CharacterState
    {
        public override Type Type { get => typeof(Shooting); }

        protected Vector2? roamDirection; //nullable destination vector
        protected Seeker seeker;
        protected Path path;

        public Shooting(CharacterControl character) : base(character)
        {
            seeker = character.gameObject.GetComponent<Seeker>();
        }

        public override Type LogicUpdate()
        {
            return typeof(Roaming);
        }
    }
}
