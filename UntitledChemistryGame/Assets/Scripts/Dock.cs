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
    public Transform spawnLocation;
    public Transform boatDockLocation;

    private Player player;

    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canDock && !player.docked)
        {
            // TODO: Convert to Unity's new input system
            if (Input.GetKeyDown(promptKeyText))
            {
                player.DockBoat(spawnLocation, boatDockLocation);
                // switching from docked to undocked and vice versa requires that we hide the prompt to reset the promptTextMesh (to prevent error)
                base.HidePrompt();
            }
        }
        if (canSail && player.docked)
        {
            // TODO: Convert to Unity's new input system
            if (Input.GetKeyDown(promptKeyText))
            {
                player.SetSail();
                base.HidePrompt();
            }
        }
    }

    public override void TriggerEnterEvent()
    {
        if (!player.docked)
        {
            canDock = true;
            canSail = false;
        }
        else
        {
            canDock = false;
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
