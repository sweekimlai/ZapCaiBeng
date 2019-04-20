using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickGroup : MonoBehaviour
{
    public int GetChildCount()
    {
        return transform.childCount;
    }

    public GameObject GetTick(int index)
    {
        return transform.GetChild(index).gameObject;
    }

    public void DestroyAllChildObject()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
