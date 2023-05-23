using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 1f;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float moveSpeed = 5f;

    private CharacterController characterController;
    private float cameraVerticalAngle;
    private float characterVelocityY;
    private Vector3 characterVelocityMomentum;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        HandleCharacterLook();
        HandleCharacterMovement();
    }

    private void HandleCharacterLook()
    {
        float lookX = Input.GetAxisRaw("Mouse X");
        float lookY = Input.GetAxisRaw("Mouse Y");

        // Rotate the transform with the input speed around its local Y axis.
        transform.Rotate(new Vector3(0f, lookX * mouseSensitivity, 0f), Space.Self);

        // Add vertical inputs to the camera's vertical angle.
        cameraVerticalAngle -= lookY * mouseSensitivity;

        // Limit the camera's vertical angle to min/max.
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -89f, 89f);

        // Apply the vertical angle as a local rotation to the camera transform along its right axis (makes it pivot up and down).
        playerCamera.transform.localEulerAngles = new Vector3(cameraVerticalAngle, 0, 0);
    }

    private void HandleCharacterMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 characterVelocity = transform.right * moveX * moveSpeed + transform.forward * moveZ * moveSpeed;

        // Apply gravity to the velocity.
        float gravityDownForce = -60f;
        characterVelocityY += gravityDownForce * Time.deltaTime;

        // Apply Y velocity to move vector.
        characterVelocity.y = characterVelocityY;

        // Apply momentum
        characterVelocity += characterVelocityMomentum;

        // Move Character Controller.
        characterController.Move(characterVelocity * Time.deltaTime);

        // Dampen momentum
        if (characterVelocityMomentum.magnitude >= 0f)
        {
            float momentumDrag = 3f;
            characterVelocityMomentum -= characterVelocityMomentum * momentumDrag * Time.deltaTime;
            if (characterVelocityMomentum.magnitude < .0f)
            {
                characterVelocityMomentum = Vector3.zero;
            }
        }
    }
}
