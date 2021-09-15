using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Characters;

namespace MarblesAndMonsters.States.CharacterStates
{
    public class Idle : CharacterState
    {
        //protected List<Collider2D> colliders;

        public override Type Type { get => typeof(Idle); } 

        public Idle(CharacterControl character) : base(character) 
        {
            timeToStateChange = 3f; //spend 3 seconds Idle before beginning roam behavior.
        }
        
        public override Type LogicUpdate()
        {
            timeToStateChangeTimer -= Time.deltaTime;
            if (timeToStateChangeTimer <= 0f)
            {
                return typeof(Roaming);
            }
            return typeof(Idle);
        }

        public override void Enter()
        {
            base.Enter();
            _character.MySheet.IsBoardMovable = false;
        }
    }
}