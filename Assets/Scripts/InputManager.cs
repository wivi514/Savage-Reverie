using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerControls controls;
    Interact interact;
    PlayerMovement playerMovement;
    PlayerMenu playerMenu;
    AimDownSight aimDownSight;
    [SerializeField] EquippedWeapon equippedWeapon; //Need to change how this is done

    public static Vector2 movementInput;
    public static bool isSprinting;

    private void Awake()
    {
        controls = new PlayerControls();

        interact = GameObject.Find("Interact").GetComponent<Interact>();

        playerMovement = GameObject.Find("Capsule and Scripts").GetComponent<PlayerMovement>();

        playerMenu = GameObject.Find("Player Menu UI").GetComponent<PlayerMenu>();

        aimDownSight = GameObject.Find("Capsule and Scripts").GetComponent<AimDownSight>();

        // Enable the New Input System from unity
        controls.Player.Enable();

        // Will need to be placed somewhere else later in development
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        controls.Player.Movement.performed += MoveInput;
        controls.Player.Movement.canceled += StopMove;
        controls.Player.Jump.performed += JumpInput;
        controls.Player.Sprint.performed += SprintInput;
        controls.Player.Sprint.canceled += StopSprint;

        controls.Player.Interact.performed += InteractInput;

        controls.Player.Menu.performed += MenuInput;

        controls.Player.Aim.performed += StartAiming;
        controls.Player.Aim.canceled += StopAiming;

        controls.Player.Shoot.performed += ShootInput;

        controls.Player.ScrollWheel.performed += ScrollInput;
    }

    private void OnDisable()
    {
        controls.Player.Movement.performed -= MoveInput;
        controls.Player.Movement.canceled -= StopMove;
        controls.Player.Jump.performed -= JumpInput;
        controls.Player.Sprint.performed -= SprintInput;
        controls.Player.Sprint.canceled -= StopSprint;

        controls.Player.Interact.performed -= InteractInput;

        controls.Player.Menu.performed -= MenuInput;

        controls.Player.Aim.performed -= StartAiming;
        controls.Player.Aim.canceled -= StopAiming;

        controls.Player.Shoot.performed -= ShootInput;

        controls.Player.ScrollWheel.performed -= ScrollInput;
    }

    //Get the Input from the movement key
    private void MoveInput(InputAction.CallbackContext ctx)
    {
        // Grab the input and set it as a vector2 variable
        movementInput = ctx.ReadValue<Vector2>();
        Debug.Log(movementInput);
    }

    //Get when you stop pressing the movement key
    private void StopMove(InputAction.CallbackContext ctx)
    {
        // When movement is canceled, set the movement input to zero to stop the player
        movementInput = Vector2.zero;
    }

    // Make the player jump when pressing the jump input
    private void JumpInput(InputAction.CallbackContext ctx)
    {
        Debug.Log("Jumping");
        playerMovement.Jump();
    }

    //Interact with object when pressing the interact input
    private void InteractInput(InputAction.CallbackContext ctx)
    {
        interact.TryInteract();
        UIManager.Instance.TakeSelectedItem(); // Call a method to handle item pickup
        DialogueManager.Instance.ConfirmResponse();
    }

    //Make the player sprint when pressing the sprint input
    private void SprintInput(InputAction.CallbackContext ctx)
    {
        isSprinting = ctx.ReadValueAsButton();
    }

    //When the player stop holding the sprint button it stop sprinting
    private void StopSprint(InputAction.CallbackContext ctx)
    {
        isSprinting = false;
    }

    private void MenuInput(InputAction.CallbackContext context)
    {
        playerMenu.ToggleMenu();
    }

    private void StartAiming(InputAction.CallbackContext context)
    {
        aimDownSight.Aiming(true);
    }

    private void StopAiming(InputAction.CallbackContext context)
    {
        aimDownSight.Aiming(false);
    }

    private void ShootInput(InputAction.CallbackContext context)
    {
        equippedWeapon.shoot();
    }

    private void ScrollInput(InputAction.CallbackContext ctx)
    {
        float scroll = ctx.ReadValue<float>(); // Read the scroll value
        UIManager.Instance.SelectItemByScroll(scroll); // Call a method to handle the selection
        DialogueManager.Instance.ChangeResponseSelection(scroll);
    }
}
