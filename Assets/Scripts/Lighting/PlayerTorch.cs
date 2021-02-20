using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerTorch : MonoBehaviour, IAdjustableLight2D
{
    private Light2D light2D;

    public float Intensity { get { return light2D.intensity; } }

    public void Awake()
    {
        light2D = GetComponent<Light2D>();
    }

    public void AdjustLight(float intensity, Color color)
    {
        light2D.intensity = intensity;
        light2D.color = color;
    }
}
