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
        playerCurrentHealth = FsmVariables.GlobalVariables.FindFsmInt("PlayerHealth_global").Value;
        playerMaxHealth = FsmVariables.GlobalVariables.FindFsmInt("PlayerMaxHealth_global").Value;
        //activate heart objects equal to max health
        int i = 0;
        for (; i < playerCurrentHealth; i++)
        {
            heartArray[i].SetActive(true);
            heartAnimators[i].Play("FullHeart");   
        }
        //set hearts up to max health to empty
        for(; i < playerMaxHealth; i++)
        {
            heartArray[i].SetActive(true);
            heartAnimators[i].Play("LoseHeart");
        }
        //deactivate remaining hearts
        for (; i < heartArray.Length; i++)
        {
            heartArray[i].SetActive(false);
        }
    }

    public void UpdateHealthUI()
    {
        AdjustHealth(0);
    }

    //increase player health by amount (probably just 1 heart at a time, but maybe not)
    public void IncreaseMaxHealth(int amount)
    {
        FsmVariables.GlobalVariables.FindFsmInt("PlayerMaxHealth_global").Value += amount;
        playerMaxHealth = FsmVariables.GlobalVariables.FindFsmInt("PlayerMaxHealth_global").Value;
        AdjustHealth(amount);
    }

    public void AdjustHealth(int health)
    {
        int targetHealth = Mathf.Clamp(playerCurrentHealth + health, 0, playerMaxHealth);
        
        FsmVariables.GlobalVariables.FindFsmInt("PlayerHealth_global").Value = targetHealth;
        if (targetHealth < playerCurrentHealth)
        {
            for (int i = targetHealth; i < playerCurrentHealth; i++)
            {
                heartAnimators[i].SetTrigger("LoseHealth");
            }
        } else if (targetHealth > playerCurrentHealth){
            for (int i = playerCurrentHealth; i < targetHealth; i++) 
            {
                if (!heartArray[i].activeInHierarchy) { heartArray[i].SetActive(true); }
                else
                {
                    //Debug.Log("send GainHealth trigger");
                    heartAnimators[i].SetTrigger("GainHealth");
                }
            }
        }
        playerCurrentHealth = targetHealth;
    }
}
