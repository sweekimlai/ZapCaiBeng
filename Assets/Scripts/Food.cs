using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private void OnMouseDown()
    {
        /*
            When user click or select this food image,
            send the name of the selected food to check if 
            it is in the food order list
        */
        Sprite foodImage = GetComponent<SpriteRenderer>().sprite;
        FindObjectOfType<CustomerCommands>().FindMatchingFood(foodImage.name);
    }
}
