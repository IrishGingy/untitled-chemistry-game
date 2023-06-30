using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dock : MonoBehaviour
{
    public bool canDock = false;
    public GameObject playerParent;
    public GameObject boat;
    public Transform spawnLocation;
    public TextMeshProUGUI canDockTextMesh;

    private bool docked;

    private void Start()
    {
        docked = playerParent.GetComponentInChildren<PlayerController>().docked;
        canDockTextMesh.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canDock && Input.GetKeyDown(KeyCode.Q)) 
        {
            Instantiate(playerParent, spawnLocation);
            boat.SetActive(false);
            canDock = false;
            docked = true;
            canDockTextMesh.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !docked)
        {
            canDock = true;
            canDockTextMesh.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !docked)
        {
            canDockTextMesh.enabled = false;
        }
    }
}
