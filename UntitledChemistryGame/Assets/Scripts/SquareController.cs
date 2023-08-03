using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class SquareController : MonoBehaviour
{
    private Camera mainCamera;
    private float camZDistance;
    private FishingManager fm;

    // Start is called before the first frame update
    void Start()
    {
        fm = FindObjectOfType<FishingManager>();
        mainCamera = Camera.main;
        camZDistance = mainCamera.WorldToScreenPoint(transform.position).z; // z axis of the game object for screen view
    }

    private void OnMouseDrag()
    {
        // square can only be dragged if we are in the second phase of fishing
        if (fm.currentPhase == 1)
        {
            Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, camZDistance); // z axis to screen point
            Vector3 newWorldPosition = mainCamera.ScreenToWorldPoint(screenPosition); // screen point converted to world point
            transform.position = newWorldPosition;
        }
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    //Vector3 mouse = Camera.main.ScreenToViewportPoint(Input.mousePosition);
    //    transform.position = Input.mousePosition;
    //}
}
