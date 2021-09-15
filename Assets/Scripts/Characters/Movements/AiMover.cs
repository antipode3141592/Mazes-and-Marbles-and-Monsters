using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;  //powered by Aron Granberg's excellent Pathfinding asset
using System;
using Random = UnityEngine.Random;

namespace MarblesAndMonsters
{
    public enum MovementMode { None, Roam, LeftHandRule, Follow}

    /// <summary>
    /// For position based movement, powered by Aron Granberg's Pathfinding asset
    /// 
    /// </summary>
    public class AiMover : MonoBehaviour, IMove
    {
        protected Vector2? currentDestination; //nullable destination vector

        [SerializeField] public MovementMode Mode;
        [SerializeField] public Transform TargetTransform;
        [SerializeField] protected Transform parentTransform;

        //for pathfinding library
        protected Seeker seeker;
        protected Path path;

        public float speed = 2.0f;  //target speed 

        public float nextWaypointDistance = 0.75f;  //

        [SerializeField] private int currentWaypoint = 0;

        public bool reachedEndOfPath;

        public bool IsControlling;  //indicates if this controlls the game object or not (

        public event EventHandler OnPathEnd;

        #region Unity Functions
        private void Awake()
        {
            seeker = GetComponent<Seeker>();
            Mode = MovementMode.None;
            parentTransform = GetComponentInParent<Transform>();
        }

        private void OnEnable()
        {
            seeker.pathCallback += OnPathCalculationComplete;
        }

        private void OnDisable()
        {
            seeker.pathCallback -= OnPathCalculationComplete;
        }

        // Update is called once per frame
        void Update()
        {
            //if there isn't a path defined or mover is not controlling, don't do anything
            if (path != null && IsControlling)
            {
                Move();
            }
        }

        public void Move()
        {
            if (path == null)
                return;
            // Check in a loop if we are close enough to the current waypoint to switch to the next one.
            // We do this in a loop because many waypoints might be close to each other and we may reach
            // several of them in the same frame.
            reachedEndOfPath = false;
            // The distance to the next waypoint in the path
            float distanceToWaypoint;
            while (true)
            {
                // If you want maximum performance you can check the squared distance instead to get rid of a
                // square root calculation. But that is outside the scope of this tutorial.
                distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
                if (distanceToWaypoint < nextWaypointDistance)
                {
                    // Check if there is another waypoint or if we have reached the end of the path
                    if (currentWaypoint + 1 < path.vectorPath.Count)
                    {
                        currentWaypoint++;
                    }
                    else
                    {
                        //Debug.Log("End of Path!");
                        // Set a status variable to indicate that the agent has reached the end of the path.
                        // You can use this to trigger some special code if your game requires that.
                        reachedEndOfPath = true;
                        path = null;
                        StartNewPath(Mode);
                        return;
                    }
                }
                else
                {
                    break;
                }
            }
            // Direction to the next waypoint
            // Normalize it so that it has a length of 1 world unit
            Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
            // Multiply the direction by our desired speed to get a velocity
            Vector3 velocity = dir * speed;
            //Debug.Log($"{gameObject.name} is roaming at {velocity.ToString()} velocity");
            transform.position += velocity * Time.deltaTime;
        }

        /// <summary>
        /// stop moving 
        /// </summary>
        public void Stop()
        {
            path = null;
            IsControlling = false;
        }


        #endregion
        public void StartMoving()
        {

        }

        protected void SetNewDestination(Vector2? destination = null)
        {
            if (destination.HasValue)
            {
                currentDestination = destination;
                seeker.StartPath(transform.position, destination.Value);
            }
        }

        public void OnPathCalculationComplete(Path p)
        {
            if (!p.error)
            {
                path = p;
                currentWaypoint = 0;
                IsControlling = true;
            }
        }



        public void StartNewPath(MovementMode mode)
        {
            Mode = mode;
            switch(mode)
            {
                case MovementMode.Roam:
                    SetNewDestination(GetRandomNearbyTarget());
                    break;
                case MovementMode.LeftHandRule:
                    break;
                case MovementMode.Follow:
                    if (TargetTransform != null)
                        SetNewDestination(TargetTransform.position);
                    break;
                case MovementMode.None:
                    break;
                default:
                    break;
            }
        }

        private Vector2 GetRandomNearbyTarget()
        {
            return Random.insideUnitCircle * 5f + (Vector2)parentTransform.position;
        }
    }
}