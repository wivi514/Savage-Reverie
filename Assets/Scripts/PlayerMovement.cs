using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 10f;
<<<<<<< HEAD
    private float jumpForce = 5f; // Ajustez la force du saut selon vos besoins
=======
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

        Move(InputManager.movementInput); // Call the movement function in FixedUpdate

        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) // Check if the player is grounded and the spacebar is pressed
        {
            Jump(); // Call the jump function
        }
    }

    public void Move(Vector2 moveInput)
    {
        // Calculate the move direction based on camera orientation
        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 cameraRight = playerCamera.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        Vector3 moveDirection = cameraForward.normalized * moveInput.y + cameraRight.normalized * moveInput.x;

        if (isGrounded)
        {
            rb.velocity = new Vector3(moveDirection.x * speed, rb.velocity.y, moveDirection.z * speed);
        }
        else
        {
            rb.velocity = new Vector3(moveDirection.x * speed * speedJumpModifier, rb.velocity.y, moveDirection.z * speed * speedJumpModifier);
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
