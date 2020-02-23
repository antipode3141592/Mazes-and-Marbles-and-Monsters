using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HutongGames.PlayMaker;

public class TreasureCounterController : MonoBehaviour
{
    int treasureCount;
    public Text treasureCountText;

    void Awake()
    {
        treasureCount = FsmVariables.GlobalVariables.FindFsmInt("TreasureCount_global").Value;
    }

    public void UpdateTreasureCountUI()
    {
        treasureCount = FsmVariables.GlobalVariables.FindFsmInt("TreasureCount_global").Value;
        treasureCountText.text = treasureCount.ToString();
        Debug.Log("Update treasureCount UI!");
    }
}
