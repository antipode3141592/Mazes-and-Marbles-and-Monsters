using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadMoad)
        {
            Debug.Log($"{gameObject.name} processing the SceneLoaded event for the scene {scene.name}");
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void OnFollowTransformHandler(object sender, TransformEventArgs e)
        {
            FollowObject(e._transform);
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