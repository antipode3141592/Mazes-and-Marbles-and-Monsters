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

        protected Vector2? roamDirection; //nullable destination vector
        protected Seeker seeker;
        protected Path path;

        public Dying(CharacterControl character) : base(character)
        {
            seeker = character.gameObject.GetComponent<Seeker>();
        }

        public override Type LogicUpdate()
        {
            return typeof(Dying);
        }
    }
}
