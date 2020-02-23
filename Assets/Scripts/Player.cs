using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ParticleSystem hitEffect;
    public ParticleSystem healEffect;
    HealthBarController healthBarController;


    private void Awake()
    {
        healthBarController = GameObject.FindObjectOfType<HealthBarController>();
    }

    public void AdjustHealthUI(int health)
    {
        healthBarController.AdjustHealth(health);
    }

    public void RestetHealthUI()
    {
        healthBarController.ResetHealth();
    }

    public void IsHitEffectParticles()
    {
        Debug.Log("Fire the particles!");
        hitEffect.Play();
    }

    public void IsHealedEffectParticles()
    {
        Debug.Log("Fire the particles... of HEALING!");
    }
}
