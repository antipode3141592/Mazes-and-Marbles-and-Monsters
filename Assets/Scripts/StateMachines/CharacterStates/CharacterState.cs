using FiniteStateMachine;
using MarblesAndMonsters.Characters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.States.CharacterStates
{
    public abstract class CharacterState : BaseState
    {
        protected CharacterControl _character;
        //protected Transform _targetTransform;
        protected List<Collider2D> collisionCheckResults;
        protected StateMachine stateMachine;

        protected float timeToStateChange; //time until an automatic state change occurs (optional)
        protected float timeToStateChangeTimer; //timer for automatic state change

        protected CharacterState(CharacterControl character) : base(character.gameObject)
        {
            _character = character;
            stateMachine = _character.GetComponent<StateMachine>();
            collisionCheckResults = new List<Collider2D>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool TargetInRangedRange()
        {
            if (_character.Combat.rangedCollider != null)
            {
                return TargetInRange(_character.Combat.rangedCollider, _character.Combat.targetDetectFilter);
            }
            else
            {
                return false;
            }
                
        }

        public bool TargetInMeleeRange()
        {
            if (_character.Combat.meleeCollider != null)
            {
                return TargetInRange(_character.Combat.meleeCollider, _character.Combat.targetDetectFilter);
            }
            else
            {
                return false;
            }
        }

        protected bool TargetInRange(Collider2D collider, ContactFilter2D filter)
        {
            try
            {
                collisionCheckResults.Clear();
                if (collider == null)
                {
                    throw new Exception($"{_gameObject.name} collider cannot be null");
                }
                if (collider.OverlapCollider(filter, collisionCheckResults) > 0)
                {
                    //foreach (var collision in collisionCheckResults)
                    //{
                    //    Debug.Log($"{_character.name} has collided with {collision.gameObject.name}");
                    //}
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"{_gameObject.name} encountered exception: {ex.Message}");
            }
            return false;
        }
    }
}