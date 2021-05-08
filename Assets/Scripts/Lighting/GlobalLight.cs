using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GlobalLight : MonoBehaviour, IAdjustableLight2D
{
    private Light2D light2D;

    [SerializeField]
    private bool initialState = false;

    [SerializeField]
    private float litIntensity = 1.0f;
    [SerializeField]
    private float unlitIntensity = 0.1f;

    public float Intensity { get { return light2D.intensity; } }

    protected void Awake()
    {
        light2D = GetComponent<Light2D>();
        if (initialState)
        {
            light2D.intensity = litIntensity;
        } else
        {
            light2D.intensity = unlitIntensity;
        }
    }

    public void AdjustLight(float intensity, Color color)
    {
        light2D.intensity = intensity;
        light2D.color = color;
    }
}

public interface IAdjustableLight2D
{
    public void AdjustLight(float intensity, Color color);
}
