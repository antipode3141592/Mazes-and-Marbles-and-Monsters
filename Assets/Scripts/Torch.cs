using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace MarblesAndMonsters.Objects
{

    public class Torch : MonoBehaviour
    {
        [SerializeField]
        private bool initialState = false;

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
                animationController.SetBool("initialState", true);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other != null && other.gameObject.CompareTag("Player"))
            {
                //lightObject.intensity = 1.0f;
                animationController.SetTrigger("Light");
            }

        }
    }
}