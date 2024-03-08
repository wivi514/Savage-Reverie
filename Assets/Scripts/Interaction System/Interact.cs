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
            Interactable interactableObject = previousHit.collider.GetComponent<Interactable>();
            if (interactableObject != null)
            {
                interactableObject.Interact();
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
        if (interactableObject != null && containerInventory == null)
        {
            interactionText.text = "E) Interact";
            interactionText.enabled = true;
            gameobjectText.text = interactableObject.gameObject.name;
            gameobjectText.enabled = true;
        }
        else if (interactableObject != null && containerInventory != null)
        {
            string[] itemNames = containerInventory.items.Select(item => item.objectName).ToArray();
            UIManager.Instance.UpdateContainerUI(itemNames);
            gameobjectText.text = interactableObject.gameObject.name;
            gameobjectText.enabled = true;
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
