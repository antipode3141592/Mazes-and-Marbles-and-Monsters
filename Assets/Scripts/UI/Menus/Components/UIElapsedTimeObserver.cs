using System.Collections;
using UnityEngine;
using MarblesAndMonsters.Utilities;
using MarblesAndMonsters.Events;
using UnityEngine.UI;

namespace MarblesAndMonsters.Menus.Components
{
    public class UIElapsedTimeObserver : MonoBehaviour
    {
        private TimeTracker _timeTracker;
        [SerializeField]
        private Text ingameTimerText;

        private void Awake()
        {
            _timeTracker = FindObjectOfType<TimeTracker>();
        }

        private void OnEnable()
        {
            _timeTracker.IngameTimeEvent += OnTimerChange;
        }

        private void OnDisable()
        {
            _timeTracker.IngameTimeEvent -= OnTimerChange;
        }

        public void OnTimerChange(object sender, ElapsedTimeEventArgs e)
        {
            ingameTimerText.text = $"{e.elapsedTime:c}";    //c = Constant format
        }
    }
}