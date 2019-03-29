using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerCommands : MonoBehaviour
{   
    [SerializeField] GameObject customerQueueLocation;
    [SerializeField] GameObject customerList;    
    [SerializeField] GameObject tick;
    [SerializeField] float queueGap = 0.5f;
    [SerializeField] int topOrderLayer = 20;
    GameObject customerGroup;
    GameObject tickGroup;
    float moveSpeed;

    private void Start()
    {
        customerGroup = new GameObject("customerGroup");
        tickGroup = new GameObject("tickGroup");
    }

    public void CallingAllCustomers()
    {
        /*
            Gather all Customer GameObjects and line them up based on the CustomerQueue 
            gameobject X position. Set a X pos gap between each customer and assign each
            customer with an order layer
        */
        List<Customer> allCustomers = customerList.GetComponent<CustomerList>().GetAllCustomers();
        float customerXPos = customerQueueLocation.transform.position.x;
        int customerOrderLayer = topOrderLayer;
        foreach (Customer customer in allCustomers)
        {
            Vector2 customerQueuePosition = new Vector2(customerXPos, customerQueueLocation.transform.position.y);
            Customer newCustomer = Instantiate(customer, customerQueuePosition, Quaternion.identity) as Customer;
            newCustomer.GetComponent<Renderer>().sortingOrder = customerOrderLayer;
            newCustomer.transform.parent = customerGroup.transform;
            customerXPos += queueGap;
            customerOrderLayer -= 1;
        }
    }

    public void CallingFirstCustomer()
    {
        /*
            Find the first in the queue, top child gameobject under 
            CustomerQueue gameobject and return it as current customer
        */
        if(customerGroup.transform.childCount > 0)
        {
            GameObject currentCustomer = customerGroup.transform.GetChild(0).gameObject;
            currentCustomer.GetComponent<Customer>().StartMoving();
        }
    }

    public void DoneServing()
    {
        /*
            When serving is done, destroy SpeechBubble, ticks gameobject and
            set CurrentCustomer moving to left
        */
        if (customerGroup.transform.childCount > 0)
        {
            GameObject currentCustomer = customerGroup.transform.GetChild(0).gameObject;
            currentCustomer.GetComponent<Customer>().StartMoving();
            currentCustomer.GetComponent<Customer>().DestroyFoodOrder();            
            for(int i = 0; i < tickGroup.transform.childCount; i++)
            {
                Destroy(tickGroup.transform.GetChild(i).gameObject);
            }
        }
    }

    public void FindMatchingFood(string selectedFood)
    {
        /*
            Find matching food from user selected food. If match is found,
            display a tick and set the correct food image alpha to half.
        */
        if(customerGroup.transform.childCount <= 0) { return; }

        bool foodFound = false;
        GameObject currentCustomer = customerGroup.transform.GetChild(0).gameObject;
        List<GameObject> currentOrder = currentCustomer.GetComponent<Customer>().GetCurrentOrder();

        if (currentOrder.Count <= 0) { return; }

        foreach (GameObject order in currentOrder)
        {
            SpriteRenderer foodSprite = order.GetComponent<SpriteRenderer>();
            if(foodSprite.sprite.name == selectedFood)
            {
                foodFound = true;
                foodSprite.color = new Color(1f, 1f, 1f, 0.5f);                
                GameObject newTick = Instantiate(tick, order.transform.position, Quaternion.identity) as GameObject;
                newTick.transform.parent = tickGroup.transform;
            }
        }

        if(!foodFound)
        {
            Debug.Log(string.Format("Selected food {0} not found", selectedFood));
        }
    }
}
