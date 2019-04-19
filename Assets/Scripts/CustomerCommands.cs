using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerCommands : MonoBehaviour
{   
    [SerializeField] GameObject customerQueueLocation;
    [SerializeField] GameObject customerServeLocation;
    [SerializeField] GameObject customerLeaveLocation;
    [SerializeField] CustomerList customerList;
    [SerializeField] FoodList foodList;
    [SerializeField] GameObject tick;
    [SerializeField] GameObject speechBubble;
    [SerializeField] GameObject tickGroup;
    [SerializeField] float queueGap = 0.5f;
    [SerializeField] int topOrderLayer = 20;
    [SerializeField] [Range(1,10)] int customerCount = 10;

    GameObject currentSpeechBubble;
    GameObject currentCustomer;
    float moveSpeed;    
    float foodIconScale = 0.4f;
    int numberOfOrder = 3;

    private void Start()
    {
        CallingAllCustomers();
        StartCoroutine(WaitTimeBeforeDoneServing(1.5f, emptyAction));
    }

    private void emptyAction(){}

    private float GetServeLocation()
    {
        return customerServeLocation.transform.position.x;
    }

    private float GetLeaveLocation()
    {
        return customerLeaveLocation.transform.position.x;
    }

    IEnumerator WaitTimeBeforeDoneServing(float timeToWait, System.Action action)
    {
        yield return new WaitForSeconds(timeToWait);
        action();
        CallingNextCustomer();
    }

    public void CallingAllCustomers()
    {
        /* Gather all Customer GameObjects and line them up based on the CustomerQueue 
        gameobject X position. Set a X pos gap between each customer and assign each
        customer with an order layer */

        Customer[] allCustomerArray = customerList.GetAllCustomerArray(customerCount);
        float customerXPos = customerQueueLocation.transform.position.x;
        int customerOrderLayer = topOrderLayer;
        foreach (Customer customer in allCustomerArray)
        {
            Vector2 customerQueuePosition = new Vector2(customerXPos, customerQueueLocation.transform.position.y);
            Customer newCustomer = Instantiate(customer, customerQueuePosition, Quaternion.identity) as Customer;
            newCustomer.GetComponent<Renderer>().sortingOrder = customerOrderLayer;
            newCustomer.GetComponent<Customer>().CustomerCommands = this;
            //newCustomer.transform.parent = customerGroup.transform;
            newCustomer.transform.parent = customerList.transform;
            customerXPos += queueGap;
            customerOrderLayer -= 1;
        }
    }

    public void CallingNextCustomer()
    {
        /* Find the first in the queue, top child gameobject under 
        CustomerQueue gameobject and return it as current customer */
        int customerCount = customerList.transform.childCount;
        
        if (customerCount > 0)
        {
            for(int i = 0; i < customerCount; i++)
            {
                GameObject nextCustomer = customerList.transform.GetChild(i).gameObject;
                if (nextCustomer.GetComponent<Customer>().CustomerStatus == Customer.status.WAIT)
                {
                    currentCustomer = nextCustomer;
                    currentCustomer.GetComponent<Customer>().CustomerStatus = Customer.status.SERVE;
                    currentCustomer.GetComponent<Customer>().MoveTargetLocation = GetServeLocation();
                    currentCustomer.GetComponent<Customer>().StartMoving = true;
                    break;
                }
            }            
        }
    }

    public void DestroyFoodOrder()
    {
        /* Destroy speechBubble and foodOrderGroup gameobject
        along with all its children gameobjects */
        Destroy(currentSpeechBubble.gameObject);
        foodList.DestroyAllChildObject();
    }

    public void DoneServing()
    {
        /* When serving is done, destroy SpeechBubble, ticks gameobject and
        set CurrentCustomer moving to left */
        if (customerList.transform.childCount > 0)
        {
            currentCustomer.GetComponent<Customer>().MoveTargetLocation = GetLeaveLocation();
            currentCustomer.GetComponent<Customer>().StartMoving = true;
            currentCustomer.GetComponent<Customer>().CustomerStatus = Customer.status.LEAVE;

            DestroyFoodOrder();
            for(int i = 0; i < tickGroup.transform.childCount; i++)
            {
                Destroy(tickGroup.transform.GetChild(i).gameObject);
            }
        }
    }

    public List<GameObject> GetCurrentOrder()
    {
        /* Return all food child gameobjects under foodOrderGroup as list */
        if (!foodList) { return null; }

        List<GameObject> currentOrder = new List<GameObject>();
        int foodOrderCount = foodList.GetChildCount();
        for (int i = 0; i < foodOrderCount; i++)
        {
            currentOrder.Add(foodList.GetFood(i));
        }
        return currentOrder;
    }

    public void ShowFoodOrder()
    {
        /* Every Customer gameobject has a child gameobject indicating
        the position of the speech bubble. SpeechBubble object will 
        spawn at thie location. The SpeechBubble has three child
        gameobjects indicating the locations of the spawned food 
        gameobjects. At the moment the number of foods, three, is
        hardcoded */
        
        currentSpeechBubble = Instantiate(speechBubble, 
            currentCustomer.transform.GetChild(0).transform.position, Quaternion.identity) as GameObject;

        Food[] randomFoodArray = foodList.GetFoodArray(numberOfOrder);
        int childIndex = 0;
        foreach (Food food in randomFoodArray)
        {         
            Food newFood = Instantiate(food, currentSpeechBubble.transform.GetChild(childIndex).position,
                Quaternion.identity) as Food;
            newFood.transform.localScale = new Vector3(foodIconScale, foodIconScale, 0.0f);
            newFood.transform.parent = foodList.transform;
            childIndex += 1;
        }
    }

    public void checkAllMatching()
    {
        if(foodList.GetChildCount() == tickGroup.transform.childCount)
        {
            StartCoroutine(WaitTimeBeforeDoneServing(0.25f,DoneServing));
        }
    }

    public void FindMatchingFood(string selectedFood)
    {
        /* Find matching food from user selected food. If match is found,
        display a tick and set the correct food image alpha to half */

        if(customerList.transform.childCount <= 0) { return; }

        bool foodFound = false;
        List<GameObject> currentOrder = GetCurrentOrder();

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

        checkAllMatching();

        if(!foodFound)
        {
            Debug.Log(string.Format("Selected food {0} not found", selectedFood));
        }
    }
}
