using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerQueue : MonoBehaviour
{
    [SerializeField] float queueGap = 0.5f;
    [SerializeField] int topOrderLayer = 20;

    public float GetQueueGap()
    {
        return queueGap;
    }

    public int GetTopOrderLayer()
    {
        return topOrderLayer;
    }
  
    public GameObject GetCurrentCustomer()
    {
        /* Return top child gameobject */
        return transform.GetChild(0).gameObject;
    }

    public int GetCustomerCount()
    {
        return transform.childCount;
    }
}
