using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public float HighestPBeforeDamage = 5;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("GroundCheck")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;
    
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
    private InputAction sprintAction;
    private InputAction crouchAction;
    private bool isSprinting = false;
    private bool isCrouching = false;
    
    // Start is called before the first frame update


    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        air
    }
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        // disabling controller collider since it collides with player object
        controller.detectCollisions = false;
        playerInput = GetComponent<PlayerInput>();

        // Unity New Input System
        moveAction=playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        playerInput.actions["SprintStart"].performed += X => SprintPressed();
        playerInput.actions["SprintFinish"].performed += X => SprintPressed();
        playerInput.actions["CrouchStart"].performed += X => CrouchPressed();
        playerInput.actions["CrouchFinish"].performed += X => CrouchPressed();
        
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        ResetJump();
        startYScale = transform.localScale.y;
    }
    // Update is called once per frame
    private void Update()
    {
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down,playerHeight*0.25f+0.2f, whatIsGround);
        MyInput();
        SpeedControl();
        StateHandler();
        //handle drag
        if (grounded)
        {
            rb.drag = groundDrag;
            // calculates if the fall height - the ground position is higher than the highest allowed height before damage
            if (FallHeight- this.transform.position.y > HighestPBeforeDamage)
            {
                //passes the calculated damage value to the player health script
                playerHealthScript.TakeFallDamage(Mathf.Round((FallHeight - this.transform.position.y) * 1) / 1 * 2);
                //resets the fall height
                FallHeight = this.transform.position.y;
            }

        }

        else
        {
            rb.drag = 0;
            // checks the highest point of the player off the ground
            if (this.transform.position.y>FallHeight)
            {
                FallHeight = this.transform.position.y;
            }
        }
            

    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MyInput()
    {
        // getting values from WASD Input from Unity new Input system
        Vector2 input = moveAction.ReadValue<Vector2>();
        horizontalInput = input.x;
        verticalInput = input.y;
        
        // only jumps when space bar is pressed, is ready to jump and player on the ground
        if (jumpAction.triggered && readyToJump && grounded)
        {
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

    

    private void StateHandler()

        
    {
        //mode crouching
        if (isCrouching)
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }

        // Sprints only in Basic mode
        if (grounded && isSprinting && PlayerCamera.currentView.Equals(ThirdPersonCamera.CameraView.Basic))
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
            rb.AddForce(GetSlopeMoveDirection()*moveSpeed*20f,ForceMode.Force);

            if (rb.velocity.y > 0)
            {
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }
        // player movement on ground
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
            
        // player movement in air
        else if(!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMult, ForceMode.Force);
        }

        //off gravity while on slope
        rb.useGravity = !OnSlope();
        
    }

    private void SpeedControl()
    {
        //limit on slope
        if (OnSlope()&& !exitingSlope)
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
   
}
