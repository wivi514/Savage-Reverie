using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Object Type", menuName = "Objects")]
public class PickableObject : ScriptableObject
{
    public string objectName; //Insert object name
    public string description; //Insert object description

    public ObjectType type; //Select the type of the object
    public Sprite uiArtwork; //Insert the sprite that will be shown in the UI it needs to be square

    public int weight; //Insert object weight that'll be used to slow down the player if he has too much weight on him
    public int value; //Insert monetary value of the object
}

//Here are all type of object that we'll sort in multiple menu depending of the type it is. Add more type if required(will also need to update the inventory UI accordingly)
public enum ObjectType
{
    Misc,
    Aid,
    Weapon
}
