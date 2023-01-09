using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
//Automatically implements the charactercomponent and playerInput if it doesnt exist in player component
[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerMovement3 : MonoBehaviour
{

    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    [Header("Damage")]
    public playerHealth playerHealthScript;

    public float groundDrag;
    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMult;
    bool readyToJump;
    public bool takeFallDamage = true;
    public float HighestPBeforeDamage = 5;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("GroundCheck")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Movement Sound Effects")]
    [SerializeField] private AudioClip walkSound;
    [SerializeField] private AudioClip sprintSound;
    [SerializeField] private AudioClip jumpStartSound;
    [SerializeField] private AudioClip jumpLandSound;
    private AudioSource _audioSource;

    [Header("PlayerCam")]
    public ThirdPersonCamera PlayerCamera;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;
    private float FallHeight;
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;
    private CharacterController controller;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction sprintStart;
    private InputAction sprintFinish;
    private InputAction crouchStart;
    private InputAction crouchFinish;
    private bool isSprinting = false;
    private bool isCrouching = false;


    [Header("Sliders")]
    public Slider JumpingSlider;
    public Slider SprintSpeedSlider;
    // Start is called before the first frame update


    private Animator anim;


    public MovementState state;
    public enum MovementState
    {
        walking,
        idle,
        sprinting,
        crouching,
        air
    }

    public void JumpingForceSet(float value)
    {
        jumpForce = value;

    }
    public void SprintSpeedSet(float value)
    {
        sprintSpeed = value;

    }
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();


        controller = GetComponent<CharacterController>();
        // disabling controller collider since it collides with player object
        controller.detectCollisions = false;
        playerInput = GetComponent<PlayerInput>();

        if (PlayerPrefs.HasKey("JumpForce"))
        {
            jumpForce = PlayerPrefs.GetFloat("JumpForce");
        }
        if (PlayerPrefs.HasKey("SprintSpeed"))
        {
            sprintSpeed = PlayerPrefs.GetFloat("SprintSpeed");
        }
        if (PlayerPrefs.HasKey("TakeFallDamage"))
        {
            if (PlayerPrefs.GetInt("TakeFallDamage") == 0)
            {
                takeFallDamage = false;
            }
            else
            {
                takeFallDamage = true;
            }
        }
        if (JumpingSlider != null)
        {
            JumpingSlider.value = jumpForce;
        }
        if (SprintSpeedSlider != null)
        {
            SprintSpeedSlider.value = walkSpeed;
        }


        // Unity New Input System
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        sprintStart = playerInput.actions["SprintStart"];
        sprintStart.performed += X => SprintPressed();
        sprintFinish = playerInput.actions["SprintFinish"];
        sprintFinish.performed += X => SprintPressed();
        crouchStart = playerInput.actions["CrouchStart"];
        crouchStart.performed += X => CrouchPressed();
        crouchFinish = playerInput.actions["CrouchFinish"];
        crouchFinish.performed += X => CrouchPressed();

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        ResetJump();
        startYScale = transform.localScale.y;
    }
    // Update is called once per frame
    private void Update()
    {
        GetReferences();
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.25f + 0.2f, whatIsGround);
        MyInput();
        SpeedControl();
        StateHandler();
        //handle drag
        if (grounded)
        {
            rb.drag = groundDrag;
            // calculates if the fall height - the ground position is higher than the highest allowed height before damage
            if (FallHeight - this.transform.position.y > HighestPBeforeDamage)
            {
                if (takeFallDamage == true)
                {
                    //passes the calculated damage value to the player health script
                    playerHealthScript.TakeFallDamage(Mathf.Round((FallHeight - this.transform.position.y) * 1) / 1 * 2);
                    //resets the fall height
                    FallHeight = this.transform.position.y;
                }

            }

        }

        else
        {
            rb.drag = 0;
            // checks the highest point of the player off the ground
            if (this.transform.position.y > FallHeight)
            {
                FallHeight = this.transform.position.y;
            }
        }

        PlayerPrefs.SetFloat("JumpForce", jumpForce);
        PlayerPrefs.SetFloat("SprintSpeed", sprintSpeed);



     



    }
    private void FixedUpdate()
    {
        MovePlayer();
        if (state == MovementState.walking)
        {
            anim.SetBool("Walking", true);
            anim.SetBool("Jump", false);
            anim.SetBool("Running", false);
            anim.SetBool("Crouching", false);
            anim.SetBool("Idle", false);
        }

        if (state == MovementState.sprinting)
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Jump", false);
            anim.SetBool("Running", true);
            anim.SetBool("Crouching", false);
            anim.SetBool("Idle", false);
        }
        if (state == MovementState.crouching)
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Jump", false);
            anim.SetBool("Running", false);
            anim.SetBool("Crouching", true);
            anim.SetBool("Idle", false);
        }

        if (state == MovementState.air)
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Jump", true);
            anim.SetBool("Running", false);
            anim.SetBool("Crouching", false);
            anim.SetBool("Idle", false);
        }

        if (state == MovementState.idle)
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Jump", false);
            anim.SetBool("Running", false);
            anim.SetBool("Crouching", false);
            anim.SetBool("Idle", true);
        }

    }
    private void MyInput()
    {
        //This prevents the player to move, jump or crouch when game is paused
        if (!PauseMenu.GameIsPaused)
        {
            // getting values from WASD Input from Unity new Input system
            Vector2 input = moveAction.ReadValue<Vector2>();
            horizontalInput = input.x;
            verticalInput = input.y;

            if (moveAction.ReadValue<Vector2>() == Vector2.zero)
            {
                state = MovementState.idle;
            }

            // only jumps when space bar is pressed, is ready to jump and player on the ground
            if (jumpAction.triggered && readyToJump && grounded)
            {
                _audioSource.PlayOneShot(jumpStartSound);
                readyToJump = false;
                Jump();
                Invoke(nameof(ResetJump), jumpCooldown);
            }

            //crouch
            if (isCrouching)
            {
                transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);

            }

            //stop crouch
            if (!isCrouching)
            {
                transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            }
        }


    }



    private void StateHandler()


    {
        //mode crouching
        if (isCrouching)
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }

        // Sprints only in Basic mode
        if (grounded && isSprinting && PlayerCamera.ViewSwitch)
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        else
        {
            state = MovementState.air;
        }
    }
    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //on Slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }
        // player movement on ground
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            _audioSource.PlayOneShot(walkSound);
        }

        // player movement in air
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMult, ForceMode.Force);
        }

        //off gravity while on slope
        rb.useGravity = !OnSlope();

    }

    private void SpeedControl()
    {
        //limit on slope
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
            {
                rb.velocity = rb.velocity.normalized * moveSpeed;
            }
        }
        else
        {
            // limits the maximym player movement speed to the setted value
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }

        }

    }
    //basic Jump function
    private void Jump()
    {
        exitingSlope = true;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
        exitingSlope = false;
    }

    //on slope movement method
    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.15f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;

        }
        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

    // basic switch method for sprint and crouch
    private void SprintPressed()
    {
        isSprinting = !isSprinting;
    }

    private void CrouchPressed()
    {
        isCrouching = !isCrouching;
    }


    private void GetReferences()
    {

        anim = GetComponentInChildren<Animator>();



    }

}

