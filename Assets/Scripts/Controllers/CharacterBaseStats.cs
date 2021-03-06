using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Actions;


namespace MarblesAndMonsters.Characters
{
    [CreateAssetMenu(fileName = "BaseStats_", menuName = "BaseStats/Characters")]
    public class CharacterBaseStats : ScriptableObject
    {

        public int MaxHealth;
        public static readonly int MaxHealthLimit = 20;

        public int Armor;

        public List<DamageType> DamageImmunities;

        public bool RespawnFlag;   //if true, character respawns automatically, otherwise, when character only respawns at PopulateLevel state
        public float RespawnPeriod;

        public TouchAttack TouchAttack;
        public RangedAttack RangedAttack;
        public ReachAttack ReachAttack;

        //sounds clips
        public AudioClip ClipHit;
        public AudioClip ClipHeal;
        public AudioClip ClipDeathNormal;
        public AudioClip ClipDeathFall;
        public AudioClip ClipDeathFire;


    }
}