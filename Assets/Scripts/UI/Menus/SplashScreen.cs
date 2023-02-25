using System.Collections;
using UnityEngine;
using LevelManagement;
using Zenject;

namespace MarblesAndMonsters.Menus
{
    public class SplashScreen : MonoBehaviour
    {
        [SerializeField]
        ScreenFader _screenFader;

        IMenuManager _menuManager;
        ILevelManager _levelManager;

        [Inject]
        public void Init(IMenuManager menuManager, ILevelManager levelManager)
        {
            _menuManager = menuManager;
            _levelManager = levelManager;
        }

        [SerializeField]
        float delay = 1f;

        void Awake()
        {
            _screenFader = GetComponent<ScreenFader>();
        }

        void Start()
        {
            _screenFader.FadeOn();
        }

        public void FadeAndLoad()
        {
            StartCoroutine(FadeAndLoadRoutine());
        }

        IEnumerator FadeAndLoadRoutine()
        {
            _screenFader.FadeOff();
            //wait for fade
            yield return new WaitForSeconds(_screenFader.FadeOffDuration);
            //remove the splash screen object
            _levelManager.LoadMainMenuLevel();
        }
    }
}