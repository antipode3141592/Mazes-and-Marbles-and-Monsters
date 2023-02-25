using LevelManagement.DataPersistence;
using MarblesAndMonsters.Actions;
using MarblesAndMonsters.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.Spells
{
    [CreateAssetMenu(menuName = "Stats/Spell Stats/Levitation Spell Stats")]
    public class LevitatingSpellStats : SpellStatsBase
    {
        //public override void Action()
        //{
        //    base.Action();
        //    if (Player.Instance != null)
        //    {
        //        //Player.Instance.ApplyLevitate(EffectDuration);
        //        Player.Instance.MySheet.Actions.Find(x => x.SpellName == AssociatedSpell).Cast();
        //    }
        //}
    }
}