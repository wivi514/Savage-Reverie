
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Raycaster : MonoBehaviour
{
    public Transform mainCamera;

    private void Start()
    {
        mainCamera = Camera.main.transform;
    }

    //private void Update()
    //{
    //    //clique l'objet, puis l'objet commencer a bouger.

    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hit = new RaycastHit();
    //        if (Physics.Raycast(ray, out hit, 1000))
    //        {
    //            MouseDrag mDrag = hit.collider.GetComponent<MouseDrag>();
    //            if (mDrag) mDrag.enabled = true;//enable the script.

    //        }
    //    }
    //}
}