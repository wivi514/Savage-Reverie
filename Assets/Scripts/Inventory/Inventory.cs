using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<PickableObject> items = new List<PickableObject>();

    public void AddItem(PickableObject item)
    {
        items.Add(item);
        Debug.Log("Added " + item.objectName + " to inventory.");
    }

    public string[] GetItemNames()
    {
        return items.Select(item => item.objectName).ToArray();
    }
}
