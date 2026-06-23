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
        if ((xaw + Input.GetAxis("Mouse Y") * sensitivity) <= 64.0f && (xaw + Input.GetAxis("Mouse Y") * sensitivity) >= -103.0f)
        {
            xaw += Input.GetAxis("Mouse Y") * sensitivity;
        } else
        {
            
        }
        //Debug.Log(yaw);
         Debug.Log(xaw);
        //zaw += Input.GetAxis("Mouse Z") * sensitivity;
        

        // Rotate offset based on yaw
        // Quaternion rotX = Quaternion.AngleAxis(xaw, Vector3.right);
        // Quaternion rotY = Quaternion.AngleAxis(yaw, Vector3.up);
        Quaternion rotation = Quaternion.Euler(xaw, yaw, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        // Move camera
        //transform.position = Vector3.
        //Vector3.MoveTowards(transform.position, desiredPosition, smoothSpeed);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Look at the player
        transform.LookAt(target.position + Vector3.up * 1.5f); // slight offset for better framing

        // Vector3 currentRotation = transform.localEulerAngles;
        // currentRotation.y += yaw;
        // transform.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up);
        // Vector3 currentCameraRotation = transform.localEulerAngles;
        // currentCameraRotation.x -= xaw;//if +, mouse look is reversed
        // //_mainCamera.gameobject.transform.localeulerangles = currentCamerarotation;
        // //Above works fine but we better use Quaternion.
        // transform.localRotation = Quaternion.AngleAxis(currentCameraRotation.x, Vector3.right);
        //66 and -105
    }

    public Vector3 GetCameraForward() => Quaternion.Euler(0, yaw, 0) * Vector3.forward;
    public Vector3 GetCameraRight() => Quaternion.Euler(0, yaw, 0) * Vector3.right;
}
   