using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class FishingSpot : MonoBehaviour
{
    public GameObject fishingUI;

    public bool inRange = false;
    public float outlineThickness = 10.0f;

    private BoxCollider bCollider; 

    private void Awake()
    {
        bCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            fishingUI.SetActive(true);
        }

        if (fishingUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            fishingUI.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (inRange)
        {
            Gizmos.DrawWireCube(transform.position, bCollider.size);
        }
    }
}
