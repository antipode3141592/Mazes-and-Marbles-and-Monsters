using System.Collections;
using UnityEngine;
using LevelManagement;
using Zenject;

namespace MarblesAndMonsters.Menus
{
    public class SplashScreen : MonoBehaviour
    {
        [SerializeField]
        private ScreenFader _screenFader;

        MenuManager _menuManager;

        [Inject]
        public void Init(MenuManager menuManager)
        {
            _menuManager = menuManager;
        }

        [SerializeField]
        private float delay = 1f;

        private void Awake()
        {
            _screenFader = GetComponent<ScreenFader>();
        }

        private void Start()
        {
            _screenFader.FadeOn();
        }

        public void FadeAndLoad()
        {
            StartCoroutine(FadeAndLoadRoutine());
        }

        private IEnumerator FadeAndLoadRoutine()
        {
            _menuManager.OpenMenu(MenuTypes.MainMenu);
            _screenFader.FadeOff();
            //wait for fade
            yield return new WaitForSeconds(_screenFader.FadeOffDuration);
            //remove the splash screen object
            Object.Destroy(gameObject);
        }
    }
}