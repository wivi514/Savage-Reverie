using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public PickableObject item;
    public int quantity;
}

public class Inventory : MonoBehaviour
{
    public List<PickableObject> items = new List<PickableObject>();
    public List<InventoryItem> inventoryItems = new List<InventoryItem>(); // New list to handle stackable items

    public void AddItem(PickableObject itemToAdd)
    {
        // Check if the item can be stacked
        if (itemToAdd.canStack)
        {
            // Look for the item in the inventoryItems list
            InventoryItem existingItem = inventoryItems.Find(x => x.item == itemToAdd);
            if (existingItem != null)
            {
                // If the item exists and is stackable, just increase the quantity
                existingItem.quantity++;
            }
            else
            {
                // If the item is not found, add it as a new InventoryItem
                inventoryItems.Add(new InventoryItem { item = itemToAdd, quantity = 1 });
            }
        }
        else
        {
            // If the item does not stack, add it as a new InventoryItem
            inventoryItems.Add(new InventoryItem { item = itemToAdd, quantity = 1 });
        }

        // Assuming AddItem is the only place that modifies the inventory, we don't need to sync the items list here
        // SyncInventoryItems() will be called outside of this method after all items are added
        Debug.Log($"Added {itemToAdd.objectName} to inventory.");
    }

    public string[] GetItemNames()
    {
        // You may need to adjust this method if you want it to include stackable items
        return items.Select(item => item.objectName).ToArray();
    }

    // Call this method to synchronize the inventoryItems list with the items list
    public void SyncInventoryItems()
    {
        // Clear the inventoryItems list to start fresh
        inventoryItems.Clear();

        // Iterate through the items list to repopulate inventoryItems
        foreach (var item in items)
        {
            // Since this is a non-stackable list, add each item as a new InventoryItem
            inventoryItems.Add(new InventoryItem { item = item, quantity = 1 });
        }

        // Use a dictionary to consolidate stackable items
        Dictionary<PickableObject, InventoryItem> consolidationDict = new Dictionary<PickableObject, InventoryItem>();
        foreach (var invItem in inventoryItems)
        {
            if (invItem.item.canStack)
            {
                if (consolidationDict.TryGetValue(invItem.item, out InventoryItem existingItem))
                {
                    // Increase the quantity of the existing entry
                    existingItem.quantity += invItem.quantity;
                }
                else
                {
                    // Add a new entry to the dictionary
                    consolidationDict[invItem.item] = invItem;
                }
            }
        }

        // Clear and update inventoryItems with the consolidated items
        inventoryItems.Clear();
        foreach (var kvp in consolidationDict)
        {
            inventoryItems.Add(kvp.Value);
        }

        // Now inventoryItems contains both stackable (consolidated) and non-stackable items
    }
}

