using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dock : MonoBehaviour
{
    public bool canDock = false;
    public GameObject player;
    public GameObject boat;
    public Transform spawnLocation;

    // Update is called once per frame
    void Update()
    {
        if (canDock && Input.GetKeyDown(KeyCode.Q)) 
        {
            Instantiate(player, spawnLocation);
            boat.SetActive(false);
            canDock = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canDock = true;
        }
    }
}
