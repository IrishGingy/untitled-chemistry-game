using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bookshelf : Trigger
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

    public GameObject book;
    public GameObject prerequisiteTrigger;

    private GameManager gm;
    private bool canPlaceBook;

    protected override void Start()
    {
        base.Start();
        gm = FindObjectOfType<GameManager>();
        canPlaceBook = false;
        book.SetActive(false);
        prerequisiteTrigger.SetActive(false);
    }

    private void Update()
    {
        if (canPlaceBook && Input.GetKeyDown(promptKeyText))
        {
            Debug.Log("Place Book!");
            book.SetActive(true);
            base.HidePrompt();
            base.noPrompt = true;
            prerequisiteTrigger.SetActive(true);
        }
    }

    public override void TriggerEnterEvent()
    {
        canPlaceBook = true;
    }

    public override void TriggerExitEvent()
    {
        canPlaceBook = false;
    }
}
