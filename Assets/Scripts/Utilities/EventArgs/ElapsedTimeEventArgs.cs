using System.Collections;
using UnityEngine;
using System;

namespace MarblesAndMonsters.Events
{
    public class ElapsedTimeEventArgs : EventArgs
    {
        public TimeSpan elapsedTime;

        public ElapsedTimeEventArgs(TimeSpan timeSpan)
        {
            elapsedTime = timeSpan;
        }
    }
}