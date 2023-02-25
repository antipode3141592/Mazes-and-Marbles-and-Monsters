using LevelManagement.DataPersistence;
using System;

namespace MarblesAndMonsters
{
    /// <summary>
    /// The spell types
    /// </summary>


    public class SpellEventArgs : EventArgs
    {
        public SpellStatsBase SpellStats;

        public SpellEventArgs(SpellStatsBase spellStats)
        {
            SpellStats = spellStats;
        }
    }
}
