using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : Trigger
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
    private bool canSleep;

    protected override void Start()
    {
        base.Start();
        gm = FindObjectOfType<GameManager>();
        canSleep = false;
    }

    private void Update()
    {
        if (canSleep && Input.GetKeyDown(promptKeyText))
        {
            gm.LoadNextScene();
        }
    }

    public override void TriggerEnterEvent()
    {
        canSleep = true;
    }

    public override void TriggerExitEvent()
    {
        canSleep = false;
    }
}
