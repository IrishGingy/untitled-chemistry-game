using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRug : Trigger
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
    public Transform rugTransform;
    public Transform movedRugParent;
    public GameObject prerequisiteTrigger;

    private GameManager gm;
    private bool rugMoved;
    private bool trapDoorOpen;
    private bool canInteract;

    protected override void Start()
    {
        base.Start();
        gm = FindObjectOfType<GameManager>();
        canInteract = false;
        rugMoved = false;
        prerequisiteTrigger.SetActive(false);
        //trapDoorOpen = false;
    }

    private void Update()
    {
        if (canInteract && !rugMoved && Input.GetKeyDown(promptKeyText))
        {
            rugMoved = true;
            // move the rug
            rugTransform.parent = movedRugParent;
            rugTransform.localPosition = Vector3.zero;
            rugTransform.Rotate(0, 90, 0);
            prerequisiteTrigger.SetActive(true);
            gameObject.SetActive(false);
            //rugTransform.position = new Vector3(rugTransform.position.x + 16, rugTransform.position.y, rugTransform.position.z);
        }
        //else if (!trapDoorOpen && canInteract && rugMoved && Input.GetKeyDown(promptKeyText)) 
        //{
        //    // open trapdoor
        //    trapDoorOpen = true;
        //    canInteract = false;
        //    gm.LoadNextScene();
        //}
    }

    public override void TriggerEnterEvent()
    {
        canInteract = true;
    }

    public override void TriggerExitEvent()
    {
        canInteract = false;
    }
}
