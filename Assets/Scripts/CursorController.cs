using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CursorController : MonoBehaviour
{

    [SerializeField] private Texture2D cursor;

    private Vector2 cursorHotspot; 

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        cursorHotspot = new Vector2(cursor.width/ 2, cursor.height/2);
        Cursor.SetCursor(cursor, cursorHotspot, CursorMode.Auto);
    }

}
