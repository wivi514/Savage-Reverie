using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class SlotsClass : MonoBehaviour
{
   [SerializeField] private PickableObject item;
   [SerializeField] private int quantity;

    public SlotsClass()
    {
        item = null;
        quantity = 0;
    }

    public SlotsClass(PickableObject _item, int _quantity)
    {
        item = _item;
        quantity = _quantity;     }

    public PickableObject GetItem()
    {
        return item;
    }

    public int GetQuantity() 
    { 
      return quantity;
    }

    public void AddQuantity(int _quantity)
    {
        quantity += _quantity;
    }
    public void SubQuantity(int _quantity)
    {
        quantity -= _quantity;
    }
}
