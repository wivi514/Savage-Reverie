using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class SceneObjectInformation : MonoBehaviour
{
    public PickableObject scriptableObject;
    void Start()
    {
         if (scriptableObject == null)
        {
            Debug.Log("Please add a scriptable object to " + gameObject.name);
        }
    }
}
