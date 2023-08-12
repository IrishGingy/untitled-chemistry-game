using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 cameraOffset = new Vector3(0f, 2f, -5f);
    public float rotationSpeed = 5f;
    public float minCamY = -35f;
    public float maxCamY = 55f;

    private float mouseX, mouseY;

    private void Update()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, minCamY, maxCamY); // Limit the vertical rotation angle.

        Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0);
        transform.position = player.position + rotation * cameraOffset;
        transform.LookAt(player.position);
    }
}