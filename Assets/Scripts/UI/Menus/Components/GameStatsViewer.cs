using LevelManagement.DataPersistence;
using MarblesAndMonsters;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameStatsViewer : MonoBehaviour
{
    //restarting the level means killing the PC and resetting all items/monsters/obstacles
    public Text Attempts;
    public Text InGameTimer;
    public Text TotalTimer;

    IDataManager _dataManager;
    ITimeTracker _timeTracker;

    [Inject]
    public void Init(IDataManager dataManager, ITimeTracker timeTracker)
    {
        _dataManager = dataManager;
        _timeTracker = timeTracker;
    }

    private void OnEnable()
    {
        SetGameDataText();
    }

    /// <summary>
    /// Sets the text for game stats
    /// </summary>
    public void SetGameDataText()
    {
        Attempts.text = $"Total Attempts: {_dataManager.PlayerTotalDeathCount}";
        InGameTimer.text = $"In Game Time: {TimeSpan.FromSeconds(_timeTracker.LevelTime):mm\\:ss}";
        TotalTimer.text = $"Total Time: {TimeSpan.FromSeconds(_dataManager.TotalGameTime):mm\\:ss}";
    }
}
