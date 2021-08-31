using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Characters;
using System;

namespace MarblesAndMonsters.Spells
{
    /// <summary>
    /// 
    /// </summary>
    public class TimeStop : Spell
    {
        public override SpellType SpellType { get { return SpellType.TimeSlow; } }

        public override void SpellStartHandler(object sender, EventArgs e)
        {
            base.SpellStartHandler(sender, e);
            Time.timeScale = 0.5f;

        }

        public override void SpellEndHandler(object sender, EventArgs e)
        {
            base.SpellEndHandler(sender, e);
            Time.timeScale = 1.0f;
        }


        private void StoreVelocity(CharacterControl character) 
        {

        }

        private void RestoreVelocity(CharacterControl character)
        {

        }
    }
}