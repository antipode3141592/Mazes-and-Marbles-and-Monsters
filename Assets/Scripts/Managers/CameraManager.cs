using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using MarblesAndMonsters.Characters;

namespace MarblesAndMonsters
{
    /// <summary>
    /// Controls the Cinemachine cameras to point at different points of interest as well as follow the player
    /// </summary>
    public class CameraManager : MonoBehaviour
    {
        private CinemachineVirtualCamera followCamera;

        private void Awake()
        {
            followCamera = FindObjectOfType<CinemachineVirtualCamera>();
        }

        void Start()
        {
            FollowObject(Player.Instance.gameObject.transform);
        }

        // Update is called once per frame
        void Update()
        {

        }


        public void FollowObject(Transform followTransform)
        {
            if (followCamera != null)
            {
                //
                followCamera.Follow = followTransform;
            }
        }
    }
}