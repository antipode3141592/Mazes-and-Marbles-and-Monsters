using MarblesAndMonsters.Characters;
using System;
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
        [SerializeField] GameObject vinePrefab;   //the vine object that enwraps entangled characters
        CircleCollider2D circleCollider2D;
        Dictionary<Guid, CharacterControl> entangledCharacters;

        protected override void Awake()
        {
            base.Awake();
            circleCollider2D = GetComponent<CircleCollider2D>();
            entangledCharacters = new();
        }

        public override void EndEffect()
        {
            circleCollider2D.enabled = false;
            foreach (var character in entangledCharacters)
                character.Value.MyRigidbody.simulated = true;
            entangledCharacters.Clear();
            base.EndEffect();
        }

        void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.isTrigger)
                return;
            var characterControl = collision.gameObject.GetComponent<CharacterControl>();
            if (characterControl is null)
                return;
            if (entangledCharacters.ContainsKey(characterControl.Guid))
                return;
            Debug.Log($"{characterControl.name} has become entangled!");
            //generate a Vine.  vines have this object's transform as a parent, so when it is destroyed, all vines are destroyed
            Instantiate(vinePrefab, characterControl.transform.position, Quaternion.identity, transform);
            entangledCharacters.Add(characterControl.Guid, characterControl);
            characterControl.MyRigidbody.velocity = Vector2.zero;
            characterControl.MyRigidbody.simulated = false;
        }
    }
}