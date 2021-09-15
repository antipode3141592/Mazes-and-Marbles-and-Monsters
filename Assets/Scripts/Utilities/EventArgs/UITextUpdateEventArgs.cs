using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITextUpdate : EventArgs
{
    public string Message;

    public UITextUpdate(string message)
    {
        Message = message;
    }

}
