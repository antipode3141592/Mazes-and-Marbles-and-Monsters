using UnityEngine;


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

        private UnityEngine.Rendering.Universal.Light2D lightObject;
        private Animator animationController;
        private int aInitialState;
        private int aLight;

        private void Awake()
        {
            lightObject = gameObject.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
            animationController = gameObject.GetComponent<Animator>();
            aInitialState = Animator.StringToHash("initialState");
            aLight = Animator.StringToHash("Light");
        }

        private void Start()
        {
            if (initialState)
            {
                lightObject.intensity = litIntensity;
                animationController.SetBool(aInitialState, true);
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
                animationController.SetTrigger(aLight);
            }

        }
    }
}