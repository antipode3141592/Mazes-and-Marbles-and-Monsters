using MarblesAndMonsters;
using UnityEngine;


public class PlayerTorch : MonoBehaviour, IAdjustableLight2D
{
    private UnityEngine.Rendering.Universal.Light2D light2D;

    public float Intensity { get { return light2D.intensity; } }

    public void Awake()
    {
        light2D = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
    }

    public void AdjustLight(float intensity, Color color)
    {
        light2D.intensity = intensity;
        light2D.color = color;
    }
}
