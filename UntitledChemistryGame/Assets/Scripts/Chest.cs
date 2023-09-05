using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Trigger
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
    public GameObject noteUI;
    
    private bool canOpen;

    protected override void Start()
    {
        base.Start();
        //gm = FindObjectOfType<GameManager>();
        canOpen = false;
    }

    private void Update()
    {
        if (canOpen && Input.GetKeyDown(promptKeyText))
        {
            // pick up item (notify it's in inventory or have way to inspect note?)
            //noteUI.SetActive(true);
            //base.HidePrompt();
            //base.noPrompt = true;
            Debug.Log("Inspect note...");
        }
    }

    public override void TriggerEnterEvent()
    {
        canOpen = true;
    }

    public override void TriggerExitEvent()
    {
        canOpen = false;
    }
}
