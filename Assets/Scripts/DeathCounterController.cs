using LevelManagement.Data;
using UnityEngine;
using UnityEngine.UI;

public class DeathCounterController : MonoBehaviour
{
    int deathCount;
    public Text deathCountText;
    

    void Start()
    {
        //deathCount = FsmVariables.GlobalVariables.FindFsmInt("PlayerDeaths_global").Value;
        UpdateDeathCountUI();
    }

    public void UpdateDeathCountUI()
    {
        deathCount = DataManager.Instance.PlayerTotalDeathCount;
        deathCountText.text = deathCount.ToString();
        //Debug.Log("Update deathcount UI!");
    }
}
