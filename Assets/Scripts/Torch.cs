using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace MarblesAndMonsters.Objects
{

    public class Torch : MonoBehaviour
    {
        [SerializeField]
        private bool initialState = false;
        [SerializeField]
        private float litIntensity = .9f;
        [SerializeField]
        private float unlitIntensity = 0.2f;

        private Light2D lightObject;
        private Animator animationController;

        private void Awake()
        {
            lightObject = gameObject.GetComponent<Light2D>();
            animationController = gameObject.GetComponent<Animator>();
            
        }

        private void Start()
        {
            if (initialState)
            {
                lightObject.intensity = litIntensity;
                animationController.SetBool("initialState", true);
            } else
            {
                lightObject.intensity = unlitIntensity;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other != null && other.gameObject.CompareTag("Player"))
            {
                lightObject.intensity = litIntensity;
                animationController.SetTrigger("Light");
            }

        }
    }
}