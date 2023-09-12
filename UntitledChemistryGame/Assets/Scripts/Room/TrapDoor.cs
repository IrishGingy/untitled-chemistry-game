using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : Trigger
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
    private GameManager gm;
    private bool canGoDownstairs;

    protected override void Start()
    {
        base.Start();
        gm = FindObjectOfType<GameManager>();
        canGoDownstairs = false;
    }

    private void Update()
    {
        if (canGoDownstairs && Input.GetKeyDown(promptKeyText))
        {
            base.HidePrompt();
            gm.LoadNextScene();
        }
    }

    public override void TriggerEnterEvent()
    {
        canGoDownstairs = true;
    }

    public override void TriggerExitEvent()
    {
        canGoDownstairs = false;
    }
}
