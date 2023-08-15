using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public abstract class Trigger : MonoBehaviour
{
    public abstract KeyCode promptKeyText { get; set; }
    public abstract EventDependency eventDependency { get; set; }

    private GameManager gm;
    private GameObject buttonPrompt;
    private TextMeshProUGUI promptKeyTextMesh;

    protected virtual void Start()
    {
        gm = FindObjectOfType<GameManager>();
        buttonPrompt = gm.buttonPrompt;
        promptKeyTextMesh = buttonPrompt.GetComponentInChildren<TextMeshProUGUI>();
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        // TODO: Change this to detect a like script called "Player" instead of checking for tags
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player has entered!");
            ShowPrompt();
            // call derived class's "TriggerEnterEvent" method that overrides this class's "TriggerEnterEvent" method
            TriggerEnterEvent();
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        // TODO: Change this to detect a like script called "Player" instead of checking for tags
        if (other.CompareTag("Player"))
        {
            HidePrompt();
            // call derived class's "TriggerExitEvent" method that overrides this class's "TriggerExitEvent" method
            TriggerExitEvent();
        }
    }

    private void ShowPrompt()
    {
        if (promptKeyTextMesh.text != "")
        {
            //Debug.LogError($"There are two triggers trying to change the text of the button prompt at the same time. Errored on '{this.name}'," +
            //    $" called by '{this.GetType()}'");
        }
        promptKeyTextMesh.text = this.promptKeyText.ToString();
        buttonPrompt.SetActive(true);
    }

    public void HidePrompt()
    {
        promptKeyTextMesh.text = "";
        buttonPrompt.SetActive(false);
    }

    public bool HasDependentDialogueBeenPlayed(EventDependency.Methods method)
    {
        eventDependency = this.eventDependency;
        if (!eventDependency || eventDependency.preventedMethod != method)
        {
            // dependent dialogue for this event doesn't exist, so the event is allowed to occur.
            return true;
        }
        else
        {
            if (!eventDependency.dependentDialogue.played)
            {
                // dependent dialogue exists for this event, and has not been played yet: prevent event from occurring.
                return false;
            }
            else
            {
                // dependent dialogue has been played: allow event to occur.
                return true;
            }
        }
    }

    // The "abstract" keyword here requires derived classes to implement these methods
    public abstract void TriggerEnterEvent();
    public abstract void TriggerExitEvent();
}
