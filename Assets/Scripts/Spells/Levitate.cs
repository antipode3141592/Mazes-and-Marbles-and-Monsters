using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Characters;
using System;
using MarblesAndMonsters.Events;

namespace MarblesAndMonsters.Spells
{
    public class Levitate : Spell
    {
        public CircleCollider2D CircleCollider2D;
        public override SpellType SpellType { get { return SpellType.Levitate;  } }

        protected override void Awake()
        {
            base.Awake();
            //SpellName = SpellName.Levitate;
            if (CircleCollider2D == null)
            {
                CircleCollider2D = GetComponent<CircleCollider2D>();
            }
            CircleCollider2D.enabled = false;
            //set the radius of the circle collider based on the spell stats
        }

        public override void SpellStartHandler(object sender, EventArgs e)
        {
            base.SpellStartHandler(sender, e);
            _characterControl.MySheet.IsLevitating = true;
            _characterControl.ForceMultiplier = 0.5f;
            CircleCollider2D.enabled = true;
        }

        public override void SpellEndHandler(object sender, EventArgs e)
        {
            base.SpellEndHandler(sender, e);
            _characterControl.MySheet.IsLevitating = false;
            _characterControl.ForceMultiplier = 1.0f;
            CircleCollider2D.enabled = false;
        }

        // Apply a simulated "wind" force every frame they are within the range of the trigger collider
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (!collision.isTrigger)
            {
                Vector3 casterTotarget = _characterControl.gameObject.transform.position - collision.transform.position;
                Vector3 forceVector = Vector3.Cross(new Vector3(0, 0, -1.0f), casterTotarget).normalized;
                collision.attachedRigidbody.AddForce(forceVector * 50.0f, ForceMode2D.Impulse);
            }
            //CharacterControl character = collision.gameObject.GetComponent<CharacterControl>();
            //if (character != null && character != _characterControl)
            //{
            //    Vector3 casterTotarget = _characterControl.gameObject.transform.position - character.gameObject.transform.position;
            //    //apply counter-clockwise normal/tangent
            //    Vector3 forceVector = Vector3.Cross(new Vector3(0,0,-1.0f), casterTotarget).normalized;
            //    character.ApplyImpulse(forceVector * 50.0f);
            //}
        }
    }
}