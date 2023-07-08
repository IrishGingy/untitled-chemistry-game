using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Trigger
{
    [SerializeField] private KeyCode _promptKeyText;
    public override KeyCode promptKeyText
    {
        get { return _promptKeyText; }
        set { _promptKeyText = value; }
    }

    [SerializeField] private bool canTriggerDialogue;
    
    private Camera shopCamera;
    private Player player;
    private PlayerController playerController;

    private void Awake()
    {
        canTriggerDialogue = false;
        shopCamera = transform.parent.GetComponentInChildren<Camera>();
        shopCamera.enabled = false;
        player = FindObjectOfType<Player>();
        //playerController = player.playerPrefab.GetComponentInChildren<PlayerController>();
    }

    private void Update()
    {
        if (canTriggerDialogue && !player.inDialogue)
        {
            if (Input.GetKeyDown(promptKeyText))
            {
                // Change player camera
                EnableShopCamera();
                base.HidePrompt();
            }
        }
        else if (player.inDialogue)
        {
            if (Input.GetKeyDown(promptKeyText))
            {
                DisableShopCamera();
                base.HidePrompt();
            }
        }
    }

    private void EnableShopCamera()
    {
        Debug.Log("Call from EnableShopCamera by " + this);
        shopCamera.enabled = true;
        player.playerObject.SetActive(false);
        player.inDialogue = true;
    }

    private void DisableShopCamera()
    {
        //player.playerObject.GetComponentInChildren<PlayerController>().enabled = true;
        player.playerObject.SetActive(true);
        shopCamera.enabled = false;
        player.inDialogue = false;
    }

    public override void TriggerEnterEvent()
    {
        //Debug.Log("Called the trigger event from Dialogue Trigger!");
        canTriggerDialogue = true;
    }

    public override void TriggerExitEvent()
    {
        //Debug.Log("exiting!!!");
        canTriggerDialogue = false;
    }
}
