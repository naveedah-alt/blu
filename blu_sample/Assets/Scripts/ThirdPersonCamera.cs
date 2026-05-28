using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;            // The player
    public Vector3 offset = new Vector3(0f, 3f, -6f); // Camera offset
    public float sensitivity = 2f;      // Mouse sensitivity
    public float smoothSpeed = 10f;     // Camera follow smoothness

    private float yaw = 0f;             // Rotation around Y axis
    private float xaw = 0f;
    private float zaw = 0f;

    void LateUpdate()
    {
        // Mouse input
        // Vector3 mouseInWorld = Camera.main.WorldToScreenPoint(Input.mousePosition);
        // yaw = mouseInWorld.x;

        yaw += Input.GetAxis("Mouse X") * sensitivity;
        xaw += Input.GetAxis("Mouse Y") * sensitivity;
        

        // Rotate offset based on yaw
        Quaternion rotation = Quaternion.Euler(xaw, yaw, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        // Move camera
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Look at the player
        transform.LookAt(target.position + Vector3.up * 1.5f); // slight offset for better framing
    }

    public Vector3 GetCameraForward() => Quaternion.Euler(0, yaw, 0) * Vector3.forward;
    public Vector3 GetCameraRight() => Quaternion.Euler(0, yaw, 0) * Vector3.right;
}
   