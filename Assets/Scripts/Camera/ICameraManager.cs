using UnityEngine;

namespace MarblesAndMonsters
{
    public interface ICameraManager
    {
        void FollowObject(Transform followTransform);
        void SetFollowCameraPriority(int priority);
    }
}