using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using TMPro;
using UnityEngine.UI;

public class UiInventoryLayout : MonoBehaviour
{
    [Header("Money, Health and Weight text")]
    [SerializeField] TMP_Text moneyText;
    [SerializeField] TMP_Text weightText;
    [SerializeField] TMP_Text healthText;
    [SerializeField] LocalizedString localStringMoney;
    [SerializeField] LocalizedString localStringWeight;

    [Header("Inventory Slots")]
    [SerializeField] Transform slotsParent; // Parent GameObject of all SlotPanels
    public Inventory playerInventory;
    public GameObject[] slotPanels; // Array to hold the SlotPanel references

    [Header("Object Info")]
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text descriptionText;

    [Header("Character Info")]
    [SerializeField] CharacterSheet playerSheet; //located in Assets > Scriptable Objects > Characters
    [SerializeField] EquippedWeapon equippedWeaponComponent;
    [SerializeField] CharacterHealth healthComponent;

    [Header("Weapon Slots")]
    [SerializeField] GameObject gunLocation;
    private GameObject currentlyEquippedGun;

    void Start()
    {
        // Initialize slotPanels array with existing SlotPanel children
        slotPanels = new GameObject[slotsParent.childCount];
        for (int i = 0; i < slotsParent.childCount; i++)
        {
            slotPanels[i] = slotsParent.GetChild(i).gameObject;
        }
        for (int i = 0; i < slotPanels.Length; i++)
        {
            int index = i; // Local copy for closure
            Button slotButton = slotPanels[i].GetComponent<Button>();
            if (slotButton != null)
            {
                slotButton.onClick.AddListener(() => OnSlotClicked(index));
            }
        }
        UpdateInventoryUI();
    }

    void OnEnable()
    {
        localStringMoney.Arguments = new[] { playerSheet.money.ToString() };
        localStringWeight.Arguments = new[] { playerSheet.actualWeight.ToString() };
        UpdateHealthText();

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

    public void OnSlotClicked(int slotIndex)
    {
        if (slotIndex < playerInventory.inventoryItems.Count)
        {
            InventoryItem clickedItem = playerInventory.inventoryItems[slotIndex];
            nameText.text = clickedItem.item.name;
            descriptionText.text = clickedItem.item.description;
            if (clickedItem.item.type == ObjectType.Weapon)
            {
                // Assuming you have a reference to the EquippedWeapon component on the player
                // This will be your method to equip the weapon
                if(currentlyEquippedGun != null)
                {
                    Destroy(currentlyEquippedGun);
                }
                equippedWeaponComponent.EquipWeapon(clickedItem.item);
                currentlyEquippedGun = Instantiate(clickedItem.item.PickableObjectPrefab, gunLocation.transform);
            }
            if (clickedItem.item.type == ObjectType.Aid)
            {
                // Heal the player
                healthComponent.HealHealth(clickedItem.item.health);
                UpdateHealthText();

                // Remove one aid item from the inventory
                RemoveItem(clickedItem.item, slotIndex);
            }
        }
    }
    public void RemoveItem(PickableObject itemToRemove, int slotIndex)
    {
        InventoryItem inventoryItem = playerInventory.inventoryItems[slotIndex];

        // Decrease the quantity or remove the item
        if (inventoryItem.quantity > 1)
        {
            // Just decrease the quantity if there's more than one
            inventoryItem.quantity--;
        }
        else
        {
            // Remove the item entirely if it was the last one
            playerInventory.inventoryItems.RemoveAt(slotIndex);
            playerInventory.items.Remove(itemToRemove);
        }

        // Update the UI to reflect the changes
        UpdateInventoryUI();
    }

    private void UpdateMoneyText(string value)
    {
        moneyText.text = value;
    }

    private void UpdateWeightText(string value)
    {
        weightText.text = value + playerSheet.weightLimit.ToString(); //Stupid way to do it but it works and I can't find any info online on how to add two arguments on a localization
    }

    private void UpdateHealthText()
    {
        healthText.text = "Health: " + playerSheet.currentHealth + "/" + playerSheet.maxHealth;
    }
}
