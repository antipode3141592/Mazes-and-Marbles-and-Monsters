using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Characters;
using System;
using MarblesAndMonsters.Events;

namespace MarblesAndMonsters.Actions
{
    public class LevitateAction : ItemAction
    {
        protected override void Awake()
        {
            base.Awake();
            ActionName = ActionName.Levitate;
        }
        protected void OnEnable()
        {
            _characterControl.MySheet.OnLevitating += ActionOnHandler;
            _characterControl.MySheet.OnLevitatingEnd += ActionOffHandler;
        }

        protected void OnDisable()
        {
            _characterControl.MySheet.OnLevitating -= ActionOnHandler;
            _characterControl.MySheet.OnLevitatingEnd -= ActionOffHandler;
        }

        public override void Action()
        {
            base.Action();
            //_characterControl.ApplyLevitate(ItemStats.EffectDuration);
            _characterControl.MySheet.LevitatingTimeCounter = ItemStats.EffectDuration;
            _characterControl.MySheet.IsLevitating = true;

        }
    }
}