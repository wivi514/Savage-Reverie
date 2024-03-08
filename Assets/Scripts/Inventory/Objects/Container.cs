using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    public Inventory inventory; // Assign this in the inspector
    public GameObject uiTextPrefab; // Assign a prefab with a Text component

    private GameObject uiTextObject;

    void Update()
    {
        
    }
}
