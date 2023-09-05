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
    [SerializeField] private EventDependency _eventDependency;
    public override EventDependency eventDependency
    {
        get { return _eventDependency; }
        set { _eventDependency = value; }
    }

    public bool canDock = false;
    public bool canSail = false;

    [Header("Quests")]
    public Quest noteQuest;
    public Quest prevQuest;

    private Player player;
    private GameManager gm;
    private bool questAdded = false;

    protected override void Start()
    {
        base.Start();
        eventDependency = this.GetComponent<EventDependency>();
        player = FindObjectOfType<Player>();
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canDock && !player.docked)
        {
            Docking();
        }
        if (canSail && player.docked)
        {
            Undocking();
        }
        // if questAdded = true, then don't allow docking
    }

    private void Docking()
    {
        // TODO: Convert to Unity's new input system
        if (Input.GetKeyDown(promptKeyText))
        {
            player.DockBoat(gm.spawnLocation, gm.boatDockLocation);
            gm.AddQuest(noteQuest, prevQuest);
            CheckEventDependency(EventDependency.Methods.dock);
            // switching from docked to undocked and vice versa requires that we hide the prompt to reset the promptTextMesh (to prevent error)
            //base.HidePrompt();
        }
    }

    private void Undocking()
    {
        // TODO: Convert to Unity's new input system
        if (Input.GetKeyDown(promptKeyText))
        {
            player.SetSail();
            CheckEventDependency(EventDependency.Methods.sail);
            //base.HidePrompt();
        }
    }

    private void CheckEventDependency(EventDependency.Methods curMethod)
    {
        // check if there is an event dependency on this gameObject.
        if (eventDependency && eventDependency.preventedMethod == curMethod)
        {
            if (eventDependency.boardPassenger)
            {
                player.BoardPassenger();
            }
            if (eventDependency.nextSceneTrigger)
            {
                gm.LoadNextScene();
                return;
            }
        }
    }

    public override void TriggerEnterEvent()
    {
        if (!player.docked)
        {
            canSail = false;

            if (!base.HasDependentDialogueBeenPlayed(EventDependency.Methods.dock))
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

            if (!base.HasDependentDialogueBeenPlayed(EventDependency.Methods.sail))
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
}
