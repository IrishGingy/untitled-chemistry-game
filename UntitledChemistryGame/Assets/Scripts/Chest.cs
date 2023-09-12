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
    // trigger that is disabled until this trigger has been completed
    public GameObject prerequisiteTrigger;
    // this is the page we are adding notes to
    public PageContent pageToUpdate;
    public Quest noteQuest;
    
    private bool canOpen;
    private GameManager gm;

    protected override void Start()
    {
        base.Start();
        prerequisiteTrigger.SetActive(false);
        //gm = FindObjectOfType<GameManager>();
        canOpen = false;
        gm = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (canOpen && Input.GetKeyDown(promptKeyText))
        {
            // pick up item (notify it's in inventory or have way to inspect note?)
            //noteUI.SetActive(true);
            //base.HidePrompt();
            //base.noPrompt = true;
            // show book notification with page number
            gm.ShowBookNotification();
            // update the page content scriptableobject description of the third page
            pageToUpdate.description = "The peach bearded fish is a marvelous specimen. Beautiful to look at, but its sharp teeth make it " +
                "quite the predator. Interestingly, this fish doesn't like to show off its beard, and is usually found secluded in narrow passageways without much traffic.";
            // update the actual page in the book (UpdateUI which could be done here and/or when "B" is pressed)
            prerequisiteTrigger.SetActive(true);
            gm.qm.CompleteQuest(noteQuest);
            //base.HidePrompt();
            //base.noPrompt = true;
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
