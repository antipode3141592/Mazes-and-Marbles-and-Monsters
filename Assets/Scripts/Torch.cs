﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Torch : MonoBehaviour
{
    private Light2D lightObject;
    private Animator animationController;

    private void Awake()
    {
        lightObject = gameObject.GetComponent<Light2D>();
        animationController = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.gameObject.CompareTag("Player"))
        {
            lightObject.intensity = 1.0f;
            animationController.SetTrigger("Light");
        }
        
    }


}
