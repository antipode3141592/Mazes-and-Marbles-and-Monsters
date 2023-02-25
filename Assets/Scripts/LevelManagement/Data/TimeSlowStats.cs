using UnityEngine;

namespace LevelManagement.DataPersistence
{
    [CreateAssetMenu(menuName = "Stats/Spell Stats/Time Slow Stats")]
    public class TimeSlowStats : SpellStatsBase
    {
        [SerializeField, Range(0f, 1f)] float intensity;

        public float Intensity => intensity;
    }
}