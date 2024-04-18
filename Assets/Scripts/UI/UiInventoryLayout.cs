using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using TMPro;
using UnityEngine.UI;

public class UiInventoryLayout : MonoBehaviour
{
    [Header("Money and Weight text")]
    [SerializeField] TMP_Text moneyText;
    [SerializeField] TMP_Text weightText;
    [SerializeField] LocalizedString localStringMoney;
    [SerializeField] LocalizedString localStringWeight;

    [Header("Inventory Slots")]
    [SerializeField] Transform slotsParent; // Parent GameObject of all SlotPanels
    public Inventory playerInventory;
    public GameObject[] slotPanels; // Array to hold the SlotPanel references

    [Header("Character Info")]
    [SerializeField] CharacterSheet playerSheet; //located in Assets > Scriptable Objects > Characters

    void Start()
    {
        // Initialize slotPanels array with existing SlotPanel children
        slotPanels = new GameObject[slotsParent.childCount];
        for (int i = 0; i < slotsParent.childCount; i++)
        {
            slotPanels[i] = slotsParent.GetChild(i).gameObject;
        }
        UpdateInventoryUI();
    }

    void OnEnable()
    {
        localStringMoney.Arguments = new[] { playerSheet.money.ToString() };
        localStringWeight.Arguments = new[] { playerSheet.actualWeight.ToString() };

        localStringMoney.StringChanged += UpdateMoneyText;
        localStringWeight.StringChanged += UpdateWeightText;

        UpdateInventoryUI();
    }

    private void OnDisable()
    {
        localStringMoney.StringChanged -= UpdateMoneyText;
        localStringWeight.StringChanged -= UpdateWeightText;
    }

    public void UpdateInventoryUI()
    {
        Debug.Log("Updating Inventory UI");

        // Debugging: Check the state of inventory items
        Debug.Log("Number of items in player inventory: " + playerInventory.inventoryItems.Count);

        // Loop through each SlotPanel and update its UI elements
        for (int i = 0; i < slotPanels.Length; i++)
        {
            Image itemImage = slotPanels[i].GetComponentInChildren<Image>(true); // Include inactive children
            TMP_Text quantityText = slotPanels[i].GetComponentInChildren<TMP_Text>(true); // Include inactive children

            // Debugging: Ensure the components are found
            if (itemImage == null)
            {
                Debug.LogError("No Image component found for slot at index " + i);
                continue;
            }
            if (quantityText == null)
            {
                Debug.LogError("No TMP_Text component found for slot at index " + i);
                continue;
            }

            if (i < playerInventory.inventoryItems.Count)
            {
                InventoryItem inventoryItem = playerInventory.inventoryItems[i];

                // Debugging: Log item details to check their state
                Debug.Log($"Slot {i}: Item {inventoryItem.item.objectName}, Quantity {inventoryItem.quantity}");

                // Set the sprite and quantity if the item exists
                itemImage.sprite = inventoryItem.item.uiArtwork;
                itemImage.enabled = true; // Enable the image component in case it was disabled
                quantityText.text = inventoryItem.item.canStack && inventoryItem.quantity > 1 ? inventoryItem.quantity.ToString() : "";
            }
            else
            {
                // If there are no items for this slot, clear or disable the UI elements
                itemImage.sprite = null;
                itemImage.enabled = false; // Optional: disable the image component to hide the default sprite
                quantityText.text = "";
            }
        }
    }


    private void UpdateMoneyText(string value)
    {
        moneyText.text = value;
    }

    private void UpdateWeightText(string value)
    {
        weightText.text = value + playerSheet.weightLimit.ToString(); //Stupid way to do it but it works and I can't find any info online on how to add two arguments
    }
}
