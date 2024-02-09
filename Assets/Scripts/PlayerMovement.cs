using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 10f;
    private float jumpForce = 1;
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

    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce));
        }
    }
}
