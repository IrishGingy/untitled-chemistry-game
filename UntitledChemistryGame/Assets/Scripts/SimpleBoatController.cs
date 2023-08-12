using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleBoatController : MonoBehaviour
{
    [SerializeField] public float maxMoveSpeed;
    [SerializeField] public bool carryingPassenger;
    [SerializeField] public DialogueItem passengerDialogue;
    //[SerializeField] public GameObject boatCam;
    //[SerializeField] public Vector3 cameraOffset = new Vector3(0f, 2f, -5f);
    //[SerializeField] public float rotationSpeed = 5f;

    [SerializeField] private Vector3 _rotation;
    [SerializeField] private float _speed;
    [SerializeField] private float _velocity;
    [SerializeField] private Vector3 _vectorVelocity;

    private Rigidbody _rb;
    //private float mouseX, mouseY;

    private void Awake()
    {
        //boatCam.SetActive(true);
        _rb = GetComponent<Rigidbody>();
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //HandleCameraMovement();

        if (Input.GetKey(KeyCode.A)) { _rotation = Vector3.down; }
        else if (Input.GetKey(KeyCode.D)) { _rotation = Vector3.up; }
        else { _rotation = Vector3.zero; }

        transform.Rotate(_rotation * _speed * Time.deltaTime);

        _velocity = _rb.velocity.magnitude;
        _vectorVelocity = _rb.velocity;
    }

    private void FixedUpdate()
    {
        float _moveSpeed = 0;
        if (Input.GetKey(KeyCode.W)) { _moveSpeed = maxMoveSpeed; }
        if (Input.GetKey(KeyCode.S)) { _moveSpeed = -maxMoveSpeed; }

        _rb.AddForce(_moveSpeed * transform.forward, ForceMode.Acceleration);

        _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, maxMoveSpeed);
    }

    //private void HandleCameraMovement()
    //{
    //    mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
    //    mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
    //    mouseY = Mathf.Clamp(mouseY, -35, 60); // Limit the vertical rotation angle.

    //    Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0);
    //    boatCam.transform.position = boatCam.transform.position + rotation * cameraOffset;
    //    boatCam.transform.LookAt(transform.position);
    //}

    public void PlayPassengerDialogue()
    {
        carryingPassenger = true;
        if (passengerDialogue)
        {
            FindObjectOfType<DialogueManager>().PlayDialogue(passengerDialogue);
        }
        else
        {
            Debug.LogWarning("No passenger dialogue to play!");
        }
    }
}
