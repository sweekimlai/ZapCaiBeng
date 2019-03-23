using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerCommands : MonoBehaviour
{
    [SerializeField] GameObject customerQueue;
    float queueGap;
    float moveSpeed;
    int topOrderLayer;

    private void Start()
    {
        if (!customerQueue)
        {
            Debug.LogError("No CustomerQueue gameobject found.");
        }

        queueGap = customerQueue.GetComponent<CustomerQueue>().GetQueueGap();
        topOrderLayer = customerQueue.GetComponent<CustomerQueue>().GetTopOrderLayer();
    }

    public void CallingCustomer()
    {
        /*
            Gather all Customer GameObjects and line them up based on the CustomerQueue 
            gameobject X position. Set a X pos gap between each customer and assign each
            customer with an order layer
        */
        List<Customer> allCustomers = FindObjectOfType<CustomerList>().GetAllCustomers();
        float customerXPos = customerQueue.transform.position.x;
        int customerOrderLayer = topOrderLayer;
        foreach (Customer customer in allCustomers)
        {
            Vector2 customerQueuePosition = new Vector2(customerXPos, customerQueue.transform.position.y);
            Customer newCustomer = Instantiate(customer, customerQueuePosition, Quaternion.identity) as Customer;
            newCustomer.GetComponent<Renderer>().sortingOrder = customerOrderLayer;
            newCustomer.transform.parent = customerQueue.transform;
            customerXPos += queueGap;
            customerOrderLayer -= 1;
        }
    }

    public void GetCurrentCustomer()
    {
        /*
            Find the first in the queue, top child gameobject under 
            CustomerQueue gameobject and return it as current customer
        */
        if(FindObjectOfType<CustomerQueue>().GetCustomerCount() > 0)
        {
            GameObject currentCustomer = FindObjectOfType<CustomerQueue>().GetCurrentCustomer();
            currentCustomer.GetComponent<Customer>().StartMoving();
        }
    }

    public void DoneServing()
    {
        /*
            When serving is done, destroy SpeechBubble gameobject and
            set CurrentCustomer moving to left
        */
        if (FindObjectOfType<CustomerQueue>().GetCustomerCount() > 0)
        {
            GameObject currentCustomer = FindObjectOfType<CustomerQueue>().GetCurrentCustomer();
            currentCustomer.GetComponent<Customer>().StartMoving();
            currentCustomer.GetComponent<Customer>().DestroyFoodOrder();
        }
    }
}
