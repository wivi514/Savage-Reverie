using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Object Type", menuName = "Objects")]
public class PickableObject : ScriptableObject
{
    public string objectName;
    public string description;

    public int weight;
}
