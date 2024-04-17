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
    public List<PickableObject> containerItems; // This holds the items in the container inventory

    public Transform itemsContainer; // Assign the 'Item list content' Transform here

    public Color defaultColor = Color.white; // Default color for non-selected items
    public Color selectedColor = new Color(1f, 0.51f, 0f); // Orange color for selected items

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

    public void UpdateContainerUI(List<PickableObject> items)
    {
        ClearContainerUI();
        containerItems = items;
        foreach (var item in containerItems)
        {
            GameObject itemUI = Instantiate(uiPanelPrefab, contentPanel);
            Text itemText = itemUI.GetComponentInChildren<Text>();
            itemText.text = item.objectName; 
        }
        // Make sure the first item is selected by default
        selectedItemIndex = 0;
        HighlightSelectedItem();
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
        if (containerItems == null || containerItems.Count == 0) return;

        if (scrollAmount < 0) selectedItemIndex = Mathf.Min(selectedItemIndex + 1, containerItems.Count - 1);
        else if (scrollAmount > 0) selectedItemIndex = Mathf.Max(selectedItemIndex - 1, 0);

        HighlightSelectedItem();
    }

    public void TakeSelectedItem()
    {
        if (selectedItemIndex >= 0 && selectedItemIndex < containerItems.Count)
        {
            PickableObject itemToTake = containerItems[selectedItemIndex];
            playerInventory.AddItem(itemToTake);
            containerItems.RemoveAt(selectedItemIndex);
            UpdateContainerUI(containerItems); 
        }
    }

    private void HighlightSelectedItem()
    {
        for (int i = 0; i < contentPanel.childCount; i++)
        {
            Text itemText = contentPanel.GetChild(i).GetComponentInChildren<Text>();
            itemText.color = i == selectedItemIndex ? selectedColor : defaultColor;
        }
    }
}
