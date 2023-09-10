using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public GameObject prerequisiteTrigger;

    private Collider triggerCollider;
    private GameManager gm;
    private bool eventPlayed;
    //private SceneLoader loader;
    private AudioSource trapDoorAudio;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        //loader = gm.GetComponent<SceneLoader>();
        triggerCollider = GetComponent<BoxCollider>();
        triggerCollider.enabled = true;
        prerequisiteTrigger.SetActive(false);
        trapDoorAudio = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!eventPlayed && other.CompareTag("Player"))
        {
            eventPlayed = true;
            trapDoorAudio.Play();
            // turn off current collider, turn on trapdoor collider, (turn player around?)
            triggerCollider.enabled = false;
            prerequisiteTrigger.SetActive(true);
        }
    }
}
