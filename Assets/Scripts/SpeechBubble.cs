using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    float foodIconScale = 0.4f;

    private void Start()
    {
        Hide();
    }

    public void Show(Customer currentCustomer)
    {
        transform.GetComponent<SpriteRenderer>().enabled = true;
        GameObject speechLocation = currentCustomer.transform.Find("SpeechBubbleLocation").gameObject;
        transform.position = speechLocation.transform.position;        
    }

    public void Hide()
    {
        transform.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void DisplayFood(Food[] foodArray, FoodList foodList)
    {
        int index = 0;
        foreach (Food food in foodArray)
        {
            Food newFood = Instantiate(food, transform.GetChild(index).position,
                Quaternion.identity) as Food;
            newFood.transform.localScale = new Vector3(foodIconScale, foodIconScale, 0.0f);
            newFood.transform.parent = foodList.transform;
            index += 1;
        }
    }
}
