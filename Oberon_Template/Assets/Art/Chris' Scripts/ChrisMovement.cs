using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChrisMovement : MonoBehaviour
{

    CharacterController controller;

    public Transform groundCheck;

    public LayerMask groundMask;
    public LayerMask wallMask;

    //moves controller
    Vector3 move;

    //stores input
    Vector3 input;

    Vector3 Yvelocity;

    Vector3 forwardDirection;


    int jumpCharges;

    [HideInInspector] public bool isGrounded;

    [HideInInspector] public bool isSprinting;

    [HideInInspector] public bool isCrouching;

    [HideInInspector] public bool isSliding;

    [HideInInspector] public bool isWallRunning;

    [HideInInspector] public bool isGrappling;


    public float slideSpeedIncrease;

    public float slideSpeedDecrease;

    public float wallRunSpeedIncrease;

    public float wallRunSpeedDecrease;



    float speed;

    public float runSpeed;

    public float airSpeed;

    public float sprintSpeed;

    public float crouchSpeed;

    public float climbSpeed;


    float gravity;

    public float normalGravity;

    public float wallRunGravity;

    public float jumpHeight;


    float startHeight;
    float crouchHeight = 0.5f;
    Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
    Vector3 standingCenter = new Vector3(0, 0, 0);

    float slideTimer;
    public float maxSlideTimer;

    //wall stuff
    bool hasWallRun = false;
    bool onLeftWall;
    bool onRightWall;

    RaycastHit leftWallHit;

    RaycastHit rightWallHit;

    Vector3 wallNormal;

    Vector3 lastWallNormal;

    bool isClimbing;
    bool canClimb;
    bool hasClimbed;
    RaycastHit wallHit;
    float climbTimer;
    public float maxClimbTimer;

    bool isWallJumping;
    float wallJumpTimer;
    public float maxWallJumpTimer; 

    public Camera playerCamera;
    public float cameraChangeTime;
    public float wallRunTilt;
    public float tilt;

    /*
     * Grappling and Swinging Code Possibly
     */
    [Header("Input")]
    public KeyCode grappleKey = KeyCode.Mouse1;
    public bool activeGrapple;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        startHeight = transform.localScale.y;
    }

    void IncreaseSpeed(float speedIncrease)
    {
        speed += speedIncrease;
    }

    void DecreaseSpeed(float speedDecrease)
    {
        speed -= speedDecrease * Time.deltaTime;
    }

    void HandleInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        input = transform.TransformDirection(input);
        input = Vector3.ClampMagnitude(input, 1f);

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && jumpCharges > 0)
        {
            Jump();
        }
        //Crouch
        if (Input.GetKeyDown(KeyCode.C))
        {
            Crouch();
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            ExitCrouch();
        }
        //Sprint
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            isSprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }
        //if (Input.GetKeyCode(grappleKey))
        //{
        //    isGrappling= true;
        //}
    }

    void CameraEffects()
    {
        if (isWallRunning)
        {
            if (onRightWall)
            {
                tilt = Mathf.Lerp(tilt, wallRunTilt, cameraChangeTime * Time.deltaTime);
            }
            if (onLeftWall)
            {
                tilt = Mathf.Lerp(tilt, -wallRunTilt, cameraChangeTime * Time.deltaTime);
            }
        }
        if (!isWallRunning)
        {
            tilt = Mathf.Lerp(tilt, 0f, cameraChangeTime * Time.deltaTime);

        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        CheckWallRun();
        CheckClimbing();
        if (isGrounded && !isSliding && !activeGrapple)
        {
            GroundedMovement();
        }
        else if (!isGrounded && !isWallRunning && !isClimbing)
        {
            AirMovement();
        }
        else if (isSliding)
        {
            SlideMovement();
            DecreaseSpeed(slideSpeedDecrease);
            slideTimer -= 1f * Time.deltaTime;
            if (slideTimer < 0)
            {
                isSliding = false;
            }
        }
        else if (isWallRunning)
        {
            WallRunMovement();
            DecreaseSpeed(wallRunSpeedDecrease);
        }
        else if (isClimbing)
        {
            ClimbMovement();
            climbTimer -= 1f * Time.deltaTime;
            if (climbTimer < 0)
            {
                isClimbing = false;
                hasClimbed = true;
            }
        } else if (isGrappling)
        {
            if (activeGrapple) return;
        }

        GroundedMovement();
        checkGround();
        controller.Move(move * Time.deltaTime);
        ApplyGravity();
        CameraEffects();
    }

    void GroundedMovement()
    {
        speed = isSprinting ? sprintSpeed : isCrouching ? crouchSpeed : runSpeed;
        if (input.x != 0)
        {
            move.x += input.x * speed;
        }
        else
        {
            move.x = 0;
        }
        if (input.z != 0)
        {
            move.z += input.z * speed;
        }
        else
        {
            move.z = 0;
        }

        move = Vector3.ClampMagnitude(move, speed);

    }

    void AirMovement()
    {
        move.x += input.x * airSpeed;
        move.z += input.z * airSpeed;
        if (isWallJumping)
        {
            move += forwardDirection;
            wallJumpTimer -= 1f * Time.deltaTime;
            if (wallJumpTimer < 0)
            {
                isWallJumping = false;
            }
        }

        move = Vector3.ClampMagnitude(move, speed);
    }

    void SlideMovement()
    {
        move += forwardDirection;
        move = Vector3.ClampMagnitude(move, speed);
    }

    void WallRunMovement()
    {
        if (input.z > (forwardDirection.z - 10f) && input.z < (forwardDirection.z + 10f))
        {
            move += forwardDirection;
        }
        else if (input.z < (forwardDirection.z - 10f) && input.z > (forwardDirection.z + 10f))
        {
            move.x = 0f;
            move.y = 0f;
            ExitWallRun();
        }
        move.x += input.x * airSpeed;

        move = Vector3.ClampMagnitude(move, speed);
    }

    void ClimbMovement()
    {
        forwardDirection = Vector3.up;
        move.x += input.x * airSpeed;
        move.z += input.z * airSpeed;

        Yvelocity += forwardDirection;
        speed = climbSpeed;

        move = Vector3.ClampMagnitude(move, speed);
        Yvelocity = Vector3.ClampMagnitude(Yvelocity, speed);
    }


    void checkGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundMask);
        if (isGrounded)
        {
            jumpCharges = 1;
            hasWallRun = false;

            hasClimbed = false;
            climbTimer = maxClimbTimer;
        }
    }

    void CheckWallRun()
    {
        onLeftWall = Physics.Raycast(transform.position, -transform.right, out leftWallHit, 0.7f, wallMask);
        onRightWall = Physics.Raycast(transform.position, transform.right, out rightWallHit, 0.7f, wallMask);

        if((onRightWall || onLeftWall) && !isWallRunning)
        {
            TestWallRun();
        }
        if((!onRightWall && !onLeftWall) && isWallRunning)
        {
            ExitWallRun();
        }
    }

    void TestWallRun()
    {
        wallNormal = onLeftWall ? leftWallHit.normal : rightWallHit.normal;
        if (hasWallRun)
        {
            float wallAngle = Vector3.Angle(wallNormal, lastWallNormal);
            if (wallAngle > 15)
            {
                WallRun();
            }
        }
        else
        {
            WallRun();
            hasWallRun = true;
        }
    }


    void ApplyGravity()
    {
        gravity = isWallRunning ? wallRunGravity : isClimbing ? 0f : normalGravity;
        Yvelocity.y += gravity * Time.deltaTime;
        controller.Move(Yvelocity * Time.deltaTime);
    }

    void Jump()
    {
        if (!isGrounded && !isWallRunning)
        {
            jumpCharges -= 1;
        }
        else if (isWallRunning)
        {
            ExitWallRun();
            IncreaseSpeed(wallRunSpeedIncrease);
        }

        hasClimbed = false;
        climbTimer = maxClimbTimer;
        Yvelocity.y = Mathf.Sqrt(jumpHeight * -2f * normalGravity);
    }

    void Crouch()
    {
        controller.height = crouchHeight;
        controller.center = crouchingCenter;
        transform.localScale = new Vector3(transform.localScale.x, crouchHeight, transform.localScale.z);
        isCrouching = true;

        if (speed > runSpeed)
        {
            isSliding = true;
            forwardDirection = transform.forward;
            if (isGrounded)
            {
                IncreaseSpeed(slideSpeedIncrease);
            }

            slideTimer = maxSlideTimer;
        }
    }

    void ExitCrouch()
    {
        controller.height = (startHeight * 2);
        controller.center = standingCenter;
        transform.localScale = new Vector3(transform.localScale.x, startHeight, transform.localScale.z);
        isCrouching = false;
        isSliding = false;
    }

    void WallRun()
    {
        isWallRunning = true;
        jumpCharges = 2;
        IncreaseSpeed(wallRunSpeedIncrease);
        Yvelocity = new Vector3(0f, 0f, 0f);

        forwardDirection = Vector3.Cross(wallNormal, Vector3.up);

        if (Vector3.Dot(forwardDirection, transform.forward) < 0)
        {
            forwardDirection = -forwardDirection;
        }
    }

    void ExitWallRun()
    {
        isWallRunning = false;
        lastWallNormal = wallNormal;
        forwardDirection = wallNormal;
        isWallJumping = true;
        wallJumpTimer = maxWallJumpTimer;
        
    }
    
    void CheckClimbing()
    {
        canClimb = Physics.Raycast(transform.position, transform.forward, out wallHit, 0.7f, wallMask);
        float wallAngle = Vector3.Angle(-wallHit.normal, transform.forward);
        if (wallAngle < 15 && !hasClimbed && canClimb)
        {
            isClimbing = true;
        }
        else
        {
            isClimbing = false;
        }
    }

    /*
     * Grapple and Swinging Added Code
     */
    private Vector3 velocityToSet;
    private bool enableMovementOnNextTouch;
    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
    {
        activeGrapple = true;

        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        Invoke(nameof(SetVelocity), 0.1f);

        Invoke(nameof(ResetRestrictions), 3f);
    }

    private void SetVelocity()
    {
        enableMovementOnNextTouch = true;
        //rb.velocity = velocityToSet;
    }

    public void ResetRestrictions()
    {
        activeGrapple = false;
    }

    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity)
            + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

        return velocityXZ + velocityY;
    }
}
