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
    public GameObject playerParent;
    public GameObject boat;
    public Transform spawnLocation;
    //public TextMeshProUGUI canDockTextMesh;

    private bool docked;

    protected override void Start()
    {
        base.Start();
        docked = playerParent.GetComponentInChildren<PlayerController>().docked;
        //canDockTextMesh.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canDock && !docked)
        {
            InstantiatePlayer();
        }
        if (canSail && docked)
        {
            InstantiateBoat();
        }
        //if (canDock && Input.GetKeyDown(KeyCode.Q))
        //{
        //    Instantiate(playerParent, spawnLocation);
        //    boat.SetActive(false);
        //    canDock = false;
        //    docked = true;
        //    canDockTextMesh.enabled = false;
        //}
    }

    private void InstantiatePlayer()
    {
        if (Input.GetKeyDown(promptKeyText))
        {
            Instantiate(playerParent, spawnLocation);
            boat.SetActive(false);
            canDock = false;
            docked = true;
            base.HidePrompt();
        }
    }

    private void InstantiateBoat()
    {
        //if (Input.GetKeyDown(promptKeyText))
        //{
        //    Instantiate(playerParent, spawnLocation);
        //    boat.SetActive(false);
        //    canDock = false;
        //    docked = true;
        //    base.HidePrompt();
        //}
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player") && !docked)
    //    {
    //        canDock = true;
    //        canDockTextMesh.enabled = true;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player") && !docked)
    //    {
    //        canDockTextMesh.enabled = false;
    //    }
    //}

    public override void TriggerEnterEvent()
    {
        if (!docked)
        {
            canDock = true;
        }
        else
        {
            canSail = true;
        }
        Debug.Log("Called the trigger enter event from Dock!");
    }

    public override void TriggerExitEvent() 
    {
        if (!docked)
        {
            canDock = false;
        }
        else
        {
            canSail = false;
        }
        Debug.Log("Called the trigger exit event from Dock!");
    }
}
