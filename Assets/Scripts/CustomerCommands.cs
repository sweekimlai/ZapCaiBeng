using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerCommands : MonoBehaviour
{
    [SerializeField] GameObject foodOrder;
    [SerializeField] GameObject customerQueue;
    [SerializeField] GameObject customerList;    
    [SerializeField] GameObject ticksDisplay;
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

    public GameObject GetFoodOrder()
    {
        return foodOrder;
    }

    public void CallingAllCustomers()
    {
        /*
            Gather all Customer GameObjects and line them up based on the CustomerQueue 
            gameobject X position. Set a X pos gap between each customer and assign each
            customer with an order layer
        */
        List<Customer> allCustomers = customerList.GetComponent<CustomerList>().GetAllCustomers();
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

    public void CallingFirstCustomer()
    {
        /*
            Find the first in the queue, top child gameobject under 
            CustomerQueue gameobject and return it as current customer
        */
        if(customerQueue.GetComponent<CustomerQueue>().GetCustomerCount() > 0)
        {
            GameObject currentCustomer = customerQueue.GetComponent<CustomerQueue>().GetCurrentCustomer();
            currentCustomer.GetComponent<Customer>().StartMoving();
        }
    }

    public void DoneServing()
    {
        /*
            When serving is done, destroy SpeechBubble, ticks gameobject and
            set CurrentCustomer moving to left
        */
        if (customerQueue.GetComponent<CustomerQueue>().GetCustomerCount() > 0)
        {
            GameObject currentCustomer = customerQueue.GetComponent<CustomerQueue>().GetCurrentCustomer();
            currentCustomer.GetComponent<Customer>().StartMoving();
            currentCustomer.GetComponent<Customer>().DestroyFoodOrder();
            ticksDisplay.GetComponent<TicksDisplay>().DestroyAllTicks();
        }
    }

    public void FindMatchingFood(string selectedFood)
    {
        /*
            Find matching food from user selected food. If match is found,
            display a tick and set the correct food image alpha to half.
        */
        bool foodFound = false;
        List<GameObject> currentOrder = foodOrder.GetComponent<FoodOrder>().GetCurrentOrder();
        
        if(currentOrder.Count <= 0) { return; }

        foreach (GameObject order in currentOrder)
        {
            SpriteRenderer foodSprite = order.GetComponent<SpriteRenderer>();
            if(foodSprite.sprite.name == selectedFood)
            {
                foodFound = true;
                foodSprite.color = new Color(1f, 1f, 1f, 0.5f);
                ticksDisplay.GetComponent<TicksDisplay>().AddTick(order);
            }
        }

        if(!foodFound)
        {
            Debug.Log(string.Format("Selected food {0} not found", selectedFood));
        }
    }
}
