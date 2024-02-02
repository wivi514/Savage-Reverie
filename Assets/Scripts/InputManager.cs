using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private InventorySystem inventorySystem;

    PlayerControls controls;
    PlayerMovement pm;

    public static Vector2 movementInput;

    private void Awake()
    {
        controls = new PlayerControls();
        inventorySystem = GetComponent<InventorySystem>();
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();

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

        controls.Player.Inventory.performed += ctx => inventorySystem.ToggleInventory();

    }

    private void OnDisable()
    {
        controls.Player.Movement.performed -= MoveInput;
        controls.Player.Movement.canceled -= StopMove;

        controls.Player.Inventory.performed -= ctx => inventorySystem.ToggleInventory();
    }

    private void MoveInput(InputAction.CallbackContext ctx)
    {
        // Grab the input and set it as a vector2 variable
        movementInput = ctx.ReadValue<Vector2>();
        Debug.Log(movementInput);
    }

    private void StopMove(InputAction.CallbackContext ctx)
    {
        // When movement is canceled, set the movement input to zero to stop the player
        movementInput = Vector2.zero;
    }

}
