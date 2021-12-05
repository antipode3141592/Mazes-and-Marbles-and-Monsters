using System;

namespace MarblesAndMonsters.Events
{
    public class DeathEventArgs: EventArgs
    {
        public DeathType DeathType;

        public DeathEventArgs(DeathType deathType)
        {
            DeathType = deathType;
        }
    }
}
