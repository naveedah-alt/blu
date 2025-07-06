using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour
{
    public Camera camera;

    public Rigidbody rBody;

    public GameObject follower;

    // Fields for Speed
    public float speed;

    public float accelerationRate, decelerationRate;

    // Fields for Turning
    public float turnSpeed;

    // Fields for Input
    public Vector3 movementDirection;

    // Fields for Movement Vectors
    Vector3 velocity, acceleration;

    bool done;

    bool canJump;

    bool canStomp;

    // Fields for Quaternions
    Quaternion turning;

    Vector3 bufferSpace;


    public int terrainLayer = 3;
    int terrainLayerMask;
    RaycastHit terrainHit;
    Vector3 rayOrigin;

    Vector3 m_EulerAngleVelocity;


    float forceConrol;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {

        bufferSpace = new Vector3(0f, 1f, -1f);

    }

    // Update is called once per frame
    void Update()
    {
        follower.transform.position = transform.position;
        // camera.transform.position = transform.position + bufferSpace;
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        transform.Translate(movementDirection * speed * Time.deltaTime);
        if (movementDirection.x != 0f || movementDirection.z != 0f)
        {
            Debug.Log("is running");
            anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false);
        }
        
        if (movementDirection.y == 1f)
        {
            if (!done)
            {
                if (canJump)
                {
                    // rBody.AddForce(0f, 5f, 0f, ForceMode.Impulse);
                    transform.Translate(movementDirection * speed * Time.deltaTime);
                    done = true;
                    canJump = false;
                    canStomp = true;
                }
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>();

        movementDirection.z = movementDirection.y;

        movementDirection.y = 0f;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawRay(rayOrigin, Vector3.down * 120f);
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        movementDirection = new Vector3(0f, 1f, 0f);
        done = false;
    }

    void OnCollisionStay(Collision collision)
        {
            Debug.Log("collision");
            done = true;
            canJump = true;
            canStomp = false;
        }

}
