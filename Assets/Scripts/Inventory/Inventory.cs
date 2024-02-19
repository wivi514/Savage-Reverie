using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<PickableObject> items = new List<PickableObject>();

    public void AddItem(PickableObject item)
    {
        items.Add(item);
        Debug.Log("Added " + item.objectName + " to inventory.");
    }
}
