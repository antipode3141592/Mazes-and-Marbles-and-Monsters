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
        private CircleCollider2D circleCollider2D;

        protected override void Awake()
        {
            base.Awake();
            //SpellName = SpellName.Levitate;
            circleCollider2D = GetComponent<CircleCollider2D>();
            //set the radius of the circle collider based on the spell stats
        }

        public override void SpellStartHandler(object sender, EventArgs e)
        {
            base.SpellStartHandler(sender, e);
            _characterControl.MySheet.IsLevitating = true;
        }

        public override void SpellEndHandler(object sender, EventArgs e)
        {
            base.SpellEndHandler(sender, e);
            _characterControl.MySheet.IsLevitating = false;
        }

        // Apply a simulated "wind" force every frame they are within the range of the trigger collider
        //private void OnTriggerStay2D(Collider2D collision)
        //{
        //    if (collision)
        //    {

        //    }
        //}
    }
}