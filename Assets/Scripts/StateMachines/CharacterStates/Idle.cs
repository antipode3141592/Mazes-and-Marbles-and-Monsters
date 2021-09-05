using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Characters;

namespace MarblesAndMonsters.States.CharacterStates
{
    public class Idle : CharacterState
    {
        ContactFilter2D ContactFilter2D;
        protected List<Collider2D> colliders;

        public override Type Type { get => typeof(Idle); } 

        public Idle(CharacterControl character) : base(character) 
        {
            ContactFilter2D.layerMask = LayerMask.NameToLayer("Player");
            ContactFilter2D.useTriggers = false;
            colliders = new List<Collider2D>();
        }
        
        public override Type LogicUpdate()
        {
            return typeof(Roaming);
            ////if player enters the ranged collider, go to Roaming state
            //if (_character.rangedCollider.OverlapCollider(ContactFilter2D, colliders) > 0)
            //{
            //    return typeof(Roaming);
            //} else
            //{
            //    return typeof(Idle);
            //}
        }

        public override void Enter()
        {
            base.Enter();
            _character.MySheet.IsBoardMovable = false;
        }
    }
}