using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 10f;

    private float sprintSpeedMultiplier = 1.75f; // 75% faster than basic movement
    private float sprintSpeed;

    private float jumpForce = 250f;
    public bool isGrounded;
    private byte raycastJump = 1;
    private float speedJumpModifier = 0.5f; // Speed multiplier when player jumps

    private Rigidbody rb;
    private Camera playerCamera;

    [SerializeField] CharacterSheet characterSheetPlayer;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = Camera.main;

        sprintSpeed = speed * sprintSpeedMultiplier;

        Debug.Log(characterSheetPlayer.GetSubSkillLevel("Survival", "Athletics") / 100 + 1);
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
        Move(InputManager.movementInput, InputManager.isSprinting); // Call the movement function in FixedUpdate
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
            rb.velocity = new Vector3(moveDirection.x * currentSpeed * (characterSheetPlayer.GetSubSkillLevel("Survival", "Athletics")/100 + 1), rb.velocity.y, moveDirection.z * currentSpeed * (characterSheetPlayer.GetSubSkillLevel("Survival", "Athletics") / 100 + 1));
        }
        else
        {
            rb.velocity = new Vector3(moveDirection.x * currentSpeed * speedJumpModifier * (characterSheetPlayer.GetSubSkillLevel("Survival", "Athletics") / 100 + 1), rb.velocity.y, moveDirection.z * currentSpeed * speedJumpModifier * (characterSheetPlayer.GetSubSkillLevel("Survival", "Athletics") / 100 + 1));
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
