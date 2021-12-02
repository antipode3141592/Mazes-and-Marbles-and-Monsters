using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarblesAndMonsters.Events
{
    public class DamageEventArgs: EventArgs
    {
        public DamageType DamageType;

        public DamageEventArgs(DamageType damageType)
        {
            DamageType = damageType;
        }
    }
}
