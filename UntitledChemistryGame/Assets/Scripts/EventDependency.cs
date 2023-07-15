using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDependency : MonoBehaviour
{
    public enum Methods
    {
        sail,
        dock
    }

    public DialogueItem dependentDialogue;
    public Methods preventedMethod;
}
