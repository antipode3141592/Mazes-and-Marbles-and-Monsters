using System;
using UnityEngine;
using UnityEngine.UI;

namespace MarblesAndMonsters.Menus.Components
{
    public class UIElapsedTimeObserver : MonoBehaviour
    {
        ITimeTracker _timeTracker;
        [SerializeField] Text ingameTimerText;

        void Awake()
        {
            _timeTracker = FindObjectOfType<TimeTracker>();
        }

        void Start()
        {
            _timeTracker.LevelTimeEvent += OnTimerChange;
            ingameTimerText.text = "";
        }

        private void OnDestroy()
        {
            _timeTracker.LevelTimeEvent -= OnTimerChange;
        }

        public void OnTimerChange(object sender, TimeSpan e)
        {
            ingameTimerText.text = $"{e:mm\\:ss}";    //c = Constant format
        }
    }
}