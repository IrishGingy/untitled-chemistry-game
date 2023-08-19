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
    //[SerializeField] public Transform boatParts;
    //[SerializeField] public GameObject boatCam;
    //[SerializeField] public Vector3 cameraOffset = new Vector3(0f, 2f, -5f);
    //[SerializeField] public float rotationSpeed = 5f;

    [SerializeField] private Vector3 _rotation;
    [SerializeField] private float _speed;
    [SerializeField] private float _velocity;
    [SerializeField] private Vector3 _vectorVelocity;
    [SerializeField] private Vector3 offset;

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

        //_velocity = _rb.velocity.magnitude;
        //_vectorVelocity = _rb.velocity;
    }

    private void FixedUpdate()
    {
        //float _moveSpeed = 0;
        //if (Input.GetKey(KeyCode.W)) { _moveSpeed = maxMoveSpeed; }
        //if (Input.GetKey(KeyCode.S)) { _moveSpeed = -maxMoveSpeed; }

        //_rb.AddForce(_moveSpeed * transform.forward, ForceMode.Acceleration);

        //_rb.velocity = Vector3.ClampMagnitude(_rb.velocity, maxMoveSpeed);

        // compute vectors
        var forward = Vector3.Scale(new Vector3(1, 0, 1), transform.forward);
        var targetVel = Vector3.zero;

        // forward/backward power
        if (Input.GetKey(KeyCode.W))
        {
            ApplyForceToReachVelocity(_rb, forward * maxMoveSpeed * Time.deltaTime, _speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            ApplyForceToReachVelocity(_rb, forward * -maxMoveSpeed * Time.deltaTime, _speed);
        }
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
            //Debug.LogWarning("No passenger dialogue to play!");
        }
    }

    private void ApplyForceToReachVelocity(Rigidbody rigidbody, Vector3 velocity, float force = 1, ForceMode mode = ForceMode.Force)
    {
        if (force == 0 || velocity.magnitude == 0)
            return;

        velocity = velocity + velocity.normalized * 0.2f * rigidbody.drag;

        //force = 1 => need 1 s to reach velocity (if mass is 1) => force can be max 1 / Time.fixedDeltaTime
        force = Mathf.Clamp(force, -rigidbody.mass / Time.fixedDeltaTime, rigidbody.mass / Time.fixedDeltaTime);

        //dot product is a projection from rhs to lhs with a length of result / lhs.magnitude https://www.youtube.com/watch?v=h0NJK4mEIJU
        if (rigidbody.velocity.magnitude == 0)
        {
            rigidbody.AddForce(velocity * force, mode);
        }
        else
        {
            var velocityProjectedToTarget = (velocity.normalized * Vector3.Dot(velocity, rigidbody.velocity) / velocity.magnitude);
            rigidbody.AddForce((velocity - velocityProjectedToTarget) * force, mode);
        }
    }
}
