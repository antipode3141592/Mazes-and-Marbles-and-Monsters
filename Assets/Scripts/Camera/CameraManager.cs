using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MarblesAndMonsters
{
    /// <summary>
    /// Controls the Cinemachine cameras to point at different points of interest as well as follow the player
    /// </summary>
    public class CameraManager : MonoBehaviour, ICameraManager
    {
        IGameManager _gameManager;


        [SerializeField] CinemachineVirtualCamera _followCamera;

        void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode loadMoad)
        {
            Debug.Log($"{gameObject.name} processing the SceneLoaded event for the scene {scene.name}");
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void FollowObject(Transform followTransform)
        {
            if (_followCamera != null)
            {
                //
                _followCamera.Follow = followTransform;
            }
        }

        public void SetFollowCameraPriority(int priority)
        {
            _followCamera.Priority = priority;
        }
    }
}