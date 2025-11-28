using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 playerVelocity;
    private Animator animator;
    int isWalkingHash = Animator.StringToHash("IsWalking");
    int isRunningHash = Animator.StringToHash("IsRunning");

    private bool isGrounded;
    private bool lerpCrouch = false;
    private bool crouching = false;
    private bool sprinting = false;


    public float speed = 5f;
    public float RunSpeedMultiplier = 2f;
    public float gravity = -9.8f;
    public float crouchTimer = 1;

    public float jumpHeight = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Assing Character Controller
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("IsWalking");
        isRunningHash = Animator.StringToHash("IsRunning");

    }

    // Update is called once per frame
    void Update()
    {
        //Check if player is on the ground
        isGrounded = characterController.isGrounded;
        //Crouch Lerp
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            if (crouching)
                characterController.height = Mathf.Lerp(characterController.height, 1, p);
            else
                characterController.height = Mathf.Lerp(characterController.height, 2, p);

            if ( p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }
    // receive the inputs from InputManger.cs and apply them to character controller
    public void ProcessMove(Vector2 input)
    {
        // Get Player Move Direction
        Vector3 moveDirection = Vector3.zero;

        //Getting the 2D Vector on apply them to the 3D Vector
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        //Calling the Move function
        characterController.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;

        //Reset y velocity when on the ground
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        characterController.Move(playerVelocity * Time.deltaTime);

        //Apply Animation
        bool isWalking = animator.GetBool(isWalkingHash);
        if (moveDirection != Vector3.zero)
        {
            animator.SetBool(isWalkingHash,true);
        }

        //Stop Walking Animation when no input
        if (moveDirection == Vector3.zero)
        {
            animator.SetBool(isWalkingHash, false);
            animator.SetBool(isRunningHash, false);
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
                  
    }

    public void Sprint()
    {
        sprinting = !sprinting;
        if (sprinting)
        {
            animator.SetBool(isRunningHash, true);
            speed = 1;
        }
            
        else
            animator.SetBool(isRunningHash, false);
            speed = 5;

    }
}
