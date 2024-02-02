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
        var playerControls = GetComponent<PlayerControls>(); // Obtient une référence au script des contrôles du joueur

        if (playerControls != null)
        {
            // Initialise toggleInventoryAction en utilisant le système d'actions d'entrée
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
