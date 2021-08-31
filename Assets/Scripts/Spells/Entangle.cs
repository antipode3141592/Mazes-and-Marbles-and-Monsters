using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.Spells
{
    /// <summary>
    /// Entangle creates a small 
    /// </summary>
    public class Entangle : Spell
    {
        public EntangleObject entanglePrefab;  //

        protected EntangleObject _entangleObject;

        public override SpellType SpellType { get { return SpellType.Entangle; } }

        public override void SpellStartHandler(object sender, EventArgs e)
        {
            base.SpellStartHandler(sender, e);
            _entangleObject = Instantiate<EntangleObject>(entanglePrefab, transform.position, Quaternion.identity);
            _entangleObject.SetCaster(_characterControl);
            _entangleObject.SetDeathTime(SpellStats.Duration);
        }

        public override void SpellEndHandler(object sender, EventArgs e)
        {
            base.SpellEndHandler(sender, e);
            if (_entangleObject)
            {
                _entangleObject.EndEffect();
            }
        }
    }
}