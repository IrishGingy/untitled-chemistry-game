using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEditor.Build.Content;

public class FishingSpot : MonoBehaviour
{
    public GameObject fishingUI;

    public bool inRange = false;
    public float outlineThickness = 10.0f;

    private BoxCollider bCollider; 

    private void Awake()
    {
        // Change the icon of the objects with this script attached to green rounded rectangle
        var iconContent = EditorGUIUtility.IconContent("sv_label_3");
        EditorGUIUtility.SetIconForObject(gameObject, (Texture2D) iconContent.image);

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
