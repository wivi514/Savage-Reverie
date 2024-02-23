using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//Script that will be used to show the information from an object in the UI
public class UiObjectInformation : MonoBehaviour
{
    [SerializeField] PickableObject scriptableObject;

    public TMP_Text objectName;
    public TMP_Text description;

    public Image uiArtwork;

    public TMP_Text weight;
    public TMP_Text value;

    void Start()
    {
        if (scriptableObject != null)
        {
            //Assign information that will be shown in the UI from the scriptable object
            objectName.text = scriptableObject.objectName;
            description.text = scriptableObject.description;

            uiArtwork.sprite = scriptableObject.uiArtwork;

            weight.text = scriptableObject.weight.ToString();
            value.text = scriptableObject.value.ToString();
        }
        else
        {
            Debug.LogWarning("Scriptable Object is not assigned to ObjectInformation script!");
        }
    }
}
