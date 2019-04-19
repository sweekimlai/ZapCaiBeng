using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerServe : MonoBehaviour
{
    [SerializeField] CustomerCommands CustomerCommands;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Customer>().StartMoving = false;
        CustomerCommands.ShowFoodOrder();
    }
}
