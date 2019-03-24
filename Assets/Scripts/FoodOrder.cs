using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodOrder : MonoBehaviour
{
    public List<GameObject> GetCurrentOrder()
    {
        /*
            Return all child gameobjects
        */
        List<GameObject> currentOrder = new List<GameObject>();
        for(int i = 0; i < transform.childCount; i++)
        {
            currentOrder.Add(transform.GetChild(i).gameObject);
        }
        return currentOrder;
    }

    public void DestroyAllFoodOrder()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
