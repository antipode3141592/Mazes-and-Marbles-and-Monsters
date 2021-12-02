using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Characters;
using System;

namespace MarblesAndMonsters.Spells
{
    public class StoneForm : Spell
    {
        [SerializeField]
        protected Material characterMaterial;

        public override SpellType SpellType { get { return SpellType.StoneForm; } }

        public override void SpellStartHandler(object sender, EventArgs e)
        {
            base.SpellStartHandler(sender, e);

            _animatorController.SetBodyType(RigidbodyType2D.Static);
            _characterControl.MySheet.DamageImmunities.Add(DamageType.All);
            _animatorController.UpdateSpriteMaterial(characterMaterial);
        }

        public override void SpellEndHandler(object sender, EventArgs e)
        {
            base.SpellEndHandler(sender, e);
            _animatorController.SetBodyType(RigidbodyType2D.Dynamic);
            _characterControl.MySheet.DamageImmunities.Remove(DamageType.All);
            _animatorController.ResetMaterial(); //return to previous material
        }
    }
}