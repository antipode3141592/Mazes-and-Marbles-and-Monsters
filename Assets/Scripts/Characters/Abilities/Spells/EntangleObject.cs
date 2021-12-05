using MarblesAndMonsters.Characters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.Spells
{
    /// <summary>
    /// Physical reprsentation of the Entangle spell on the game board.
    /// Object has a circle collider trigger that checks for charcter objects that stay in its area.
    ///     Character objects are "held" by the vines of the entanglement. Animated vines are created at the edge of the character object
    ///     "Held" characters are immobile, but can still be injured
    /// </summary>
    public class EntangleObject : SpellEffectBase
    {
        public GameObject VinePrefab;   //the vine object that enwraps entangled characters
        public CircleCollider2D CircleCollider2D;
        protected Dictionary<GameObject, AnimatorController> entangledCharacters;

        protected override void Awake()
        {
            base.Awake();
            CircleCollider2D = GetComponent<CircleCollider2D>();
            entangledCharacters = new Dictionary<GameObject, AnimatorController>();
        }

        public override void EndEffect()
        {
            CircleCollider2D.enabled = false;
            foreach (var character in entangledCharacters)
            {
                if (character.Value)
                {
                    character.Value.SetBodyType(RigidbodyType2D.Dynamic);
                }
            }
            base.EndEffect();
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (!collision.isTrigger && !collision.gameObject.Equals(Caster))
            {
               
                GameObject character = collision.gameObject;
                AnimatorController animatorController = character.GetComponent<AnimatorController>();

                if (!entangledCharacters.ContainsKey(character) && animatorController)
                {
                    Debug.Log($"{Caster.name}'s spell has entangled {character.name}!");
                    animatorController.SetBodyType(RigidbodyType2D.Static);
                    //generate a Vine.  vines have this object's transform as a parent, so when it is destroyed, all vines are destroyed
                    Instantiate(VinePrefab, character.transform.position, Quaternion.identity, transform);
                    entangledCharacters.Add(character, animatorController);
                }
            }
        }
    }
}