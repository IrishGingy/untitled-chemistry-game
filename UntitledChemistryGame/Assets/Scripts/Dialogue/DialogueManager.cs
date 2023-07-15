using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] public bool dialogueIsPlaying;
    [SerializeField] public DialogueItem placeholderDI;

    MyInkScript inkScript;

    private void Awake()
    {
        inkScript = GetComponent<MyInkScript>();
    }

    public void PlayDialogue(DialogueItem dI)
    {
        if (!dialogueIsPlaying && !dI.played)
        {
            dI.played = true;
            dialogueIsPlaying = true;
            inkScript.StartDialogue(dI.inkFile);
            //Debug.Log(string.Format("<color=#{0:X2}{1:X2}{2:X2}>{3}</color>", (byte)(Color.green.r * 255f), (byte)(Color.green.g * 255f), (byte)(Color.green.b * 255f), "Playing Ink file..."));
        }
    }

    public void StopDialogue()
    {
        dialogueIsPlaying = false;
        // give control back to the player
    }

    public void PlayPlaceholderDialogue()
    {
        if (!dialogueIsPlaying)
        {
            dialogueIsPlaying = true;
            inkScript.StartDialogue(placeholderDI.inkFile, true);
        }
    }
}
