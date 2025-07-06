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
    public float maxSpeed, minSpeed;

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
    }

    void FixedUpdate()
    {
        Vector3 up = new Vector3(0f, 0f, 1f);
        Vector3 down = new Vector3(0f, 0f, -1f);
        Vector3 left = new Vector3(1f, 0f, 0f);
        Vector3 right = new Vector3(-1f, 0f, 0f);

        Vector3 normalMoveDir = movementDirection.normalized;
        Vector3 force;
        if (normalMoveDir == up)
        {
            force = follower.transform.forward;
        } else if (normalMoveDir == down)
        {
            force = follower.transform.forward * -1f;
        } else if (normalMoveDir == right)
        {
            force = follower.transform.right * -1f;
        }  else if (normalMoveDir == left)
        {
            force = follower.transform.right;
        } else
        {
            force = new Vector3(movementDirection.x, movementDirection.y, movementDirection.z);
        }
        force.Normalize();
        if (force.y == 0f)
        {
            rBody.AddForce(force * 10f);
        } else if (force.y == 1f)
        {
            if (!done)
            {
                if (canJump)
                {
                    rBody.AddForce(0f, 5f, 0f, ForceMode.Impulse);
                    done = true;
                    canJump = false;
                    canStomp = true;
                }      
            }
        } else {
            if (canStomp)
            {
                if (!canJump)
                {
                    Debug.Log("smash");
                    force = new Vector3(0f, -5f, 0f);
                    rBody.AddForce(force*10f, ForceMode.Impulse);
                    canStomp = false;
                }
                // Debug.Log("smash");
                // rBody.AddForce(force * 15f);
                // canStomp = false;
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

    public void OnStomp(InputAction.CallbackContext context)
    {
        movementDirection = new Vector3(0f, -1f, 0f);
    }

    void OnCollisionStay(Collision collision)
        {
            Debug.Log("collision");
            done = true;
            canJump = true;
            canStomp = false;
        }

}
