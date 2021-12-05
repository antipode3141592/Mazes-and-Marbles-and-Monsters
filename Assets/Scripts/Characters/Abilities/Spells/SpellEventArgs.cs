using LevelManagement.DataPersistence;
using System;

namespace MarblesAndMonsters
{
    /// <summary>
    /// The spell types
    /// </summary>


    public class SpellEventArgs : EventArgs
    {
        public SpellStats SpellStats;

        public SpellEventArgs(SpellStats spellStats)
        {
            SpellStats = spellStats;
        }
    }
}
