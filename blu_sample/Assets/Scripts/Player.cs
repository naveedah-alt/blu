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

public class Player : MonoBehaviour
{
    public Rigidbody rBody;
    //Follower gameobject, allows camera to follow Player without being attached to it
    public GameObject follower;

    // Fields for Speed
    public float speed;

    // Fields for Input (WASD, Joystick, etc)
    public Vector3 movementDirection;

    //Bool done, checks for movement to be over, particularly useful with jumping, so there is no infinite jump
    bool done;

    //Works in conjunction with done to prevent double jumps
    bool canJump;

    //Animator that allows for transitions between states
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    /// <summary>
    /// Changes follower postion every frame update
    /// </summary>
    void Update()
    {
        follower.transform.position = transform.position;
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Translates gameObject based on InputAction parameter functions below
    /// </summary>
    void FixedUpdate()
    {
        if (movementDirection.x != 0f || movementDirection.z != 0f)
        {
            transform.Translate(movementDirection * speed * Time.deltaTime);
            Debug.Log("is running");
            anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false);
        }
        
        if (movementDirection.y != 0f)
        {
            if (canJump)
            {
                transform.Translate(movementDirection * speed * Time.deltaTime);
                canJump = false;
            }
        }
    }

    public void jump()
    {
        Vector3 jumpVector = new Vector3(0f, 3f, 0f);
        rBody.AddForce(jumpVector, ForceMode.Impulse);
        // transform.Translate(jumpVector * speed * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>();

        //movementDirection.z = movementDirection.y;

        movementDirection.y = 0f;

    }


    public void OnJump(InputAction.CallbackContext context)
    {
        jump();
    }

    void OnCollisionStay(Collision collision)
        {
            Debug.Log("collision");
            done = true;
            canJump = true;
        }

}
