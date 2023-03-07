using System;
using UnityEngine;

namespace MarblesAndMonsters
{

    /// <summary>
    /// Time is tracked as current level time and total game time.
    ///     Level Time = elapsed time of current level run
    ///     Game Time = total of all level times
    /// </summary>
    public class TimeTracker : MonoBehaviour, ITimeTracker
    {
        public event EventHandler<TimeSpan> TotalGameTimeEvent;
        public event EventHandler<TimeSpan> LevelTimeEvent;

        bool isPaused;
        float levelTime;
        float gameTime;

        public float LevelTime => levelTime;
        public float TotalGameTime => gameTime;

        void Update()
        {
            if (isPaused)
                return;
            levelTime += Time.deltaTime;
            LevelTimeEvent?.Invoke(this, TimeSpan.FromSeconds(levelTime));
        }

        public void StartLevelTimer()
        {
            levelTime = 0f;
        }

        public TimeSpan EndLevelTimer()
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(levelTime);
            LevelTimeEvent?.Invoke(this, timeSpan);
            return timeSpan;
        }

        public void PauseLevelTimer()
        {
            isPaused = true;
        }

        public void ResumeLevelTimer()
        {
            isPaused = false;
        }
    }
}