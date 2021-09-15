using MarblesAndMonsters.Characters;
using System;
using UnityEngine;

namespace MarblesAndMonsters.States.CharacterStates
{
    public class Hunting_Mummy : CharacterState
    {
        public override Type Type { get => typeof(Hunting_Mummy); }

        protected AiMover aiMover;
        protected int AttackCounter;
        protected int MinimumAttackCount = 1;

        public Hunting_Mummy(CharacterControl character) : base(character)
        {
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
            else if (AttackCounter >= MinimumAttackCount && !TargetInRangedRange())
            {
                return typeof(Idle_Mummy); //
            }
            if (timeToStateChangeTimer <= 0f)
            {
                aiMover.TargetTransform = null;
                return typeof(Idle_Mummy);    //if hunting for more than 10 seconds, take a rest
            }
            return typeof(Hunting_Mummy);
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