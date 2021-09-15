using MarblesAndMonsters.Lighting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using LightingSettings = MarblesAndMonsters.Lighting.LightingSettings;

namespace MarblesAndMonsters
{
    public class GlobalLight : MonoBehaviour, IAdjustableLight2D
    {
        private Light2D light2D;

        [SerializeField]
        private LightingSettings lightingSettings;

        [SerializeField]
        private bool initialState = false;

        public float Intensity { get { return light2D.intensity; } }

        protected void Awake()
        {
            light2D = GetComponent<Light2D>();
            if (initialState)
            {
                AdjustLight(lightingSettings.GlobalLightOnIntensity, lightingSettings.GlobalLightOnColor);
            }
            else
            {
                AdjustLight(lightingSettings.GlobalLightOffIntensity, lightingSettings.GlobalLightOffColor);
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
}