using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManagement;
using MarblesAndMonsters;

public class Player : BoardMovable<Player>
{
    public ParticleSystem hitEffect;
    public ParticleSystem healEffect;
    public ParticleSystem treasureEffect;
    public ParticleSystem addMaxHealthEffect;

    
    private List<InventoryItem> inventory;

    public List<InventoryItem> Inventory => inventory;//read only accessor
    //HealthBarController healthBarController;


    private void Awake()
    {
        inventory = new List<InventoryItem>();
        //healthBarController = GameObject.FindObjectOfType<HealthBarController>();
    }

    public void AdjustHealthUI(int health)
    {
        GameMenu.Instance.UpdateHealth(health);
    }

    public void RestetHealthUI()
    {
        GameMenu.Instance.ResetHealth();
    }

    public void IsHitEffectParticles()
    {
        //Debug.Log("Fire the particles!");
        hitEffect.Play();
    }

    public void IsHealedEffectParticles()
    {
        //Debug.Log("Fire the particles... of HEALING!");
        healEffect.Play();
    }

    public void PlayTreasureParticles()
    {
        treasureEffect.Play();
    }

    public void AddMaxHealthUI(int amount)
    {
        GameMenu.Instance.AddMaxHealthUI(amount);
    }

    public void PlayAddMaxHealthParticles()
    {
        Debug.Log("Fire the particles of player max health increase!");
    }

    public void AddItemToInventory(InventoryItem itemToAdd)
    {
        inventory.Add(itemToAdd);
        GameMenu.Instance.AddItemToInventory(itemToAdd);
        Debug.Log("Added a " + itemToAdd.name + "to inventory!");
    }

    public void RemoveItemFromInventory(InventoryItem itemToRemove)
    {
        inventory.Remove(itemToRemove);
        GameMenu.Instance.RemoveItemFromInventory(itemToRemove);
    }
}
