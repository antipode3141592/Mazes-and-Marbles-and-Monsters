using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.Actions
{
    public class ForceBubbleAction : ItemAction
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
            ActionName = ActionName.ForceBubble;
        }

        protected void OnEnable()
        {
            _characterControl.MySheet.OnForceBubble += ActionOnHandler;
            _characterControl.MySheet.OnForcebubbleEnd += ActionOffHandler;
        }

        protected void OnDisable()
        {
            _characterControl.MySheet.OnForceBubble -= ActionOnHandler;
            _characterControl.MySheet.OnForcebubbleEnd -= ActionOffHandler;
        }

        public override void Action()
        {
            base.Action();
            circleCollider2D.enabled = true;
            spriteRenderer.enabled = true;
            //StartCoroutine(PowerTimer(ItemStats.EffectDuration));
            _characterControl.MySheet.InvincibleTimeCounter = ItemStats.EffectDuration;
            _characterControl.MySheet.ForceBubbleTimeCounter = ItemStats.EffectDuration;
            _characterControl.MySheet.IsForceBubble = true;
            _characterControl.MySheet.IsInvincible = true;
        }

        public override void ActionOffHandler(object sender, EventArgs e)
        {
            base.ActionOffHandler(sender, e);
            circleCollider2D.enabled = false;
            spriteRenderer.enabled = false;
        }

    }
}