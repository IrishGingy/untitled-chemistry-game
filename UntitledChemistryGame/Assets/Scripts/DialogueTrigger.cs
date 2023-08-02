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
    [SerializeField] private EventDependency _eventDependency;
    public override EventDependency eventDependency
    {
        get { return _eventDependency; }
        set { _eventDependency = value; }
    }

    [SerializeField] private bool canTriggerDialogue;
    [SerializeField] DialogueItem dI;

    private Camera shopCamera;
    private Player player;
    private PlayerController playerController;
    private GameManager gm;
    private DialogueManager dm;

    private void Awake()
    {
        canTriggerDialogue = false;
        shopCamera = transform.parent.GetComponentInChildren<Camera>();
        shopCamera.enabled = false;
        player = FindObjectOfType<Player>();
        dI.played = false;
        gm = FindObjectOfType<GameManager>();
        dm = gm.GetComponent<DialogueManager>();
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

                // if the dependent dialogue item hasn't been played, a placeholder dialogue is instead played.
                if (dI.dependency && !dI.dependency.played)
                {
                    dm.PlayPlaceholderDialogue();
                    return;
                }
                dm.PlayDialogue(dI);
            }
        }
        else if (player.inDialogue && !dm.dialogueIsPlaying)
        {
            //dm.StopDialogue();
            dm.dialogueIsPlaying = false;
            DisableShopCamera();
            base.HidePrompt();
            //if (Input.GetKeyDown(promptKeyText))
            //{
            //    DisableShopCamera();
            //    base.HidePrompt();
            //}
        }
    }

    private void EnableShopCamera()
    {
        Debug.Log("Call from EnableShopCamera by " + this);
        shopCamera.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        player.playerObject.SetActive(false);
        player.inDialogue = true;
    }

    private void DisableShopCamera()
    {
        //player.playerObject.GetComponentInChildren<PlayerController>().enabled = true;
        player.playerObject.SetActive(true);
        shopCamera.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
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
