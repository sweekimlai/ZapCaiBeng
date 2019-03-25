using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] CustomerCommands customerCommands;
    private void OnMouseDown()
    {
        /*
            When user click or select this food image,
            send the name of the selected food to check if 
            it is in the food order list
        */
        if (!customerCommands)
        {
            Debug.LogError("customerCommands not found");
        }
        Sprite foodImage = GetComponent<SpriteRenderer>().sprite;
        customerCommands.GetComponent<CustomerCommands>().FindMatchingFood(foodImage.name);
    }
}
