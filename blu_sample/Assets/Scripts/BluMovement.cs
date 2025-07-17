using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;

public class BluMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    // Fields for Input (WASD, Joystick, etc)
    public Vector3 movementDirection;

    //grabs camera script variables
    public ThirdPersonCamera camController;

    //Animator that allows for transitions between states
    private Animator anim;
    //Blu's Rigid Body
    public Rigidbody rb;

    //Bool done, checks for movement to be over, particularly useful with jumping, so there is no infinite jump
    bool done;

    //Works in conjunction with done to prevent double jumps
    bool canJump;

    //Identifies CoolDown Time
    public float dashCD = 3f;

    // Start is called before the first frame update
    void Start()
    {
        //Initializing Components for RigidBody and Animator
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Translates gameObject based on InputAction parameter functions below
    /// </summary>
    void Update()
    {
        // Get camera-relative directions
        Vector3 camForward = camController.GetCameraForward();
        Vector3 camRight = camController.GetCameraRight();

        //Movement Variables Based on Axises 
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Flatten and normalize
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 inputDir = (camForward * vertical + camRight * horizontal).normalized;
        if (inputDir.magnitude != 0f)
        {
            transform.Translate(inputDir * moveSpeed * Time.deltaTime, Space.World);
            transform.forward = inputDir;
            anim.SetBool("Running", true);
            Debug.Log("is running");
            anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false);
        }

        //decreases cooldown time
        dashCD -= Time.deltaTime;
        // Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCD <= 0)
        {
            Vector3 dashDirection = inputDir;
            if (dashDirection.magnitude != 0f)
            {
                dashDirection = transform.forward;
            }
            rb.AddForce(dashDirection * 18f, ForceMode.VelocityChange);
            dashCD = 5f;
        }

        if (movementDirection.y != 0f)
        {
            if (canJump)
            {
                //transform.Translate(inputDir * moveSpeed * Time.deltaTime);
                canJump = false;
            }
        }

    }
    public void jump()
    {
        if (canJump)
        {
            Vector3 jumpVector = new Vector3(0f, 5f, 0f);
            rb.AddForce(jumpVector, ForceMode.Impulse);
            transform.Translate(jumpVector * moveSpeed * Time.deltaTime);
            canJump = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jump();
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>();

        //movementDirection.z = movementDirection.y;

        movementDirection.y = 0f;

    }
    void OnCollisionStay(Collision collision)
    {
        Debug.Log("collision");
        done = true;
        canJump = true;
    }

}
