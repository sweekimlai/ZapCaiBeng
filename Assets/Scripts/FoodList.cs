using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodList : MonoBehaviour
{
    [SerializeField] List<Food> allFood;

    public List<Food> GetFood(int orderNum)
    {
        /*
            Return a non repeated food order in list
        */
        if (orderNum > allFood.Count) { return null; }
        List<Food> randomFoodList = new List<Food>();
        for (int i = 0; i < orderNum; i++)
        {
            bool found = true;
            while (found)
            {
                found = false;
                int rnd = Random.Range(0, allFood.Count);
                Food food = allFood[rnd];
                if (!randomFoodList.Contains(food))
                {
                    randomFoodList.Add(food);
                }
                else
                {
                    found = true;
                }
            }
        }

        return randomFoodList;
    }
}
