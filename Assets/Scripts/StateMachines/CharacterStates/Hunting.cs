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
        ContactFilter2D contactFilter2D;
        protected List<Collider2D> colliders;

        public Hunting(CharacterControl character) : base(character)
        {
            colliders = new List<Collider2D>();
            seeker = character.gameObject.GetComponent<Seeker>();
            contactFilter2D.layerMask = LayerMask.NameToLayer("Player");    //testing the player layer
            contactFilter2D.useTriggers = false;    //but not the Player's triggers
        }

        public override Type LogicUpdate()
        {
            if (_character.rangedCollider.OverlapCollider(contactFilter2D, colliders) > 0)
            {
                return typeof(Aiming); //
            } else
            {
                if (!roamDestination.HasValue)
                {
                    roamDestination = Player.Instance.transform.position;
                    if (roamDestination.HasValue)
                    {
                        seeker.StartPath(_transform.position, roamDestination.Value, OnPathComplete);
                    }
                }
            }
            return typeof(Hunting);
        }

        public void OnPathComplete(Path p)
        {
            Debug.Log("Hunting Path Complete!");
            roamDestination = null;
        }
    }
}