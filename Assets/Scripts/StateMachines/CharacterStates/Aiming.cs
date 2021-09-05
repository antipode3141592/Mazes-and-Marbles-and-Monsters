using UnityEngine;
using System;
using MarblesAndMonsters.Characters;
using Pathfinding;
using System.Collections.Generic;

namespace MarblesAndMonsters.States.CharacterStates
{
    public class Aiming : CharacterState
    {

        public override Type Type { get => typeof(Aiming); }

        protected Vector2? roamDirection; //nullable destination vector
        protected Seeker seeker;
        protected Path path;
        ContactFilter2D contactFilter2D;
        protected List<Collider2D> colliders;

        public Aiming(CharacterControl character) : base(character)
        {
            colliders = new List<Collider2D>();
            seeker = character.gameObject.GetComponent<Seeker>();
            //seeker.pathCallback += OnPathComplete;
            contactFilter2D.layerMask = LayerMask.NameToLayer("Player");    //testing the player layer
            contactFilter2D.useTriggers = false;    //but not the Player's triggers
        }

        public override Type LogicUpdate()
        {
            if (_character.rangedCollider.OverlapCollider(contactFilter2D, colliders) > 0)
            {
                //if (_character.RangedAttackIsAvailable)
                //{
                //    Vector2 origin = _gameObject.transform.position;
                //    Vector2 direction = (Player.Instance.transform.position - _gameObject.transform.position).normalized;
                //    float distance = (Player.Instance.transform.position - _gameObject.transform.position).magnitude;
                //    List<RaycastHit2D> hits = new List<RaycastHit2D>();
                //    //contact filter limits to NPC and Wall layers
                //    int results = Physics2D.Raycast(origin, direction, contactFilter, hits, distance);
                //    //if any results, do not fire, as something is in the way
                //    if (results > 0)
                //    {
                //        //Debug.Log(string.Format("{0} senses the player but does not have line of sight", name));
                //    }
                //    else
                //    {
                //        //fire!
                //        _character.RangedAttackIsAvailable = false;
                //        //StartCoroutine(FireProjectile(0.33f, direction));
                //        //StartCoroutine(ProjectileCooldown());
                //    }
                //}
                return typeof(Hunting); //
            }

            return typeof(Aiming);
        }

        public override void Enter()
        {
            base.Enter();
        }
    }
}