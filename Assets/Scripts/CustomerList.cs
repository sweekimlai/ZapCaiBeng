using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomerList : MonoBehaviour
{
    [SerializeField] CustomerQueueLocation queuePos;
    [SerializeField] CustomerServeLocation servePos;
    [SerializeField] CustomerLeaveLocation leavePos;
    [SerializeField] [Range(1, 10)] int customerCount = 10;
    [SerializeField] int topOrderLayer = 20;
    [SerializeField] float queueGap = 0.5f;
    [SerializeField] Customer[] allCustomerArray = new Customer[10];

    public Customer CurrentCustomer { get; set; }

    public int GetChildCount()
    {
        return transform.childCount;
    }

    private Customer[] GetAllCustomerArray(int customerCount)
    {
        Customer[] shuffledCustomerArray = new Customer[customerCount];
        for(int i = 0; i < customerCount; i++)
        {
            bool found = true;
            while(found)
            {
                found = false;
                int rnd = Random.Range(0, 10);
                Customer customer = allCustomerArray[rnd];
                if(!shuffledCustomerArray.Contains(customer))
                {
                    shuffledCustomerArray[i] = customer;
                }
                else
                {
                    found = true;
                }
            }
        }
        return shuffledCustomerArray;
    }

    public void CallingAllCustomers(CustomerCommands commands)
    {
        /* Gather all Customer GameObjects and line them up based on the CustomerQueue 
        gameobject X position. Set a X pos gap between each customer and assign each
        customer with an order layer */

        Customer[] allCustomerArray = GetAllCustomerArray(customerCount);
        float customerXPos = queuePos.GetLocation().x;
        int customerOrderLayer = topOrderLayer;
        foreach (Customer customer in allCustomerArray)
        {
            Vector2 customerQueuePosition = new Vector2(customerXPos, queuePos.GetLocation().y);
            Customer newCustomer = Instantiate(customer, customerQueuePosition, Quaternion.identity) as Customer;
            newCustomer.GetComponent<Renderer>().sortingOrder = customerOrderLayer;
            newCustomer.GetComponent<Customer>().CustomerCommands = commands;
            newCustomer.transform.parent = transform;
            customerXPos += queueGap;
            customerOrderLayer -= 1;
        }
    }

    public void CallingNextCustomer()
    {
        /* Find the first in the queue, top child gameobject under 
        CustomerQueue gameobject and return it as current customer */
        int customerCount = transform.childCount;

        if (customerCount > 0)
        {
            for (int i = 0; i < customerCount; i++)
            {
                Customer nextCustomer = transform.GetChild(i).GetComponent<Customer>();
                if (nextCustomer.CustomerStatus == Customer.status.WAIT)
                {
                    CurrentCustomer = nextCustomer;
                    CurrentCustomer.CustomerStatus = Customer.status.SERVE;
                    CurrentCustomer.MoveTargetLocation = servePos.GetLocation().x;
                    CurrentCustomer.StartMoving = true;
                    break;
                }
            }
        }
    }

    public void CustomerLeaving()
    {
        CurrentCustomer.MoveTargetLocation = leavePos.GetLocation().x;
        CurrentCustomer.StartMoving = true;
        CurrentCustomer.CustomerStatus = Customer.status.LEAVE;
    }
}
