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
        }
        //protected void OnEnable()
        //{
        //    _characterControl.MySheet.OnLevitating += SpellStartHandler;
        //    _characterControl.MySheet.OnLevitatingEnd += SpellEndHandler;
        //}

        //protected void OnDisable()
        //{
        //    _characterControl.MySheet.OnLevitating -= SpellStartHandler;
        //    _characterControl.MySheet.OnLevitatingEnd -= SpellEndHandler;
        //}

        public override void Cast()
        {
            base.Cast();
            //_characterControl.ApplyLevitate(ItemStats.EffectDuration);
            //_characterControl.MySheet.LevitatingTimeCounter = SpellStats.Duration;
            _characterControl.MySheet.IsLevitating = true;
        }

        public override void SpellEndHandler(object sender, EventArgs e)
        {
            base.SpellEndHandler(sender, e);
            _characterControl.MySheet.IsLevitating = false;
        }

        //private void OnTriggerStay2D(Collider2D collision)
        //{
        //    if (collision)
        //    {

        //    }
        //}
    }
}