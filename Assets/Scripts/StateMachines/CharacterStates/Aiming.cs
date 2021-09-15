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

        protected Seeker seeker;
        protected Path path;
        protected List<RaycastHit2D> hits;
        protected AiMover aiMover;

        public Aiming(CharacterControl character) : base(character)
        {
            seeker = character.gameObject.GetComponent<Seeker>();
            hits = new List<RaycastHit2D>();
            aiMover = character.gameObject.GetComponent<AiMover>();
            timeToStateChange = 10f;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override Type LogicUpdate()
        {
            timeToStateChangeTimer -= Time.deltaTime;
            
            if (_character.Combat.RangedAttackIsAvailable)
            {
                //first, check to make sure target is in range
                if (TargetInRangedRange())
                {
                    var tagCheck = collisionCheckResults.Find(x => x.CompareTag("Player"));
                    if (tagCheck == null)
                        return typeof(Aiming);
                    aiMover.TargetTransform = tagCheck.transform;
                    Debug.Log($"{_gameObject.name} is targetting {tagCheck.transform.name}");
                    Vector2 origin = _transform.position;
                    Vector2 direction = (tagCheck.transform.position - _transform.position);
                    float distance = direction.magnitude;
                    //contact filter limits to NPC and Wall layers
                    int results = Physics2D.Raycast(origin, direction.normalized, _character.Combat.aimingFilter, hits, distance);
                    if (results <= 1)
                    {
                        //hits[0].
                        _character.Combat.RangedAttack(direction.normalized);
                        shotsFired++;
                        timeToStateChangeTimer = timeToStateChange;  //reset the idle counter, because we shot at something
                        //so the maximum time in state ~= maxShortsFired * timeToStateChange
                        if (shotsFired >= maxShotsFired)
                        {
                            return typeof(Hunting);
                        }
                    }
                } 
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

        public override void Exit()
        {
            base.Exit();
        }
    }
}