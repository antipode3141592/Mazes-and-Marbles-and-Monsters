using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HutongGames.PlayMaker;

public class DeathCounterController : MonoBehaviour
{
    int deathCount;
    public Text deathCountText;
    

    void Awake()
    {
        deathCount = FsmVariables.GlobalVariables.FindFsmInt("PlayerDeaths_global").Value;
    }

    public void UpdateDeathCountUI()
    {
        deathCount = FsmVariables.GlobalVariables.FindFsmInt("PlayerDeaths_global").Value;
        deathCountText.text = deathCount.ToString();
        Debug.Log("Update deathcount UI!");
    }
}
