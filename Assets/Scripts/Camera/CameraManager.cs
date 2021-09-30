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
        private List<GameObject> rootGameObjects;

        private void Awake()
        {
            rootGameObjects = new List<GameObject>();
            followCamera = FindObjectOfType<CinemachineVirtualCamera>();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadMoad)
        {
            Debug.Log($"{gameObject.name} processing the SceneLoaded event for the scene {scene.name}");
            //if (scene.name != "Managers")
            //{
            //    scene.GetRootGameObjects(rootGameObjects);
            //    foreach (var obj in rootGameObjects)
            //    {
            //        if (TryGetComponent<CinemachineVirtualCamera>(out CinemachineVirtualCamera vcam))
            //        {
            //            followCamera = vcam;
            //        }
            //    }
            //}
        }

        void Start()
        {
            //FollowObject(Player.Instance.gameObject.transform);

        }

        // Update is called once per frame
        void Update()
        {

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

        private void OnDestroy()
        {
            
        }
    }
}