using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FiniteStateMachine;
using TMPro;

public class UIStateObserver : MonoBehaviour
{
    private StateMachine stateMachine;
    [SerializeField]
    private TextMeshProUGUI text;

    private void Awake()
    {
        stateMachine = GetComponentInParent<StateMachine>();
        //text = GetComponent<TextMeshProUGUI>();
        if (stateMachine)
        {
            stateMachine.OnStateChange += UpdateText;
        }
    }

    private void OnDestroy()
    {
        stateMachine.OnStateChange -= UpdateText;
    }

    public void UpdateText(object sender, UITextUpdate e)
    {
        text.SetText(e.Message, true);
        //text.text = e.Message;
    }
}
