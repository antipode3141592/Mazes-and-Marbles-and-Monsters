using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelManagement;

public class Player : MonoBehaviour
{
    public ParticleSystem hitEffect;
    public ParticleSystem healEffect;
    public ParticleSystem treasureEffect;
    //HealthBarController healthBarController;


    private void Awake()
    {
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
}
