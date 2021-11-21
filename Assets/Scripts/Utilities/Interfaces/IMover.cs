using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters
{
    //for anything that moves a game object, use this!
    public interface IMover
    {
        public void SetTarget(Transform target);

        public void Move();

        public void Stop();
    }
}