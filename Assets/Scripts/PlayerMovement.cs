using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 10f;
<<<<<<< HEAD
    private float sprintSpeedMultiplier = 1.75f; // 75% faster than basic movement
    private float sprintSpeed;

=======
<<<<<<< HEAD
    private float jumpForce = 5f; // Ajustez la force du saut selon vos besoins
=======
>>>>>>> 79fe966ef40b5929ee5d7d05a71c040532056fd5
    private float jumpForce = 250f;
>>>>>>> 9c7fbdd5ba4495fcbeec6530f37ea97d18e8a9d8
    public bool isGrounded = false;
    private byte raycastJump = 1;
    private float speedJumpModifier = 0.5f; // Speed multiplier when player jumps

    private Rigidbody rb;
    private Camera playerCamera;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = Camera.main;

        sprintSpeed = speed * sprintSpeedMultiplier;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Do a raycast down to see if the player is on the ground
        if (Physics.Raycast(transform.position, Vector3.down, raycastJump))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

<<<<<<< HEAD
        Move(InputManager.movementInput, InputManager.isSprinting); // Call the movement function in FixedUpdate
=======
        Move(InputManager.movementInput); // Call the movement function in FixedUpdate

        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) // Check if the player is grounded and the spacebar is pressed
        {
            Jump(); // Call the jump function
        }
>>>>>>> 79fe966ef40b5929ee5d7d05a71c040532056fd5
    }

    public void Move(Vector2 moveInput, bool isSprinting)
    {
        // Calculate the move direction based on camera orientation
        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 cameraRight = playerCamera.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        Vector3 moveDirection = cameraForward.normalized * moveInput.y + cameraRight.normalized * moveInput.x;

        float currentSpeed = isSprinting ? sprintSpeed : speed; // Choose current speed based on sprint input

        if (isGrounded)
        {
            rb.velocity = new Vector3(moveDirection.x * currentSpeed, rb.velocity.y, moveDirection.z * currentSpeed);
        }
        else
        {
            rb.velocity = new Vector3(moveDirection.x * currentSpeed * speedJumpModifier, rb.velocity.y, moveDirection.z * currentSpeed * speedJumpModifier);
        }
    }

<<<<<<< HEAD
    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Apply a vertical force to simulate jumping
=======
    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce));
            Debug.Log("Is Jumping");
        }
>>>>>>> 9c7fbdd5ba4495fcbeec6530f37ea97d18e8a9d8
    }
}
