using Chronos;
using LevelManagement.DataPersistence;
using System;

namespace MarblesAndMonsters.Spells
{
    public class TimeSlow : Spell
    {
        Clock _enemiesClock;

        public Clock EnemiesClock
        {
            get
            {
                if (_enemiesClock is null)
                    _enemiesClock = Timekeeper.instance.Clock("Enemies");
                return _enemiesClock;
            }
        }

        public override SpellType SpellType { get { return SpellType.TimeSlow; } }

        public override void SpellStartHandler(object sender, EventArgs e)
        {
            var stats = SpellStats as TimeSlowStats;
            base.SpellStartHandler(sender, e);
            //EnemiesClock.LerpTimeScale(stats.Intensity, 0.3f);
            EnemiesClock.localTimeScale = stats.Intensity;
        }

        public override void SpellEndHandler(object sender, EventArgs e)
        {
            base.SpellEndHandler(sender, e);
            //EnemiesClock.LerpTimeScale(1f, 0.3f);
            EnemiesClock.localTimeScale = 1f;
        }
    }
}