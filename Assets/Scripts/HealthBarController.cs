using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    public GameObject[] heartArray;
    public int playerMaxHealth;
    public int playerCurrentHealth;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetHealth()
    {
        playerCurrentHealth = playerMaxHealth;
        //activate heart objects equal to max health
        for (int i = 0; i < playerMaxHealth; i++)
        {
            heartArray[i].SetActive(true);
            heartArray[i].GetComponent<Animator>().Play("FullHeart");   
        }
    }

    public void AdjustHealth(int health)
    {
        int targerHealth = Mathf.Clamp(playerCurrentHealth + health, 0, playerMaxHealth);
        Animator animator;
        for (int i = targerHealth; i < playerCurrentHealth; i++)
        {
            animator = heartArray[i].GetComponent<Animator>();
            animator.SetTrigger("LoseHealth");
        }
        playerCurrentHealth = targerHealth;

    }
}
