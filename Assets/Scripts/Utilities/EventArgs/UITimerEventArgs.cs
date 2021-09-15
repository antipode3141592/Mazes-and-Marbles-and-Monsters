using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MarblesAndMonsters.Events
{
    public class UITimerEventArgs : EventArgs
    {
        public float PercentComplete; // 0

        public UITimerEventArgs(float percentComplete)
        {
            PercentComplete = percentComplete;
        }
    }
}