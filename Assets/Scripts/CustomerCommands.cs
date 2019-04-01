using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerCommands : MonoBehaviour
{   
    [SerializeField] GameObject customerQueueLocation;
    [SerializeField] GameObject customerList;    
    [SerializeField] GameObject tick;
    [SerializeField] GameObject speechBubble;
    [SerializeField] GameObject foodList;
    [SerializeField] GameObject customerGroup;
    [SerializeField] GameObject foodOrderGroup;
    [SerializeField] GameObject tickGroup;
    [SerializeField] float queueGap = 0.5f;
    [SerializeField] int topOrderLayer = 20;

    GameObject currentSpeechBubble;
    GameObject currentCustomer;
    float moveSpeed;    
    float foodIconScale = 0.4f;
    int numberOfOrder = 3;

    IEnumerator WaitTimeBeforeDoneServing(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        DoneServing();
        CallingNextCustomer();
    }

    public void CallingAllCustomers()
    {
        /* Gather all Customer GameObjects and line them up based on the CustomerQueue 
        gameobject X position. Set a X pos gap between each customer and assign each
        customer with an order layer */

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

    public void CallingNextCustomer()
    {
        /* Find the first in the queue, top child gameobject under 
        CustomerQueue gameobject and return it as current customer */
        int customerCount = customerGroup.transform.childCount;
        
        if (customerCount > 0)
        {
            for(int i = 0; i < customerCount; i++)
            {
                GameObject nextCustomer = customerGroup.transform.GetChild(i).gameObject;
                if(nextCustomer.GetComponent<Customer>().NotYetServed())
                {
                    currentCustomer = nextCustomer;
                    break;
                }
            }

            currentCustomer.GetComponent<Customer>().StartMoving();          
        }
    }

    public void DestroyFoodOrder()
    {
        /* Destroy speechBubble and foodOrderGroup gameobject
        along with all its children gameobjects */
        Destroy(currentSpeechBubble.gameObject);        
        for (int i = 0; i < foodOrderGroup.transform.childCount; i++)
        {
            Destroy(foodOrderGroup.transform.GetChild(i).gameObject);
        }
    }

    public void DoneServing()
    {
        /* When serving is done, destroy SpeechBubble, ticks gameobject and
        set CurrentCustomer moving to left */
        if (customerGroup.transform.childCount > 0)
        {
            currentCustomer = customerGroup.transform.GetChild(0).gameObject;
            currentCustomer.GetComponent<Customer>().StartMoving();
            
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
        if (!foodOrderGroup) { return null; }

        List<GameObject> currentOrder = new List<GameObject>();
        for (int i = 0; i < foodOrderGroup.transform.childCount; i++)
        {
            currentOrder.Add(foodOrderGroup.transform.GetChild(i).gameObject);
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
        //currentCustomer = customerGroup.transform.GetChild(0).gameObject;
        currentSpeechBubble = Instantiate(speechBubble, 
            currentCustomer.transform.GetChild(0).transform.position, Quaternion.identity) as GameObject;

        List<Food> randomFood = foodList.GetComponent<FoodList>().GetFood(numberOfOrder);
        int childIndex = 0;
        foreach (Food food in randomFood)
        {
            Food newFood = Instantiate(food, currentSpeechBubble.transform.GetChild(childIndex).position,
                Quaternion.identity) as Food;
            newFood.transform.localScale = new Vector3(foodIconScale, foodIconScale, 0.0f);
            newFood.transform.parent = foodOrderGroup.transform;
            childIndex += 1;
        }
    }

    public void checkAllMatching()
    {
        if(foodOrderGroup.transform.childCount == tickGroup.transform.childCount)
        {
            currentCustomer.GetComponent<Customer>().SetServeStatus(false);
            StartCoroutine(WaitTimeBeforeDoneServing(0.25f));            
        }
    }

    public void FindMatchingFood(string selectedFood)
    {
        /* Find matching food from user selected food. If match is found,
        display a tick and set the correct food image alpha to half */

        if(customerGroup.transform.childCount <= 0) { return; }

        bool foodFound = false;
        GameObject currentCustomer = customerGroup.transform.GetChild(0).gameObject;
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
