using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Indicates whether the player is docked (on foot) or not (in the boat)
    [SerializeField] private bool _docked;
    public bool docked
    {
        get { return _docked; }
        set
        {
            _docked = value;
        }
    }
    [SerializeField] private bool _inDialogue;
    public bool inDialogue
    {
        get { return _inDialogue; }
        set
        {
            _inDialogue = value;
        }
    }

    public GameObject playerPrefab;
    public GameObject boatCam;
    [SerializeField] private GameObject boat;
    [SerializeField] public Trigger trigger;

    [Header("Don't set")]
    public GameObject playerObject;

    private Rigidbody boatRb;
    private Collider boatCollider;
    // TODO: Fix it so this is only one boat controller
    private SimpleBoatController boatController;
    private WaterBoat boatController2;
    private GameObject boatParts;

    // will want an inventory at some point that is a singleton

    // properties that would need to be applied to the next instantiated player character should go here

    private void Start()
    {
        boatRb = boat.GetComponent<Rigidbody>();
        boatCollider = boat.GetComponent<Collider>();
        boatController = boat.GetComponent<SimpleBoatController>();
        boatController2 = boat.GetComponent<WaterBoat>();
        // these parts include the camera and trigger area around boat (both not necessary after docking the boat).
        boatParts = boat.transform.GetChild(0).gameObject;
    }

    /// <summary>
    /// Instantiate player gameobject to start playing as player, move boat to the side of the dock and set to inactive, set docked to true.
    /// </summary>
    public void DockBoat(Transform spawn, Transform dock)
    {
        playerObject = Instantiate(playerPrefab, spawn);
        // TODO: Figure out why PlayerController is being set to false on instantiation
        playerObject.GetComponentInChildren<PlayerController>().enabled = true;
        boat.transform.position = dock.position;
        boatRb.velocity = Vector3.zero;
        boatCollider.enabled = false;
        boatController.enabled = false;
        boatController2.enabled = false;
        boatCam.SetActive(false);
        boatParts.SetActive(false);
        docked = true;
        trigger.HidePrompt();
    }

    /// <summary>
    /// Undock boat to start playing as boat, destroy the player gameobject, set docked to false.
    /// </summary>
    public void SetSail()
    {
        boatCollider.enabled = true;
        boatRb.velocity = Vector3.zero;
        boatController.enabled = true;
        boatController2.enabled = true;
        boatCam.SetActive(true);
        boatParts.SetActive(true);
        Destroy(playerObject);
        docked = false;
        trigger.HidePrompt();
    }

    /// <summary>
    /// Set 'carryingPassenger' to true on the boat controller.
    /// </summary>
    public void BoardPassenger()
    {
        boatController.PlayPassengerDialogue();
    }
}
