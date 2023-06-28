using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Current : MonoBehaviour
{
    public GameObject playerObject;
    public float currentForce;

    [SerializeField] private Nullable<float> difference;

    private SimpleBoatController boatController;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        boatController = playerObject.GetComponent<SimpleBoatController>();
        rb = playerObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 direction = transform.position - rb.position;
        if (difference.HasValue && difference < 0)
        {
            rb.AddForce((Vector3)(direction.normalized * Mathf.Abs((float)difference)), ForceMode.Acceleration);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            difference = null;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!difference.HasValue)
            {
                difference = boatController.maxMoveSpeed - currentForce;
            }
        }
    }
}
