using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicksDisplay : MonoBehaviour
{
    [SerializeField] GameObject tick;

    public GameObject GetTick()
    {
        return tick;
    }

    public int GetTicksCount()
    {
        return transform.childCount;
    }

    public void DestroyAllTicks()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
