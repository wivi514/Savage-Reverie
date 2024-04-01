using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public GameObject uiPanelPrefab; // Assign your UI panel prefab
    public Transform contentPanel; // Assign the content panel inside your ScrollRect

    public List<PickableObject> items; // This should hold the items in your inventory
    public Inventory playerInventory; // Reference to the player's inventory component

    public Transform itemsContainer; // Assign the 'Item list content' Transform here


    private int selectedItemIndex = 0;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateContainerUI(string[] itemNames)
    {
        ClearContainerUI(); // Clear previous UI elements
        foreach (string itemName in itemNames)
        {
            GameObject itemUI = Instantiate(uiPanelPrefab, contentPanel);
            // Ensure the prefab instantiated is the item entry prefab, not the inventory panel itself
            itemUI.GetComponentInChildren<Text>().text = itemName;
        }
    }

    public void ClearContainerUI()
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }
    }

    public void SelectItemByScroll(float scrollAmount)
    {
        if (scrollAmount > 0) selectedItemIndex++;
        else if (scrollAmount < 0) selectedItemIndex--;

        // Clamp the index to the bounds of your item list
        selectedItemIndex = Mathf.Clamp(selectedItemIndex, 0, items.Count - 1);

        HighlightSelectedItem();
    }

    public void TakeSelectedItem()
    {
        if (selectedItemIndex >= 0 && selectedItemIndex < items.Count)
        {
            // Logic to remove the item from the inventory and add it to the player
            // For example:
            PickableObject itemToTake = items[selectedItemIndex];
            playerInventory.AddItem(itemToTake);
            items.RemoveAt(selectedItemIndex);
            //UpdateContainerUI(); // Refresh UI after taking an item
        }
    }

    private void HighlightSelectedItem()
    {
        // Update your UI to highlight the selected item
        for (int i = 0; i < items.Count; i++)
        {
            Transform itemTransform = itemsContainer.GetChild(i);
            Image background = itemTransform.GetComponent<Image>();
            if (i == selectedItemIndex) background.color = Color.yellow; // Highlight color
            else background.color = Color.clear; // Normal color
        }
    }
}
