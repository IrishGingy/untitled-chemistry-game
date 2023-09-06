using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestOnLoad : Trigger
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
    public bool noPrompt;
    public Quest noteQuest;
    public Quest roomQuest;
    //public DialogueItem dialogue;
    //public GameObject prerequisiteTrigger;
    //public GameObject book;

    private GameManager gm;
    private DialogueManager dm;
    private bool givenQuest;
    private PlayerController controller;

    protected override void Start()
    {
        base.Start();
        gm = FindObjectOfType<GameManager>();
        //dm = gm.gameObject.GetComponent<DialogueManager>();
        ////controller = gm.player.GetComponentInChildren<PlayerController>();
        //prerequisiteTrigger.SetActive(false);
        //book.SetActive(false);
    }

    //private void Update()
    //{
    //    if (canTriggerDialogue && Input.GetKeyDown(promptKeyText))
    //    {
    //        dm.PlayDialogue(dialogue);
    //        //controller.enabled = false;
    //        base.HidePrompt();
    //        base.noPrompt = true;
    //        prerequisiteTrigger.SetActive(true);
    //        book.SetActive(true);
    //    }
    //}

    public override void TriggerEnterEvent()
    {
        base.HidePrompt();
        if (!givenQuest)
        {
            givenQuest = true;
            gm.AddQuest(roomQuest, noteQuest);
        }
    }

    public override void TriggerExitEvent()
    {
        
    }
}
