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
    private TMP_Text NPCNameText;
    private RaycastHit previousHit;
    private bool hasPreviousHit = false;

    void Awake()
    {
        interactionText = GameObject.Find("Interaction Text").GetComponent<TMP_Text>();
        gameobjectText = GameObject.Find("GameObject Text").GetComponent<TMP_Text>();
        NPCNameText = GameObject.Find("NPC Name Text").GetComponent<TMP_Text>();

        if (interactionText == null || gameobjectText == null || NPCNameText == null)
        {
            Debug.LogError("UI Text components are not properly assigned in Interact.cs.");
        }
        else
        {
            interactionText.enabled = false;
            gameobjectText.enabled = false;
            NPCNameText.enabled = false;
        }
    }

    void Update()
    {
        PerformRaycastUpdateUI();
    }

    public void TryInteract()
    {
        if (hasPreviousHit && previousHit.collider != null)
        {
            Interactable interactableObject = previousHit.collider.GetComponent<Interactable>();
            if (interactableObject != null)
            {
                interactableObject.Interact();
                DialogueHolder dialogueHolder = interactableObject.GetComponent<DialogueHolder>();
                if (dialogueHolder != null)
                {
                    DialogueManager.Instance.StartDialogue(dialogueHolder.NPCDialogue);
                }
            }
            else
            {
                ClearUI();
            }
        }
        else
        {
            Debug.LogWarning("No valid hit to interact with.");
            ClearUI();
        }
    }

    void PerformRaycastUpdateUI()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            if ((!hasPreviousHit || previousHit.collider == null) || hit.collider.gameObject != previousHit.collider.gameObject)
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
        CharacterHealth characterHealth = hit.collider.GetComponent<CharacterHealth>();
        if (characterHealth == null)
        {
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
                NPCNameText.text = interactableObject.gameObject.name; //Show the name of who we are talking to
                NPCNameText.enabled = true;
                interactionText.enabled = false;
            }
        }
        //To see the inventory of dead character
        else if (characterHealth != null)
        {
            if (characterHealth.isAlive == false && containerInventory != null)
            {
                Inventory containerItems = interactableObject.GetComponent<Inventory>();
                UIManager.Instance.UpdateContainerUI(containerInventory.items); //Update the UI that show the inventory of the gameObject
                gameobjectText.text = interactableObject.gameObject.name; //Show the name of what we are looting
                gameobjectText.enabled = true;
            }
            else if (characterHealth.isAlive == true)
            {
                return;
            }
        }
        else
        {
            ClearUI();
        }
    }

    public void ClearUI()
    {
        interactionText.enabled = false;
        gameobjectText.enabled = false;
        NPCNameText.enabled = false;
        if (UIManager.Instance) UIManager.Instance.ClearContainerUI(); // Make sure to clear container UI as well
        if (DialogueManager.Instance) DialogueManager.Instance.ClearContainerUI(); // Make sure to clear container UI as well
        hasPreviousHit = false; // Reset this flag when UI is cleared
    }
}
