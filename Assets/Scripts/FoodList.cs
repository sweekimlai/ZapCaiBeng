using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FoodList : MonoBehaviour
{
    [SerializeField] Food[] allFoodArray = new Food[8];

    public Food[] GetFoodArray(int orderNum)
    {
        /* Return a non repeated food order in list */
        if (orderNum > allFoodArray.Length) { return null; }
        Food[] randomFoodArray = new Food[orderNum];
        for(int i = 0; i < orderNum; i++)
        {
            bool found = true;
            while(found)
            {
                found = false;
                int rnd = Random.Range(0, allFoodArray.Length);
                Food food = allFoodArray[rnd];
                if(!randomFoodArray.Contains(food))
                {
                    randomFoodArray[i] = food;
                }
                else
                {
                    found = true;
                }
            }
        }
        return randomFoodArray;
    }

    public int GetChildCount()
    {
        return transform.childCount;
    }

    public GameObject GetFood(int index)
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
