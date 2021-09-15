using System;
using UnityEngine;

namespace MarblesAndMonsters
{
    //for transmitting transform data, very useful for camera events
    public class TransformEventArgs : EventArgs
    {
        public Transform _transform;
        public TransformEventArgs(Transform transform)
        {
            _transform = transform;
        }
    }
}