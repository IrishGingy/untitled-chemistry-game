using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Trigger
{
    [SerializeField] private KeyCode _promptKeyText;
    public override KeyCode promptKeyText
    {
        get { return _promptKeyText; }
        set { _promptKeyText = value; }
    }

    public override void TriggerEnterEvent()
    {
        Debug.Log("Called the trigger event from Dialogue Trigger!");
    }

    public override void TriggerExitEvent()
    {
        Debug.Log("exiting!!!");
    }
}
