using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Linq;

public class Interact : MonoBehaviour
{
    public float interactionRange = 3f;
    private TMP_Text interactionText;
    private TMP_Text gameobjectText;
    private RaycastHit previousHit;
    private bool hasPreviousHit = false;

    void Awake()
    {
        interactionText = GameObject.Find("Interaction Text").GetComponent<TMP_Text>();
        gameobjectText = GameObject.Find("GameObject Text").GetComponent<TMP_Text>();
        interactionText.enabled = false;
        gameobjectText.enabled = false;
    }

    void Update()
    {
        PerformRaycastUpdateUI();
    }

    public void TryInteract()
    {
        if (hasPreviousHit)
        {
            // Attempt to interact with the object
            Interactable interactableObject = previousHit.collider.GetComponent<Interactable>();
            if (interactableObject != null)
            {
                interactableObject.Interact();
                DialogueHolder dialogueHolder = interactableObject.GetComponent<DialogueHolder>();
                if (dialogueHolder != null)
                {
                    // Trigger dialogue UI update
                    DialogueManager.Instance.StartDialogue(dialogueHolder.NPCDialogue);
                }
            }
        }
    }

    void PerformRaycastUpdateUI()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            if (!hasPreviousHit || hit.collider.gameObject != previousHit.collider.gameObject)
            {
                UpdateInteractionUI(hit);
            }
        }
        else if (hasPreviousHit)
        {
            ClearUI();
        }
        previousHit = hit;
        hasPreviousHit = hit.collider != null;
    }

    void UpdateInteractionUI(RaycastHit hit)
    {
        Interactable interactableObject = hit.collider.GetComponent<Interactable>();
        Inventory containerInventory = hit.collider.GetComponent<Inventory>();
        DialogueHolder dialogueHolder = hit.collider.GetComponent<DialogueHolder>();
        if (interactableObject != null && containerInventory == null && dialogueHolder == null)
        {
            interactionText.text = "E) Interact";
            interactionText.enabled = true;
            gameobjectText.text = interactableObject.gameObject.name;
            gameobjectText.enabled = true;
        }
        // If the object is has an inventory
        else if (interactableObject != null && containerInventory != null)
        {
            Inventory containerItems = interactableObject.GetComponent<Inventory>();
            UIManager.Instance.UpdateContainerUI(containerInventory.items); //Update the UI that show the inventory of the gameObject
            gameobjectText.text = interactableObject.gameObject.name; //Show the name of what we are looting
            gameobjectText.enabled = true;
        }
        // If the object has a dialogue
        else if (interactableObject != null && dialogueHolder != null)
        {
            // Trigger dialogue UI update here
            DialogueManager.Instance.StartDialogue(dialogueHolder.NPCDialogue);
            // You might want to handle other interactions like disabling player movement, etc.
            interactionText.enabled = false;
        }
        else
        {
            ClearUI();
        }
    }

    void ClearUI()
    {
        interactionText.enabled = false;
        gameobjectText.enabled = false;
        if (UIManager.Instance) UIManager.Instance.ClearContainerUI(); // Make sure to clear container UI as well
        hasPreviousHit = false; // Reset this flag when UI is cleared
    }
}
