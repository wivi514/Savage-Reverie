using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Object Type", menuName = "Objects")]
public class PickableObject : ScriptableObject
{
    public string objectName;
    public string description;
    public ObjectType type;

    public int weight;
    public int value;
}

public enum ObjectType
{
    Misc,
    Aid,
    Weapon
}
