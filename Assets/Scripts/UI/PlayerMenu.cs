using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used for everything in the player menu that isn't specific to a player menu tab such as stat, inventory, etc.
public class PlayerMenu : MonoBehaviour
{
    [SerializeField] GameObject pMenuUi;

    public void ToggleMenu()
    {
        bool isMenuActive = pMenuUi.activeSelf; // Check if the menu is currently active

        // Toggle the menu state
        pMenuUi.SetActive(!isMenuActive);

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
