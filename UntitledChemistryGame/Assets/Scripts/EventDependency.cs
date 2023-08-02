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
    /// <summary>
    /// The method that the dependentDialogue is preventing.
    /// </summary>
    public Methods preventedMethod;
    /// <summary>
    /// Determines whether an event dependency's completion will result in boarding a passenger.
    /// </summary>
    public bool boardPassenger;
    /// <summary>
    /// Determines whether an event dependency's completion will result in loading the next scene.
    /// </summary>
    public bool nextSceneTrigger;
}
