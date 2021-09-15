using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Characters;
using System;
using Pathfinding;
using Random = UnityEngine.Random;

namespace MarblesAndMonsters.States.CharacterStates
{
    public class Roaming : CharacterState
    {
        public override Type Type { get => typeof(Roaming); }

        public AiMover aiMover;

        

        public Roaming(CharacterControl character) : base(character)
        {
            aiMover = character.gameObject.GetComponent<AiMover>();
            
        }

        public override Type LogicUpdate()
        {

            if (TargetInRangedRange())
            {
                return typeof(Aiming);
            } 
            return typeof(Roaming);
        }

        public override void Enter()
        {
            base.Enter();
            aiMover.StartNewPath(MovementMode.Roam);
        }
    }
}