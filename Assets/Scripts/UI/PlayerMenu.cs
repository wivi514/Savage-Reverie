using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used for everything in the player menu that isn't specific to a player menu tab such as stat, inventory, etc.
public class PlayerMenu : MonoBehaviour
{
    [SerializeField] GameObject playerMenuUi;
    [SerializeField] GameObject playerUi;

    public void ToggleMenu()
    {
        bool isMenuActive = playerMenuUi.activeSelf; // Check if the menu is currently active
        bool isGameActive = playerUi.activeSelf; // Check if the game UI is currently active

        // Toggle the menu state
        playerMenuUi.SetActive(!isMenuActive);
        playerUi.SetActive(!isGameActive);

        // Adjust time scale and cursor visibility based on the menu state
        if (!isMenuActive)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Debug.Log("Opening player menu");
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Debug.Log("Closing player menu");
        }
    }
}
