using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MarblesAndMonsters.Characters;
using Pathfinding;

namespace MarblesAndMonsters.States.CharacterStates
{
    public class Hunting : CharacterState
    {
        public override Type Type { get => typeof(Hunting); }

        protected Vector2? roamDestination; //nullable destination vector
        protected Seeker seeker;
        protected Path path;
        protected AiMover aiMover;
        protected int AttackCounter;
        protected int MinimumAttackCount = 1;

        public Hunting(CharacterControl character) : base(character)
        {
            //colliders = new List<Collider2D>();
            seeker = character.gameObject.GetComponent<Seeker>();
            aiMover = character.gameObject.GetComponent<AiMover>();
            timeToStateChange = 10f;
        }

        public override Type LogicUpdate()
        {
            timeToStateChangeTimer -= Time.deltaTime;
            if (_character.Combat.MeleeAttackIsAvailable && TargetInMeleeRange())
            {
                //damage all damagables within the melee attack range
                foreach (Collider2D collider in collisionCheckResults)
                {
                    if (collider.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable))
                    {
                        _character.Combat.MeleeAttack(damagable);
                        AttackCounter++;
                    }
                }
            }
            else if (AttackCounter >= MinimumAttackCount && _character.Combat.RangedAttackIsAvailable && TargetInRangedRange())
            {
                return typeof(Aiming); //
            }
            if (timeToStateChangeTimer <= 0f)
            {
                aiMover.TargetTransform = null;
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