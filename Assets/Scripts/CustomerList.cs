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
    public bool MoveUpInQueue { get; set; }
    public int MoveUpIndex { get; set; }
    public float MoveUpTarget { get; set; }

    private void Start()
    {
        MoveUpInQueue = false;
        MoveUpIndex = -1;
        MoveUpTarget = 9999.9f;
    }


    public int GetChildCount()
    {
        return transform.childCount;
    }

    public GameObject GetChild(int index)
    {
        return transform.GetChild(index).gameObject;
    }

    private Customer[] GetAllCustomerArray(int customerCount)
    {
        /* Return an array of customers in random non repeated ordered */
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
        CustomerQueue gameobject and set it as current customer */
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
        /* Setting the customer to start moving with MoveTargetLocation 
        as target locator to move to */
        CurrentCustomer.MoveTargetLocation = leavePos.GetLocation().x;
        CurrentCustomer.StartMoving = true;
        CurrentCustomer.CustomerStatus = Customer.status.LEAVE;
    }

    private int FindNextCustomer(Customer thisCustomer)
    {
        int customerCount = GetChildCount();
        int nextIndex = 0;

        for (int i = 0; i < customerCount; i++)
        {
            Customer customer = transform.GetChild(i).GetComponent<Customer>();
            if(customer == thisCustomer)
            {
                nextIndex = i + 1;
            }
        }
        return nextIndex;
    }

    public void _MoveCustomerUpInQueue(Customer thisCustomer)
    {
        Debug.Log(string.Format("thisCustomer is {0}", thisCustomer.name));
        Debug.Log(string.Format("ChildCount {0}", transform.childCount));
    }

    public void MoveCustomerUpInQueue(Customer thisCustomer)
    {
        if (GetChildCount() <= 0) { return; }
        
        int index = FindNextCustomer(thisCustomer);
        
        if(index >= GetChildCount()) { return; }

        Customer customer = transform.GetChild(index).GetComponent<Customer>();
        if (customer.CustomerStatus == Customer.status.WAIT)
        {
            customer.MoveTargetLocation = transform.GetChild(index).transform.position.x - queueGap;
            customer.StartMoving = true;
        }        
    }
}
