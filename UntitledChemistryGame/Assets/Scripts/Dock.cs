using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dock : Trigger
{
    [SerializeField] private KeyCode _promptKeyText;
    public override KeyCode promptKeyText
    {
        get { return _promptKeyText; }
        set { _promptKeyText = value; }
    }

    public bool canDock = false;
    public bool canSail = false;

    private Player player;
    private GameManager gm;
    /// <summary>
    /// A DialogueItem that must be played in order to dock or set sail.
    /// </summary>
    private EventDependency eventDependency;

    protected override void Start()
    {
        base.Start();
        eventDependency = GetComponent<EventDependency>();
        player = FindObjectOfType<Player>();
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canDock && !player.docked)
        {
            // TODO: Convert to Unity's new input system
            if (Input.GetKeyDown(promptKeyText))
            {
                player.DockBoat(gm.spawnLocation, gm.boatDockLocation);
                // switching from docked to undocked and vice versa requires that we hide the prompt to reset the promptTextMesh (to prevent error)
                //base.HidePrompt();
            }
        }
        if (canSail && player.docked)
        {
            // TODO: Convert to Unity's new input system
            if (Input.GetKeyDown(promptKeyText))
            {
                // check to see if we are taking a passenger with us (this depends on an event dependency's 'boardPassenger' field).
                if (eventDependency && eventDependency.boardPassenger)
                {
                    player.BoardPassenger();
                }
                player.SetSail();
                //base.HidePrompt();
            }
        }
    }

    public override void TriggerEnterEvent()
    {
        if (!player.docked)
        {
            canSail = false;

            if (!HasDependentDialogueBeenPlayed(EventDependency.Methods.dock))
            {
                canDock = false;
                base.HidePrompt();
                return;
            }

            canDock = true;
        }
        else
        {
            canDock = false;

            if (!HasDependentDialogueBeenPlayed(EventDependency.Methods.sail))
            {
                canSail = false;
                base.HidePrompt();
                return;
            }
            canSail = true;
        }
    }

    public override void TriggerExitEvent() 
    {
        if (!player.docked)
        {
            canDock = false;
        }
        else
        {
            canSail = false;
        }
    }


    private bool HasDependentDialogueBeenPlayed(EventDependency.Methods method)
    {
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
}
