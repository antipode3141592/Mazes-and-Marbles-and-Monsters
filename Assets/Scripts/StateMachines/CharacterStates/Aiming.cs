using UnityEngine;
using System;
using MarblesAndMonsters.Characters;
using Pathfinding;
using System.Collections.Generic;

namespace MarblesAndMonsters.States.CharacterStates
{
    /// <summary>
    /// Aiming characters stand and wait for a clear shot to fire their projectile, moving to the Shooting state.
    /// 
    /// Character stop moving while Aiming (but can still be moved by collisions)
    /// 
    /// </summary>
    public class Aiming : CharacterState
    {

        public override Type Type { get => typeof(Aiming); }

        //protected Transform Target;
        protected int shotsFired;
        protected int maxShotsFired = 3;

        protected List<RaycastHit2D> hits;
        protected AiMover aiMover;
        protected RangedController rangedController;

        public Aiming(CharacterControl character) : base(character)
        {
            hits = new List<RaycastHit2D>();
            aiMover = character.gameObject.GetComponent<AiMover>();
            rangedController = character.gameObject.GetComponent<RangedController>();
            timeToStateChange = 10f;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override Type LogicUpdate()
        {
            timeToStateChangeTimer -= Time.deltaTime;
            
            if(rangedController.TryAttack() > 0)
            {
                aiMover.TargetTransform = rangedController.CurrentTarget;
                shotsFired++;
            }

            if (shotsFired >= maxShotsFired)
            {
                return typeof(Hunting);
            }

            // if time to state change has elapsed, go to roaming state
            if (timeToStateChangeTimer <= 0f)
            {
                return typeof(Roaming);
            }
            //if all else fails, just keep aiming
            return typeof(Aiming);
        }

        public override void Enter()
        {
            base.Enter();
            shotsFired = 0;
            aiMover.Stop();
            timeToStateChangeTimer = timeToStateChange;
        }
    }
}