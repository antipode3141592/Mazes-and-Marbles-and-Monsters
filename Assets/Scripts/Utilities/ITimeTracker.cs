using System;

namespace MarblesAndMonsters
{
    public interface ITimeTracker
    {
        float LevelTime { get; }
        float TotalGameTime { get; }

        event EventHandler<TimeSpan> LevelTimeEvent;

        TimeSpan EndLevelTimer();
        void StartLevelTimer();
        void PauseLevelTimer();
        void ResumeLevelTimer();
    }
}