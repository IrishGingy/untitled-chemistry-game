using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dock : Trigger
{
    [SerializeField] private char _promptKeyText;
    public override char promptKeyText
    {
        get { return _promptKeyText; }
        set { _promptKeyText = value; }
    }

    public bool canDock = false;
    public GameObject playerParent;
    public GameObject boat;
    public Transform spawnLocation;
    public TextMeshProUGUI canDockTextMesh;

    private bool docked;

    //private void Start()
    //{
    //    docked = playerParent.GetComponentInChildren<PlayerController>().docked;
    //    canDockTextMesh.enabled = false;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (canDock && Input.GetKeyDown(KeyCode.Q)) 
    //    {
    //        Instantiate(playerParent, spawnLocation);
    //        boat.SetActive(false);
    //        canDock = false;
    //        docked = true;
    //        canDockTextMesh.enabled = false;
    //    }
    //}

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
        Debug.Log("Called the trigger event from Dock!");
    }

    public override void TriggerExitEvent() { }
}
