using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MarblesAndMonsters.Characters;
using Pathfinding;

namespace MarblesAndMonsters.States.CharacterStates
{
    /// <summary>
    /// OnEnter: If there is a target in Ranged range, lock onto that character
    /// Update: 
    /// </summary>
    public class Hunting : CharacterState
    {
        public override Type Type { get => typeof(Hunting); }

        protected Vector2? roamDestination; //nullable destination vector
        protected AiMover aiMover;
        protected MeleeController meleeController;
        protected int AttackCounter;
        protected int MinimumAttackCount = 1;

        public Hunting(CharacterControl character) : base(character)
        {
            aiMover = character.gameObject.GetComponent<AiMover>();
            meleeController = character.gameObject.GetComponent<MeleeController>();
            timeToStateChange = 10f;
        }

        public override Type LogicUpdate()
        {
            timeToStateChangeTimer -= Time.deltaTime;

            //if Melee Attack is successful, increment Attack Counter
            if (meleeController.TryAttack() > 0)
            {
                AttackCounter++;
            }
            if (timeToStateChangeTimer <= 0f)
            {
                return typeof(Idle);    //if hunting for more than 10 seconds, take a rest
            }
            return typeof(Hunting);
        }

        public override void Enter()
        {
            base.Enter();
            AttackCounter = 0;
            aiMover.StartNewPath(MovementMode.Follow);
            timeToStateChangeTimer = timeToStateChange;
        }
    }
}