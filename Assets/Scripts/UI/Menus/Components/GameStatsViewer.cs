using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LevelManagement.DataPersistence;

public class GameStatsViewer : MonoBehaviour
{
    //restarting the level means killing the PC and resetting all items/monsters/obstacles
    public Text Attempts;
    public Text InGameTimer;
    public Text TotalTimer;

    DataManager _dataManager;

    private void Awake()
    {
        _dataManager = FindObjectOfType<DataManager>();
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
        InGameTimer.text = $"In Game Time: hh:mm:ss";
        TotalTimer.text = $"Total Time: hh:mm:ss";
    }
}
