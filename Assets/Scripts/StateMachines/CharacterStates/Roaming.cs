using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Characters;
using System;
using Pathfinding;

namespace MarblesAndMonsters.States.CharacterStates
{
    public class Roaming : CharacterState
    {
        public override Type Type { get => typeof(Roaming); }

        protected Vector2? roamDestination; //nullable destination vector
        protected Seeker seeker;
        protected Path path;
        ContactFilter2D contactFilter2D;
        protected List<Collider2D> colliders;

        public float speed = 2.0f;

        public float nextWaypointDistance = 0.75f;

        private float EstimatedPathCompletionTime;

        private int currentWaypoint = 0;

        public bool reachedEndOfPath;

        private float EstimatedTimeRemainingCounter;

        public Roaming(CharacterControl character) : base(character)
        {
            colliders = new List<Collider2D>();
            seeker = character.gameObject.GetComponent<Seeker>();
            //seeker.pathCallback += OnPathComplete;
            contactFilter2D.layerMask = LayerMask.NameToLayer("Player");    //testing the player layer
            contactFilter2D.useTriggers = false;    //but not the Player's triggers
        }

        

        public override Type LogicUpdate()
        {
            ////if Player in Trigger
            //if (_character.rangedCollider.OverlapCollider(contactFilter2D, colliders) > 0)
            //{
            //    return typeof(Hunting); //
            //}

            if (path == null)
            {
                Debug.Log("No path found, skipping update");
                return typeof(Roaming);
            }

            if (EstimatedPathCompletionTime >= 0f)
            {
                EstimatedPathCompletionTime -= Time.deltaTime;
                if (EstimatedPathCompletionTime <= 0f)
                {
                    path = null;
                    roamDestination = null;
                    SetNewPath();
                    return typeof(Roaming);
                }
            }


            // Check in a loop if we are close enough to the current waypoint to switch to the next one.
            // We do this in a loop because many waypoints might be close to each other and we may reach
            // several of them in the same frame.
            reachedEndOfPath = false;
            // The distance to the next waypoint in the path
            float distanceToWaypoint;
            Debug.Log($"{_gameObject.name} in {Type.ToString()} state is about to check waypoints along path");
            while (true)
            {
                // If you want maximum performance you can check the squared distance instead to get rid of a
                // square root calculation. But that is outside the scope of this tutorial.
                distanceToWaypoint = Vector3.Distance(_transform.position, path.vectorPath[currentWaypoint]);
                if (distanceToWaypoint < nextWaypointDistance)
                {
                    // Check if there is another waypoint or if we have reached the end of the path
                    if (currentWaypoint + 1 < path.vectorPath.Count)
                    {
                        currentWaypoint++;
                    }
                    else
                    {
                        Debug.Log("End of Path!");
                        // Set a status variable to indicate that the agent has reached the end of the path.
                        // You can use this to trigger some special code if your game requires that.
                        reachedEndOfPath = true;
                        path = null;
                        roamDestination = null;
                        SetNewPath();
                        return typeof(Roaming);
                    }
                }
                else
                {
                    break;
                }
            }

            //var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint / nextWaypointDistance) : 1f;

            // Direction to the next waypoint
            // Normalize it so that it has a length of 1 world unit
            Vector3 dir = (path.vectorPath[currentWaypoint] - _transform.position).normalized;
            // Multiply the direction by our desired speed to get a velocity
            Vector3 velocity = dir * speed;
            Debug.Log($"{_gameObject.name} is roaming at {velocity.ToString()} velocity");
            _transform.position += velocity * Time.deltaTime;

            

            return typeof(Roaming); //stay roaming
        }

        private void SetNewPath()
        {
            if (!roamDestination.HasValue)
            {
                //  set random direction
                roamDestination = (Vector2)UnityEngine.Random.insideUnitCircle * 5.0f;



                //  move in direction, using A*
                if (roamDestination.HasValue)
                {
                    seeker.StartPath(_transform.position, roamDestination.Value, OnPathComplete);
                }
            }
        }
        
        public void OnPathComplete(Path p)
        {
            if (!p.error)
            {
                path = p;
                EstimatedPathCompletionTime = p.GetTotalLength() / speed;
                EstimatedTimeRemainingCounter = 1.5f * EstimatedPathCompletionTime;
                // Reset the waypoint counter so that we start to move towards the first point in the path
                currentWaypoint = 0;
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void Enter()
        {
            base.Enter();
            SetNewPath();
            //_character.MySheet.IsBoardMovable = true;
        }
    }
}