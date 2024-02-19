using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerControls controls;
    Interact interact;

    // Définissez la touche de saut
    private string jumpKey = "Jump";

    public static Vector2 movementInput;

    private void Awake()
    {
        controls = new PlayerControls();

        interact = GameObject.Find("Player").GetComponent<Interact>();

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

        controls.Player.Interact.performed += interact.TryInteract;
    }

    private void OnDisable()
    {
        controls.Player.Movement.performed -= MoveInput;
        controls.Player.Movement.canceled -= StopMove;

        controls.Player.Interact.performed -= interact.TryInteract;
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

    private void Update()
    {
        // Appel de la fonction de saut
        Jump();
    }

    private void Jump()
    {
        // Vérifie si le joueur appuie sur la touche de saut et s'il est au sol
        if (Input.GetKeyDown(jumpKey) && pm.isGrounded)
        {
            pm.Jump();
        }
    }
}
