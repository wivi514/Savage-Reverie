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

    // Définissez la touche de saut
    private string jumpKey = "Jump";

    public static Vector2 movementInput;

    private void Awake()
    {
        controls = new PlayerControls();

        interact = GameObject.Find("Player").GetComponent<Interact>();

        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();

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

        controls.Player.Interact.performed += InteractInput;
    }

    private void OnDisable()
    {
        controls.Player.Movement.performed -= MoveInput;
        controls.Player.Movement.canceled -= StopMove;
        controls.Player.Jump.performed -= JumpInput;

        controls.Player.Interact.performed -= InteractInput;
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

<<<<<<< HEAD
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
=======
    private void JumpInput(InputAction.CallbackContext ctx)
    {
        playerMovement.Jump();
    }

    private void InteractInput(InputAction.CallbackContext ctx)
    {
        interact.TryInteract();
>>>>>>> 9c7fbdd5ba4495fcbeec6530f37ea97d18e8a9d8
    }
}
