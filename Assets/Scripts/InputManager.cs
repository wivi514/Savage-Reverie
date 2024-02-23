using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerControls controls;
    Interact interact;
    PlayerMovement playerMovement;
    PlayerMenu playerMenu;

    public static Vector2 movementInput;
    public static bool isSprinting;

    private void Awake()
    {
        controls = new PlayerControls();

        interact = GameObject.Find("Interact").GetComponent<Interact>();

        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();

        playerMenu = GameObject.Find("Player Menu UI").GetComponent<PlayerMenu>();

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
        playerMovement.Jump();
    }

    //Interact with object when pressing the interact input
    private void InteractInput(InputAction.CallbackContext ctx)
    {
        interact.TryInteract();
    }

    //Make the player sprint when pressing the sprint input
    private void SprintInput(InputAction.CallbackContext ctx)
    {
        isSprinting = ctx.ReadValueAsButton();
    }

    private void StopSprint(InputAction.CallbackContext ctx)
    {
        isSprinting = false;
    }

    private void MenuInput(InputAction.CallbackContext context)
    {
        playerMenu.ToggleMenu();
    }
}
