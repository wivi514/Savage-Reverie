using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventorySystem : MonoBehaviour
{
    public Canvas inventoryPanel;
    private InputAction toggleInventoryAction;

    private void Start()
    {
        var playerControls = GetComponent<PlayerControls>(); // Obtient une r�f�rence au script des contr�les du joueur

        if (playerControls != null)
        {
            // Initialise toggleInventoryAction en utilisant le syst�me d'actions d'entr�e
            toggleInventoryAction = playerControls.Player.Inventory;
        }
        else
        {
            Debug.LogError("PlayerControls script not found on the GameObject");
        }
    }

    public void ToggleInventory()
    {
        inventoryPanel.enabled = !inventoryPanel.enabled;
    }
}
