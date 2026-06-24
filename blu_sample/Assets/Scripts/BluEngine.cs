using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// This script controls the movement of Player 1 - Daniel

public class BluEngine : MonoBehaviour
{
    //grabs camera script variables
    public ThirdPersonCamera camController;

    //Animator that allows for transitions between states
    private Animator anim;

    // Ground Movement
    private Rigidbody rb;
    public float MoveSpeed = 5f;
    private float moveHorizontal;
    private float moveVertical;
    // Jumping
    public float jumpForce = 10f;
    public float fallMultiplier = 2.5f; // Multiplies gravity when falling down
    public float ascendMultiplier = 2f; // Multiplies gravity for ascending to peak of jump
    private bool isGrounded = true;
    public LayerMask groundLayer;
    private float groundCheckTimer = 0f;
    private float groundCheckDelay = 0.3f;
    private float playerHeight;
    private float raycastDistance;
    private float cameraRotationy;
    private Quaternion rotation;
    public float rotationSpeed;
    private bool ledgeGrabbed;
    private Ledge ledge;

    public bool movementEnabled;
    public bool sprint;
    private float acceleration;
    public bool grabbed;

    // Start is called before the first frame update
    void Start()
    {
        acceleration = 2.0f;
        sprint = false;
        movementEnabled = true;
        anim = GetComponent<Animator>();
        grabbed = anim.GetBool("LedgeIsGrabbed");
        rb = GetComponent<Rigidbody>();
        //rb.freezeRotation = true;

        // Set the raycast to be slightly beneath the player's feet
        playerHeight = GetComponent<CapsuleCollider>().height * transform.localScale.y;
        raycastDistance = (playerHeight / 2) + 0.2f;

        // Hides the mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cameraRotationy = camController.gameObject.transform.localEulerAngles.y;

        rotation = Quaternion.Euler(0.0f, cameraRotationy, 0.0f);

    }

    IEnumerator StandUpStart()
    {
        ledgeGrabbed = false;
        anim.SetBool("LedgeIsGrabbed", false);
        anim.SetBool("PlayerIsClimbing", true);
        yield return new WaitForSeconds (1.1f);
        StandupFinish();
    }

    public void StandupFinish ()
    {
        transform.position = ledge.StandUpProgress();
        anim.SetBool("PlayerIsClimbing", false);
        anim.enabled = true;
        movementEnabled = true;
        rb.freezeRotation = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        rb.isKinematic = false;
    }

    // Update is called once per frame
    void Update()
    {
        cameraRotationy = camController.gameObject.transform.localEulerAngles.y;

        rotation = Quaternion.Euler(0.0f, cameraRotationy, 0.0f);
        rotation.Normalize();

        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        

        if (Input.GetButtonDown("Jump") && isGrounded && movementEnabled)
        {
            Jump();
        }

        // Checking when we're on the ground and keeping track of our ground check delay
        if (!isGrounded && groundCheckTimer <= 0f)
        {
            Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
            isGrounded = Physics.Raycast(rayOrigin, Vector3.down, raycastDistance, groundLayer);
        }
        else
        {
            groundCheckTimer -= Time.deltaTime;
        }

        if(ledgeGrabbed == true)
        {
            rb.freezeRotation = true;
            if (Input.GetKeyDown (KeyCode. Space))
            {
                anim.SetBool("PlayerIsClimbing", true);
                StartCoroutine(StandUpStart());
            }
            
        }
            
    }

    IEnumerator WaitToMove(Vector3 handPos)
    {
        anim.SetBool ("LedgeIsGrabbed", true);
        yield return new WaitForSeconds (1.1f);
        FinishMoving(handPos);
        //transform.position = handPos;
    }

    public void FinishMoving(Vector3 handPos)
    {
        transform.position = handPos;
    }
    

    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            //gameObject.transform.rotation = rotation;

        }

        // if (Input.GetKeyDown (KeyCode.LeftShift))
        // {
             //Debug.Log("sprinting!");
        //     sprint = true;
        // }
        // if (Input.GetKeyUp (KeyCode.LeftShift))
        // {
        //     Debug.Log("done sprinting!");
        //     sprint = false;
        // }
        
        if (movementEnabled)
        {
            MovePlayer();
            ApplyJumpPhysics();
        }
        if (isGrounded)
        {
            anim.SetBool("Jump", false);
        }

    }

    public void LedgeGrabbed(Vector3 handPos, Ledge currentLedge)
    {
        anim.SetBool("LedgeIsGrabbed", true);
        ledge = currentLedge;
        movementEnabled = false;
        
        ledgeGrabbed = true; 
        //Debug.Log("ledge collided");
        anim.SetBool("Walking", false);
        anim.SetBool ("Jump", false);
        // StartCoroutine(WaitToMove(handPos));
        transform.position = handPos;
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward);
        transform.rotation = toRotation;
        rb.isKinematic = true;
        
        //transform.position += Vector3.forward;
        //WaitToMove(handPos);
        
    }
    void MovePlayer()
    {
        
        // Get camera-relative directions
        Vector3 camForward = camController.GetCameraForward();
        Vector3 camRight = camController.GetCameraRight();
   
        

        //gameObject.transform.rotation = rotation;

        // Flatten and normalize
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 movement = (camRight * moveHorizontal + camForward * moveVertical).normalized;
        Vector3 targetVelocity = movement * MoveSpeed;

        // Apply movement to the Rigidbody
        Vector3 velocity = rb.velocity;
        velocity.x = targetVelocity.x;
        velocity.z = targetVelocity.z;
        rb.velocity = velocity;
        // if (movement!= Vector3.zero)
        // {
        //     //transform.forward = movement;
        // }
         if (movement!= Vector3.zero)
        {
            transform.forward = movement;
            
            // if (sprint)
            // {
            //     velocity = rb.velocity;
            //     velocity.x += acceleration * Time.deltaTime;
            //     velocity.z += acceleration * Time.deltaTime;
            //     rb.velocity = velocity;
                
            //     //anim.SetBool("Walking", false);
            //     //anim.SetBool("Running", movement.magnitude > 0);
            // } 
            // else
            // {
            //     //transform.forward = movement;
            //     //anim.SetBool("Running", false);
            //     anim.SetBool("Walking", movement.magnitude > 0);
            // }
            
            // Quaternion toRotation = Quaternion. LookRotation(movement, Vector3.up);
            // transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        // if (sprint)
        // {
        //     anim.SetBool("Running", movement.magnitude > 0);
        // } else
        // {
        //     anim.SetBool("Walking", movement.magnitude > 0);
        // }

        anim.SetBool("Walking", movement.magnitude > 0);
        
        //Debug.Log(anim.GetBool("Walking"));
        // rb.rotation = camController;

        

        // If we aren't moving and are on the ground, stop velocity so we don't slide
        if (isGrounded && moveHorizontal == 0 && moveVertical == 0)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            //rb.rotation = 
        }
    }

    IEnumerator JumpStart()
    {
        
        isGrounded = false;
        groundCheckTimer = groundCheckDelay;
        yield return new WaitForSeconds (0.2f);
        JumpFinish();
    }

    public void JumpFinish()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
    }

    void Jump()
    {
        anim.SetBool("Jump", true);
        StartCoroutine(JumpStart());
        // anim.SetBool("Jump", true);
        // isGrounded = false;
        // groundCheckTimer = groundCheckDelay;
        // rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z); // Initial burst for the jump
    }

    void ApplyJumpPhysics()
    {
        if (rb.velocity.y < 0)
        {
            // Falling: Apply fall multiplier to make descent faster
            rb.velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.fixedDeltaTime;
        } // Rising
        else if (rb.velocity.y > 0)
        {
            // Rising: Change multiplier to make player reach peak of jump faster
            rb.velocity += Vector3.up * Physics.gravity.y * ascendMultiplier * Time.fixedDeltaTime;
        }
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        sprint = true;
        Debug.Log("sprint is true");
    }


}
