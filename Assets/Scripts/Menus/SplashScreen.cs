using System.Collections;
using UnityEngine;
using LevelManagement;

namespace MarblesAndMonsters.Menus
{
    public class SplashScreen : MonoBehaviour
    {
        [SerializeField]
        private ScreenFader _screenFader;

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
            
            yield return new WaitForSeconds(delay);
            _screenFader.FadeOff();
            LevelManager.LoadMainMenuLevel();
            //wait for fade
            yield return new WaitForSeconds(_screenFader.FadeOffDuration);

            //remove the splash screen object
            Object.Destroy(gameObject);
        }
    }
}