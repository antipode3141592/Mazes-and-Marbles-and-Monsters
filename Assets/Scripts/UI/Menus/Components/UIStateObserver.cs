using FiniteStateMachine;
using MarblesAndMonsters.Characters;
using TMPro;
using UnityEngine;

public class UIStateObserver : MonoBehaviour
{
    private CharacterStateMachine stateMachine;
    [SerializeField]
    private TextMeshProUGUI text;

    private void Awake()
    {
        //var character = GetComponentInParent<CharacterControl>();
        stateMachine.OnStateChange += UpdateText;
    }

    private void OnDestroy()
    {
        stateMachine.OnStateChange -= UpdateText;
    }

    public void UpdateText(object sender, UITextUpdate e)
    {
        text.SetText(e.Message, true);
    }
}
