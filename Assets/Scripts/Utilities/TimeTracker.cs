using MarblesAndMonsters.Events;
using System;
using UnityEngine;

namespace MarblesAndMonsters
{

    /// <summary>
    /// There are two primary times to track, In Game Time and Session Time.
    ///     In-Game Time = total elapsed time in levels
    ///     Session Time = total realworld time 
    /// </summary>
    public class TimeTracker : MonoBehaviour
    {
        //for tracking playtime
        private TimeSpan sessionTimeElapsed;
        private TimeSpan ingameTimeElapsed;
        private DateTime startTime;
        private DateTime levelStartTime;
        private DateTime endTime;
        private DateTime ingameEndTime;

        public event EventHandler<ElapsedTimeEventArgs> SessionTimeEvent;
        public event EventHandler<ElapsedTimeEventArgs> IngameTimeEvent;

        public void StartSessionTime()
        {
            startTime = DateTime.Now;
        }

        public void StartInGameTime()
        {
            levelStartTime = DateTime.Now;
        }

        public TimeSpan EndSessionTime()
        {
            endTime = DateTime.Now;
            sessionTimeElapsed = endTime - startTime;
            SessionTimeEvent?.Invoke(this, new ElapsedTimeEventArgs(sessionTimeElapsed));
            return sessionTimeElapsed;
        }

        public TimeSpan EndInGameTime()
        {
            ingameEndTime = DateTime.Now;
            ingameTimeElapsed = ingameEndTime - startTime;
            IngameTimeEvent?.Invoke(this, new ElapsedTimeEventArgs(ingameTimeElapsed));
            return ingameTimeElapsed;
        }

        private void Update()
        {
            DateTime time = DateTime.Now;
            IngameTimeEvent?.Invoke(this, new ElapsedTimeEventArgs(time - levelStartTime));
            SessionTimeEvent?.Invoke(this, new ElapsedTimeEventArgs(time - startTime));
        }
    }
}