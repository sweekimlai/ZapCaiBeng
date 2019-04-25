using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerCommands : MonoBehaviour
{   
    [SerializeField] CustomerLeaveLocation leavePos;
    [SerializeField] CustomerList customerList;
    [SerializeField] FoodList foodList;
    [SerializeField] GameObject tick;
    [SerializeField] SpeechBubble speechBubble;
    [SerializeField] TickGroup tickGroup;
    int numberOfOrder = 3;

    private void Start()
    {
        customerList.CallingAllCustomers(this);
        StartCoroutine(WaitTimeBeforeDoneServing(1.5f, emptyAction));
    }

    private void emptyAction(){}

    IEnumerator WaitTimeBeforeDoneServing(float timeToWait, System.Action action)
    {
        yield return new WaitForSeconds(timeToWait);
        action();
        customerList.CallingNextCustomer();
        customerList.MoveCustomerUpInQueue(customerList.CurrentCustomer);
    }

    public void DoneServing()
    {
        /* When serving is done, hide SpeechBubble, destroy ticks gameobject and
        set CurrentCustomer moving to left */
        if (customerList.GetChildCount() > 0)
        {
            customerList.CustomerLeaving();
            speechBubble.Hide();
            foodList.DestroyAllChildObject();
            tickGroup.DestroyAllChildObject();
        }
    }

    public void ShowFoodOrder()
    {
        /* Every Customer gameobject has a child gameobject indicating
        the position of the speech bubble. SpeechBubble object will 
        spawn at thie location. The SpeechBubble has three child
        gameobjects indicating the locations of the spawned food 
        gameobjects. At the moment the number of foods, three, is
        hardcoded */

        speechBubble.Show(customerList.CurrentCustomer);
        Food[] randomFoodArray = foodList.GetFoodArray(numberOfOrder);
        speechBubble.DisplayFood(randomFoodArray, foodList);
    }

    public void checkAllMatching()
    {
        if(foodList.GetChildCount() == tickGroup.GetChildCount())
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
        GameObject[] currentOrder = foodList.GetCurrentOrder();

        if (currentOrder.Length <= 0) { return; }

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
