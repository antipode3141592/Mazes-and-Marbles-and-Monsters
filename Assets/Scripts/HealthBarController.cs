using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class HealthBarController : MonoBehaviour
{
    public GameObject[] heartArray;
    Animator[] heartAnimators;
    public int playerMaxHealth;
    public int playerCurrentHealth;
    

    void Awake()
    {
        heartAnimators = new Animator[heartArray.Length];
        //populate array of Animators
        for (int i = 0; i < heartArray.Length; i++)
        {
            heartAnimators[i] = heartArray[i].GetComponent<Animator>();
        }
    }

    private void Start()
    {
        playerCurrentHealth = FsmVariables.GlobalVariables.FindFsmInt("PlayerHealth_global").Value;
        playerMaxHealth = FsmVariables.GlobalVariables.FindFsmInt("PlayerMaxHealth_global").Value;
    }

    public void ResetHealth()
    {
        playerCurrentHealth = playerMaxHealth;
        //activate heart objects equal to max health
        for (int i = 0; i < playerMaxHealth; i++)
        {
            heartArray[i].SetActive(true);
            heartAnimators[i].Play("FullHeart");   
        }
        //deactivate remaining hearts
        for (int i = playerMaxHealth + 1; i < heartArray.Length; i++)
        {
            heartArray[i].SetActive(false);
        }
    }

    public void AdjustHealth(int health)
    {
        int targetHealth = Mathf.Clamp(playerCurrentHealth + health, 0, playerMaxHealth);
        if (targetHealth < playerCurrentHealth)
        {
            for (int i = targetHealth; i < playerCurrentHealth; i++)
            {
                heartAnimators[i].SetTrigger("LoseHealth");
            }
        } else if (targetHealth > playerCurrentHealth){
            for (int i = playerCurrentHealth; i < targetHealth; i++) 
            {
                Debug.Log("send GainHealth trigger");
                heartAnimators[i].SetTrigger("GainHealth");
            }
        }
        playerCurrentHealth = targetHealth;
        FsmVariables.GlobalVariables.FindFsmInt("PlayerHealth_global").Value = targetHealth;

    }
}
