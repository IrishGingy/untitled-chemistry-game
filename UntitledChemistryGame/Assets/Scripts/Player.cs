using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Indicates whether the player is docked (on foot) or not (in the boat)
    public bool _docked;
    public bool docked
    {
        get { return _docked; }
        set
        {
            _docked = value;
        }
    }
    public GameObject playerPrefab;
    public GameObject boat;

    private GameObject playerObject;

    // will want an inventory at some point that is a singleton

    // properties that would need to be applied to the next instantiated player character should go here

    /// <summary>
    /// Instantiate player gameobject to start playing as player, move boat to the side of the dock and set to inactive, set docked to true.
    /// </summary>
    public void DockBoat(Transform spawn, Transform dock)
    {
        playerObject = Instantiate(playerPrefab, spawn);
        boat.transform.position = dock.position;
        boat.SetActive(false);
        docked = true;
    }

    /// <summary>
    /// Undock boat to start playing as boat, destroy the player gameobject, set docked to false.
    /// </summary>
    public void SetSail()
    {
        boat.SetActive(true);
        Destroy(playerObject);
        docked = false;
    }
}
