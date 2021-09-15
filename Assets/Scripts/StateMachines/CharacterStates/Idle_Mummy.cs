using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Characters;

namespace MarblesAndMonsters.States.CharacterStates
{
    public class Idle_Mummy : CharacterState
    {
        //protected List<Collider2D> colliders;

        public override Type Type { get => typeof(Idle_Mummy); }

        public Idle_Mummy(CharacterControl character) : base(character)
        {
        }

        public override Type LogicUpdate()
        {
            if (TargetInRangedRange())
            {
                return typeof(Hunting_Mummy);
            }
            return typeof(Idle_Mummy);
        }
    }
}