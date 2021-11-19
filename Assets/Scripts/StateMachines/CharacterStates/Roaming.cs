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
        protected RangedController rangedController;


        public Roaming(CharacterControl character) : base(character)
        {
            aiMover = character.gameObject.GetComponent<AiMover>();
            rangedController = character.gameObject.GetComponent<RangedController>();
        }

        public override Type LogicUpdate()
        {
            if (rangedController.GetNearestEnemyWithLineOfSight(out Transform _transform))
            {
                Debug.Log($"{_character.name} has locked onto {_transform.name}");
                rangedController.CurrentTarget = _transform;
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