using MarblesAndMonsters;
using System;

namespace LevelManagement.DataPersistence
{

    public enum SpellName { Levitate, ForceBubble, TimeSlow, Transmute, Entangle, StoneForm, ForcePush, Teleport, GhostForm }
    
    [Serializable]
    public class SpellData
    {
        public SpellName SpellName;
        public SpellStatsBase SpellStats;
        public bool IsAssigned;
        public int QuickSlot;

        public SpellData(SpellName spellName, SpellStatsBase spellStats, bool isAssigned = false, int quickSlot = -1)
        {
            SpellName = spellName;
            SpellStats = spellStats;
            IsAssigned = isAssigned;
            QuickSlot = quickSlot;
        }
    }
}