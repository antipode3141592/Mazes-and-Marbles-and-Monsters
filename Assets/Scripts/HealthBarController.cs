using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarblesAndMonsters.Characters;
using MarblesAndMonsters;

namespace MarblesAndMonsters.UI
{
    //View Controller for HealthBar
    //  current system uses animated hearts to represent health, one heart = 1hp
    public class HealthBarController : MonoBehaviour
    {
        public GameObject[] heartArray; //currently set in Unity to twenty hearts
        Animator[] heartAnimators;

        void Awake()
        {
            heartAnimators = new Animator[heartArray.Length];
            //populate array of Animators
            for (int i = 0; i < heartArray.Length; i++)
            {
                heartAnimators[i] = heartArray[i].GetComponent<Animator>();
            }
        }

        //private void Start()
        //{
        //    //playerCurrentHealth = FsmVariables.GlobalVariables.FindFsmInt("PlayerHealth_global").Value;
        //    //playerMaxHealth = FsmVariables.GlobalVariables.FindFsmInt("PlayerMaxHealth_global").Value;

        //}

        public void ResetHealth()
        {
            if (Player.Instance != null)
            {
                //playerCurrentHealth = Player.Instance.MySheet.CurrentHealth;
                //playerMaxHealth = Player.Instance.MySheet.MaxHealth;
                //activate heart objects equal to max health
                int i = 0;
                for (; i < Player.Instance.MySheet.CurrentHealth; i++)
                {
                    heartArray[i].SetActive(true);
                    heartAnimators[i].Play("FullHeart");
                }
                //set hearts up to max health to empty
                for (; i < Player.Instance.MySheet.MaxHealth; i++)
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
        }

        public void UpdateHealthUI()
        {
            AdjustHealth(0);
        }

        //increase player health by amount (probably just 1 heart at a time, but maybe not)
        public void IncreaseMaxHealth(int amount)
        {
            //FsmVariables.GlobalVariables.FindFsmInt("PlayerMaxHealth_global").Value += amount;
            //playerMaxHealth = FsmVariables.GlobalVariables.FindFsmInt("PlayerMaxHealth_global").Value;
            AdjustHealth(amount);
        }

        //animate hearts based on whether health is increasing or decreasing
        public void AdjustHealth(int health)
        {
            int targetHealth = Mathf.Clamp(Player.Instance.MySheet.CurrentHealth + health, 0, Player.Instance.MySheet.MaxHealth);

            //FsmVariables.GlobalVariables.FindFsmInt("PlayerHealth_global").Value = targetHealth;
            if (targetHealth < Player.Instance.MySheet.CurrentHealth)
            {
                for (int i = targetHealth; i < Player.Instance.MySheet.CurrentHealth; i++)
                {
                    heartAnimators[i].SetTrigger("LoseHealth");
                }
            }
            else if (targetHealth > Player.Instance.MySheet.CurrentHealth)
            {
                for (int i = Player.Instance.MySheet.CurrentHealth; i < targetHealth; i++)
                {
                    if (!heartArray[i].activeInHierarchy) { heartArray[i].SetActive(true); }
                    else
                    {
                        //Debug.Log("send GainHealth trigger");
                        heartAnimators[i].SetTrigger("GainHealth");
                    }
                }
            }
            Player.Instance.MySheet.CurrentHealth = targetHealth;
        }
    }
}