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


    public float slideSpeedIncrease;

    public float slideSpeedDecrease;

    public float wallRunSpeedIncrease;

    public float wallRunSpeedDecrease;



    float speed;

    public float runSpeed;

    public float airSpeed;

    public float sprintSpeed;

    public float crouchSpeed;


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
    bool onLeftWall;

    bool onRightWall;

    RaycastHit leftWallHit;

    RaycastHit rightWallHit;

    Vector3 wallNormal;

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
    }


    // Update is called once per frame
    void Update()
    {
        HandleInput();
        CheckWallRun();
        if (isGrounded && !isSliding)
        {
            GroundedMovement();
        }
        else if (!isGrounded && !isWallRunning)
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

        GroundedMovement();
        checkGround();
        controller.Move(move * Time.deltaTime);
        ApplyGravity();
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
            move.z += forwardDirection.z;
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

    void checkGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundMask);
        if (isGrounded)
        {
            jumpCharges = 1;
        }
    }

    void CheckWallRun()
    {
        onLeftWall = Physics.Raycast(transform.position, -transform.right, out leftWallHit, 0.7f, wallMask);
        onRightWall = Physics.Raycast(transform.position, -transform.right, out rightWallHit, 0.7f, wallMask);

        if((onRightWall || onLeftWall) && !isWallRunning)
        {
            WallRun();
        }
        if((!onRightWall || !onLeftWall) && isWallRunning)
        {
            ExitWallRun();
        }
    }

    void ApplyGravity()
    {
        gravity = isWallRunning ? wallRunGravity : normalGravity;
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
        
        Yvelocity.y = Mathf.Sqrt(jumpHeight * -2f * normalGravity);
        //jumpCharges = jumpCharges - 1;
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
        jumpCharges = 1;
        IncreaseSpeed(wallRunSpeedIncrease);
        Yvelocity = new Vector3(0f, 0f, 0f);

        wallNormal = onLeftWall ? leftWallHit.normal : rightWallHit.normal;
        forwardDirection = Vector3.Cross(wallNormal, Vector3.up);

        if (Vector3.Dot(forwardDirection, transform.forward) < 0)
        {
            forwardDirection = -forwardDirection;
        }
    }

    void ExitWallRun()
    {
        isWallRunning = false;
    }


}
