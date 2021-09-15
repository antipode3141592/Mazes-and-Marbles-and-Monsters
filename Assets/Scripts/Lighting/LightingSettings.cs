using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarblesAndMonsters.Lighting
{
    [CreateAssetMenu(fileName = "LightingSettings", menuName = "LightSettings")]
    public class LightingSettings : ScriptableObject
    {
        public float GlobalLightOnIntensity;
        public float PlayerLightOnIntensity;
        public float GlobalLightOffIntensity;

        public Color GlobalLightOnColor;
        public Color PlayerLightOnColor;
        public Color GlobalLightOffColor;
    }
}