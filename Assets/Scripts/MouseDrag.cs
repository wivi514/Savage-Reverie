using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewBehaviourScript : MonoBehaviour, IPointerDownHandler
{
    public float zDistance = 10; //la distance entre l'objet et le Camera
    public Camera mainCamera;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name + " was clicked!");
    }

    private void Awake()
    {
        this.enabled = false;//
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        zDistance = Vector3.Distance(mainCamera.transform.position, transform.position);
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            this.enabled = false;

            Vector3 mousePos = Input.mousePosition;

            Vector3 dragPos = mainCamera.ScreenToViewportPoint(new Vector3(mousePos.x, mousePos.y, zDistance));

            transform.position = dragPos;
        }
    }
}
