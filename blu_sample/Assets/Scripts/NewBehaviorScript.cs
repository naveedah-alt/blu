using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public ThirdPersonCamera camController;

    public float rollDuration = 1f;
    private float rollTimer = 0f;
    public float rollSpeed = 360f;
    public float rollMoveSpeed = 0f;

    public GameObject blueberryDustPrefab;

    //Animator that allows for transitions between states
    private Animator anim;

    private Rigidbody rb;
    private bool isGrounded = true;
    private bool isRolling = false;
    private bool isSpinning = false;
    private float totalSpin = 0f;
    public float jumpSpinSpeed = 720f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        /* if (!isRolling)
         {*/
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Grabbing the Animator Component
        anim = GetComponent<Animator>();

        // Get camera-relative directions
        Vector3 camForward = camController.GetCameraForward();
        Vector3 camRight = camController.GetCameraRight();

        // Flatten and normalize
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 inputDir = (camForward * vertical + camRight * horizontal).normalized;

        /*bool isRollKeyHeld = Input.GetKey(KeyCode.R);

        if (isRollKeyHeld && inputDir.magnitude != 0f && isGrounded)
        {
            Vector3 move = inputDir * rollMoveSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + move);
            transform.forward = inputDir;

            float cupcakeRadius = 5f;
            float rollDegrees = (move.magnitude / cupcakeRadius) * Mathf.Rad2Deg;
            transform.Rotate(Vector3.right, rollDegrees, Space.Self);

            isRolling = true;
        }
        else
        {
            isRolling = false;*/

        if (inputDir.magnitude != 0f)
        {
            transform.Translate(inputDir * moveSpeed * Time.deltaTime, Space.World);
            transform.forward = inputDir;
            anim.SetBool("Running", true);
            /* float bounce = Mathf.Sin(Time.time * 10f) * 0.05f;
             float sideWobble = Mathf.Sin(Time.time * 20f) * 0.03f;
             transform.localScale = new Vector3(1f + sideWobble, 1f + bounce, 1f - sideWobble);*/
        }
        else
        {
            transform.localScale = Vector3.one;
            anim.SetBool("Running", false);
        }
        //}

        // Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            Vector3 dashDirection = inputDir;
            if (dashDirection.magnitude != 0f)
            {
                dashDirection = transform.forward;
            }
            rb.AddForce(dashDirection * 15f, ForceMode.VelocityChange);
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            isRolling = false;
            isSpinning = true;
            totalSpin = 0f;

            if (blueberryDustPrefab != null)
            {
                Instantiate(blueberryDustPrefab, transform.position + Vector3.up * 1f, Quaternion.identity);
            }
            isGrounded = false;
        }

        // Optional: Manual roll start (though holding R does this already)
        /*if (Input.GetKeyDown(KeyCode.R) && isGrounded)
        {
            StartRoll();
        }*/
        //}
        /*else
        {
            // Rolling logic
            rollTimer += Time.deltaTime;

            Vector3 fixedPosition = rb.position;
            fixedPosition.y = 0.5f;
            rb.position = fixedPosition;

            Vector3 velocity = rb.velocity;
            velocity.y = 0;
            rb.velocity = velocity;

            transform.Rotate(Vector3.right * rollSpeed * Time.deltaTime);
            Vector3 forwardMove = transform.forward * rollMoveSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + forwardMove);

            if (rollTimer >= rollDuration)
            {
                StopRoll();
            }
        }*/

        // Jump spin
        /*if (isSpinning)
        {
            float spinStep = jumpSpinSpeed * Time.deltaTime;
            transform.Rotate(Vector3.right, spinStep, Space.Self);
            totalSpin += spinStep;

            if (totalSpin >= 360f)
            {
                isSpinning = false;
            }
        }*/
    }

    void StartRoll()
    {
        isRolling = true;
        rollTimer = 0f;
        rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    void StopRoll()
    {
        isRolling = false;
        rollTimer = 0f;
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isSpinning = false;

            if (!isRolling)
            {
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
                rb.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }
    }
}
