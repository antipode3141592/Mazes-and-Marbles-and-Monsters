using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Events;

namespace MarblesAndMonsters
{
    [CreateAssetMenu(menuName = "Stats/Spell Stats/Basic Spell Stats")]
    public class SpellStats : SpellStatsBase
    {

    }

    public abstract class SpellStatsBase: ScriptableObject
    {
        [SerializeField]
        private string id;  //unique identifier
        public bool Stackable;
        public Sprite InventoryIcon;
        public AudioClip ClipPickup;
        public SpellName SpellName;
        public string Description;
        public string PublicName;
        

        public float Duration;
        public float CooldownDuration;

        public string Id => id;
    }
}