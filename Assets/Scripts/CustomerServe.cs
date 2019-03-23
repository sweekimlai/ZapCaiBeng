using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerServe : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Customer>().StopMoving();
        collision.GetComponent<Customer>().ShowFoodOrder();
    }
}
