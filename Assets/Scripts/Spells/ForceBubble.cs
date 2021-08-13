using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.Spells
{
    /// <summary>
    /// Create an impervious barrier around the caster
    ///     
    /// </summary>
    public class ForceBubble : Spell
    {
        public CircleCollider2D circleCollider2D;
        public Animator animator;
        public SpriteRenderer spriteRenderer;

        protected override void Awake()
        {
            base.Awake();
            circleCollider2D = GetComponent<CircleCollider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            circleCollider2D.enabled = false;
            spriteRenderer.enabled = false;
        }

        public override void Cast()
        {
            base.Cast();
            circleCollider2D.enabled = true;
            spriteRenderer.enabled = true;
            _characterControl.MySheet.DamageImmunities.Add(DamageType.All);
        }

        public override void SpellEndHandler(object sender, EventArgs e)
        {
            base.SpellEndHandler(sender, e);
            circleCollider2D.enabled = false;
            spriteRenderer.enabled = false;
            _characterControl.MySheet.DamageImmunities.Remove(DamageType.All);
        }

    }
}