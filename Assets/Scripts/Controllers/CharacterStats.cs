using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MarblesAndMonsters.Characters
{
    public abstract class CharacterStats : ScriptableObject
    {

        public int MaxHealth;
        public static readonly int MaxHealthLimit = 20;

        public int Strength;
        public int Armor;
        //private TouchAttack touchAttack;
        //private ReachAttack reachAttack;
        //private RangedAttack rangedAttack;

        public List<DamageType> DamageImmunities;

        public bool RespawnFlag;   //if true, character respawn
    }
}