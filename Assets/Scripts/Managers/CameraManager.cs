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

        void Start()
        {
            followCamera = FindObjectOfType<CinemachineVirtualCamera>();
            if (followCamera != null)
            {
                //
                followCamera.Follow = Player.Instance.gameObject.transform;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }


        public void FollowObject(Transform transform)
        {

        }
    }
}